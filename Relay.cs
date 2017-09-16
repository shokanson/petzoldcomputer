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
			get { return _input; }
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
			get { return _voltage; }
			set
			{
				VoltageSignal oldOutput = _output;

				_voltage = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal Output
		{
			get { return _output; }
		}
		#endregion

		#region IOutputEvent Methods
		public void AddOutputHandler(Action<object> handler)
		{
			OutputChanged += handler;
		}
		#endregion

		#region Object Override Members
		public override string ToString()
		{
			return _output.ToString();
		}
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
				if (OutputChanged != null)
				{
					OutputChanged(this);
				}
			}
		}
		#endregion
	}

	public class Relay : RelayBase
	{
		#region RelayBase Override Members
		protected override void SetOutput()
		{
			if (_input == VoltageSignal.LOW)
			{
				_output = VoltageSignal.LOW;
			}
			else
			{
				_output = _voltage;
			}
		}
		#endregion
	}

	public class InvertedRelay : RelayBase
	{
		#region RelayBase Override Members
		protected override void SetOutput()
		{
			if (_input == VoltageSignal.HIGH)
			{
				_output = VoltageSignal.LOW;
			}
			else
			{
				_output = _voltage;
			}
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/Relay.cs $ $NoKeyWords:$
 * 
 * 6     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
