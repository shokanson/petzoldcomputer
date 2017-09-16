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
			get { return _or.Voltage; }
			set
			{
				_or.Voltage = value;
				_nand.Voltage = value;
				_and.Voltage = value;
			}
		}

		public VoltageSignal A
		{
			get { return _or.A; }
			set
			{
				_or.A = value;
				_nand.A = value;
			}
		}

		public VoltageSignal B
		{
			get { return _or.B; }
			set
			{
				_or.B = value;
				_nand.B = value;
			}
		}

		public VoltageSignal O
		{
			get { return _and.O; }
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler)
		{
			((IOutput)_and).AddOutputHandler(handler);
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return O.ToString();
		}
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

/*
$Log: /PetzoldComputer/XOR.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
