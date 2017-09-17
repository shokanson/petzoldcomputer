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
			get => _sumXor.Voltage;
			set => _sumXor.Voltage = _carryAnd.Voltage = value;
		}

		public VoltageSignal A
		{
			get => _sumXor.A;
			set => _sumXor.A = _carryAnd.A = value;
		}

		public VoltageSignal B
		{
			get => _sumXor.B;
			set => _sumXor.B = _carryAnd.B = value;
		}

		public VoltageSignal Sum => _sumXor.O;
		public VoltageSignal Carry => _carryAnd.O;
		
		#endregion

		#region ISumEvent Members

		public void AddSumHandler(Action<object> handler) => ((IOutput)_sumXor).AddOutputHandler(handler);

		#endregion

		#region ICarryEvent Members

		public void AddCarryHandler(Action<object> handler) => ((IOutput)_carryAnd).AddOutputHandler(handler);

		#endregion

		#region Object Override Methods
		public override string ToString() => $"Sum: {_sumXor}; Carry: {_carryAnd}";
		#endregion
	}
}
