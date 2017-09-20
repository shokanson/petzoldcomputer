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

	public enum SwitchType
	{
		NormallyOpen,
		NormallyClosed
	}

	public class Switch
	{
		private enum SwitchState
		{
			Idle,
			Activated
		}

		public Switch(SwitchType type = SwitchType.NormallyOpen)
		{
			Type = type;

			Input = new ConnectionPoint();
			Output = new ConnectionPoint();

			Input.VoltageChanged += cp => UpdateOutput(cp.Voltage);
		}

		public ConnectionPoint Input { get; private set; }
		public ConnectionPoint Output { get; private set; }
		public bool IsSwitchActivated
		{
			get => (State == SwitchState.Activated);
			set => State = (value ? SwitchState.Activated : SwitchState.Idle);
		}

		private SwitchState _state = SwitchState.Idle;
		private SwitchState State
		{
			get => _state;
			set
			{
				if (_state != value)
				{
					_state = value;
					UpdateOutput(Input.Voltage);
				}
			}
		}

		private SwitchType Type { get; set; }

		private void UpdateOutput(VoltageSignal voltage)
		{
			bool isActivated = IsSwitchActivated;
			if (Type == SwitchType.NormallyOpen)
			{
				Output.Voltage = isActivated ? voltage : VoltageSignal.LOW;
			}
			else
			{
				Output.Voltage = isActivated ? VoltageSignal.LOW : voltage;
			}
		}
	}

	public class Relay_2
	{
		public Relay_2(SwitchType switchType = SwitchType.NormallyOpen)
		{
			_input = new ConnectionPoint();
			_switch = new Switch(switchType);

			// when the input voltage changes, (de)activate the switch
			_input.VoltageChanged += _ => _switch.IsSwitchActivated = _input.Voltage == VoltageSignal.HIGH;

			_voltage = new ConnectionPoint();
			_output = new ConnectionPoint();
			_type = switchType;
			//_input.VoltageChanged += _ => IsSwitchActivated = _input.Voltage == VoltageSignal.HIGH;
			//_input.VoltageChanged += cp => UpdateOutput(cp.Voltage);
		}

		private readonly ConnectionPoint _voltage;
		private readonly ConnectionPoint _input;
		private readonly ConnectionPoint _output;
		private readonly Switch _switch;

		public ConnectionPoint Voltage => _switch.Input;
		public ConnectionPoint Input => _input;
		public ConnectionPoint Output => _switch.Output;

		// internal switch details
		private readonly SwitchType _type;
		private enum SwitchState
		{
			Idle,
			Activated
		}

		private bool IsSwitchActivated
		{
			get => (State == SwitchState.Activated);
			set => State = (value ? SwitchState.Activated : SwitchState.Idle);
		}

		private SwitchState _state = SwitchState.Idle;
		private SwitchState State
		{
			get => _state;
			set
			{
				if (_state != value)
				{
					_state = value;
					UpdateOutput(Input.Voltage);
				}
			}
		}

		private void UpdateOutput(VoltageSignal voltage)
		{
			bool isActivated = IsSwitchActivated;
			if (_type == SwitchType.NormallyOpen)
			{
				Output.Voltage = isActivated ? voltage : VoltageSignal.LOW;
			}
			else
			{
				Output.Voltage = isActivated ? VoltageSignal.LOW : voltage;
			}
		}
	}
}
