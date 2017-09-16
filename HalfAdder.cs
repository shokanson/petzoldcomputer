using System;
namespace PetzoldComputer
{
	public class HalfAdder : IHalfAdder, ISum, ICarry
	{
		#region Construction
		public HalfAdder()
		{
			_sumXor = new XOR();
			_carryAnd = new AND();
		}
		#endregion

		#region Implementation
		private IXor _sumXor;
		private IAnd _carryAnd;
		#endregion

		#region IHalfAdder Members

		public VoltageSignal Voltage
		{
			get { return _sumXor.Voltage; }
			set
			{
				_sumXor.Voltage = value;
				_carryAnd.Voltage = value;
			}
		}

		public VoltageSignal A
		{
			get { return _sumXor.A; }
			set
			{
				_sumXor.A = value;
				_carryAnd.A = value;
			}
		}

		public VoltageSignal B
		{
			get { return _sumXor.B; }
			set
			{
				_sumXor.B = value;
				_carryAnd.B = value;
			}
		}

		public VoltageSignal Sum
		{
			get { return _sumXor.O; }
		}

		public VoltageSignal Carry
		{
			get { return _carryAnd.O; }
		}

		#endregion

		#region ISumEvent Members

		public void AddSumHandler(Action<object> handler)
		{
			((IOutput)_sumXor).AddOutputHandler(handler);
		}

		#endregion

		#region ICarryEvent Members

		public void AddCarryHandler(Action<object> handler)
		{
			((IOutput)_carryAnd).AddOutputHandler(handler);
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return string.Format("Sum: {0}; Carry: {1}", _sumXor, _carryAnd);
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/HalfAdder.cs $ $NoKeyWords:$
 * 
 * 4     1/26/07 6:54a Sean
 * results of ReSharper analysis
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
