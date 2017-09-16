using System;
namespace PetzoldComputer
{
	public class OR : IOr, IOutput
	{
		#region Construction
		public OR()
		{
			_relay1 = new Relay();
			_relay2 = new Relay();
			SetOutput();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		private VoltageSignal _output;

		private Action<object> OutputChanged;
		#endregion

		#region Ior Members

		public VoltageSignal Voltage
		{
			get { return _relay1.Voltage; }
			set
			{
				VoltageSignal oldOutput = _output;

				_relay1.Voltage = value;
				_relay2.Voltage = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal A
		{
			get { return _relay1.Input; }
			set {
				VoltageSignal oldOutput = _output;

				_relay1.Input = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal B
		{
			get { return _relay2.Input; }
			set {
				VoltageSignal oldOutput = _output;

				_relay2.Input = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal O
		{
			get { return _output; }
		}

		#endregion

		#region IOutput Members

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

		#region Private Members
		private void SetOutput()
		{
			if (_relay1.Output == VoltageSignal.HIGH || _relay2.Output == VoltageSignal.HIGH)
			{
				_output = VoltageSignal.HIGH;
			}
			else
			{
				_output = VoltageSignal.LOW;
			}
		}

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
}

/*
$Log: /PetzoldComputer/OR.cs $ $NoKeyWords:$
 * 
 * 4     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
