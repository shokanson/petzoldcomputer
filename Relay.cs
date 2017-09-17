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
}
