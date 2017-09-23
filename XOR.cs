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
			((IOutput)_or).AddOutputHandler(_ => _and.A = _or.O);
			((IOutput)_nand).AddOutputHandler(_ => _and.B = _nand.O);
		}
		#endregion
	}

	public class XOR_2
	{
		public XOR_2()
		{
			DoWireUp();
		}

		private readonly OR_2 _or = new OR_2();
		private readonly NAND_2 _nand = new NAND_2();
		private readonly AND_2 _and = new AND_2();

		public ConnectionPoint V => _or.V;
		public ConnectionPoint A => _or.A;
		public ConnectionPoint B => _or.B;
		public ConnectionPoint O => _and.O;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_or.V.ConnectTo(_nand.V).ConnectTo(_and.V);
			_or.O.ConnectTo(_and.A);
			_nand.O.ConnectTo(_and.B);
			_or.A.ConnectTo(_nand.A);
			_or.B.ConnectTo(_nand.B);
		}
	}
}
