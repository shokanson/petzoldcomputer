using System;

namespace PetzoldComputer
{
	public class XOR : IXor, IOutput
	{
		#region Construction
		public XOR()
		{
			_or = new OR();
			_nand = new NAND();
			_and = new AND();

			DoWireup();
		}
		#endregion

		#region Implementation
		private IOr _or;
		private INand _nand;
		private IAnd _and;
		#endregion

		#region IXor Members

		public VoltageSignal Voltage
		{
			get => _or.Voltage;
			set => _or.Voltage = _nand.Voltage = _and.Voltage = value;
		}

		public VoltageSignal A
		{
			get => _or.A;
			set => _or.A = _nand.A = value;
		}

		public VoltageSignal B
		{
			get => _or.B;
			set => _or.B = _nand.B = value;
		}

		public VoltageSignal O => _and.O;

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => ((IOutput)_and).AddOutputHandler(handler);

		#endregion

		#region Object Override Methods
		public override string ToString() => O.ToString();
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_or).AddOutputHandler(InternalEventHandler);
			((IOutput)_nand).AddOutputHandler(InternalEventHandler);
		}

		private void InternalEventHandler(object o)
		{
			_and.A = _or.O;
			_and.B = _nand.O;
		}
		#endregion
	}
}
