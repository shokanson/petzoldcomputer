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

	public class HalfAdder_2
	{
		public HalfAdder_2()
		{
			DoWireUp();
		}

		private readonly XOR_2 _xor = new XOR_2();
		private readonly AND_2 _and = new AND_2();

		public ConnectionPoint V => _xor.V;
		public ConnectionPoint A => _xor.A;
		public ConnectionPoint B => _xor.B;
		public ConnectionPoint Sum => _xor.O;
		public ConnectionPoint Carry => _and.O;

		public override string ToString() => $"Sum: {Sum}; Carry: {Carry}";

		private void DoWireUp()
		{
			_xor.V.ConnectTo(_and.V);
			_xor.A.ConnectTo(_and.A);
			_xor.B.ConnectTo(_and.B);
		}
	}
}
