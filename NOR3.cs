using System;
namespace PetzoldComputer
{
	public class NOR3 : INor3, IOutput
	{
		#region Construction
		public NOR3()
		{
			_relay1 = new InvertedRelay();
			_relay2 = new InvertedRelay();
			_relay3 = new InvertedRelay();

			DoWireup();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		private IRelay _relay3;
		#endregion

		#region INor Members

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
			get { return _relay3.Output; }
		}

		#endregion

		#region INor3 Members

		public VoltageSignal C
		{
			get { return _relay3.Input; }
			set { _relay3.Input = value; }
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler)
		{
			((IOutput)_relay3).AddOutputHandler(handler);
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return _relay3.Output.ToString();
		}
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_relay1).AddOutputHandler((o) => { _relay2.Voltage = _relay1.Output; });
			((IOutput)_relay2).AddOutputHandler((o) => { _relay3.Voltage = _relay2.Output; });
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/NOR3.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
