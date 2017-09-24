using System;
namespace PetzoldComputer
{
	public abstract class RelayBase : IRelay, IOutput
	{
		#region Construction
		protected RelayBase()
		{
			_input = VoltageSignal.LOW;
			_voltage = VoltageSignal.LOW;
			_output = VoltageSignal.LOW;
		}
		#endregion

		#region Implementation
		protected VoltageSignal _input;
		protected VoltageSignal _voltage;
		protected VoltageSignal _output;

		private Action<object> OutputChanged;
		#endregion

		#region IRelay Members
		public VoltageSignal Input
		{
			get => _input;
			set
			{
				VoltageSignal oldOutput = _output;

				_input = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal Voltage
		{
			get => _voltage;
			set
			{
				VoltageSignal oldOutput = _output;

				_voltage = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal Output => _output;
		#endregion

		#region IOutputEvent Methods
		public void AddOutputHandler(Action<object> handler) => OutputChanged += handler;
		#endregion

		#region Object Override Members
		public override string ToString() => _output.ToString();
		#endregion

		#region Protected Members
		// allow subclasses to do their thing
		protected abstract void SetOutput();
		#endregion

		#region Private Members
		private void FireEvent(VoltageSignal oldOutput)
		{
			if (_output != oldOutput)
			{
				OutputChanged?.Invoke(this);
			}
		}
		#endregion
	}

	public class Relay : RelayBase
	{
		#region RelayBase Override Members
		protected override void SetOutput() =>
			_output = _input == VoltageSignal.LOW
				? VoltageSignal.LOW
				: _voltage;
		#endregion
	}

	public class InvertedRelay : RelayBase
	{
		#region RelayBase Override Members
		protected override void SetOutput() =>
			_output = _input == VoltageSignal.HIGH
				? VoltageSignal.LOW
				: _voltage;
		#endregion
	}

	public class Relay_2
	{
		private static int Counter = 0;

		public Relay_2(string name, bool inverted = false)
		{
			_inverted = inverted;
			_myId = ++Counter;
			_name = name;

			_voltage = new ConnectionPoint($"{name}-relay.v");
			_input = new ConnectionPoint($"{name}-relay.in");
			_output = new ConnectionPoint($"{name}-relay.out");

			DoWireUp();
		}

		private readonly int _myId;
		private readonly string _name;
		private bool _inverted;
		private readonly ConnectionPoint _voltage;
		private readonly ConnectionPoint _input;
		private readonly ConnectionPoint _output;

		public ConnectionPoint Voltage => _voltage;
		public ConnectionPoint Input => _input;
		public ConnectionPoint Output => _output;

		public override string ToString() => _output.ToString();


		private void DoWireUp()
		{
			// when the input voltage changes, (de)activate the switch
			_input.Changed += input => IsSwitchActivated = input.V == VoltageSignal.HIGH;

			_voltage.Changed += voltage => UpdateOutput(voltage.V);
		}

		// internal switch details
		private bool _switchActive;
		private bool IsSwitchActivated
		{
			get => _switchActive;
			set
			{
				bool originalValue = _switchActive;
				_switchActive = value;
				if (originalValue != value) UpdateOutput(_voltage.V);
			}
		}

		private void UpdateOutput(VoltageSignal voltage)
		{
			_output.V = _inverted
				? _switchActive ? VoltageSignal.LOW : voltage
				: _switchActive ? voltage : VoltageSignal.LOW;
		}
	}
}
