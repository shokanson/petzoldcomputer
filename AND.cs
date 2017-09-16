using System;
namespace PetzoldComputer
{
	public class AND : IAnd, IOutput
	{
		#region Construction
		public AND()
		{
			_relay1 = new Relay();
			_relay2 = new Relay();

			DoWireup();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		#endregion

		#region IAnd Members
		public VoltageSignal Voltage
		{
			get { return _relay1.Voltage; }
			set { _relay1.Voltage = value; }
		}

		public VoltageSignal A
		{
			get { return _relay1.Input; }
			set { _relay1.Input = value; }
		}

		public VoltageSignal B
		{
			get { return _relay2.Input; }
			set { _relay2.Input = value; }
		}

		public VoltageSignal O
		{
			get { return _relay2.Output; }
		}
		#endregion

		#region IOutput Members
		public void AddOutputHandler(Action<object> handler)
		{
			((IOutput)_relay2).AddOutputHandler(handler);
		}
		#endregion

		#region Object Overrides
		public override string ToString()
		{
			return _relay2.Output.ToString();
		}
		#endregion

		#region Private Members
		private void DoWireup()
		{
			((IOutput)_relay1).AddOutputHandler( (o) => { _relay2.Voltage = _relay1.Output; });
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/AND.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/