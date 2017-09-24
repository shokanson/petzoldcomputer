namespace PetzoldComputer
{
	public class RippleAdder8
	{
		public RippleAdder8(string name)
		{
			_adder0 = new FullAdder($"{name}_adder8.0");
			_adder1 = new FullAdder($"{name}_adder8.1");
			_adder2 = new FullAdder($"{name}_adder8.2");
			_adder3 = new FullAdder($"{name}_adder8.3");
			_adder4 = new FullAdder($"{name}_adder8.4");
			_adder5 = new FullAdder($"{name}_adder8.5");
			_adder6 = new FullAdder($"{name}_adder8.6");
			_adder7 = new FullAdder($"{name}_adder8.7");

			_adder0.V.ConnectTo(_adder1.V)
						.ConnectTo(_adder2.V)
						.ConnectTo(_adder3.V)
						.ConnectTo(_adder4.V)
						.ConnectTo(_adder5.V)
						.ConnectTo(_adder6.V)
						.ConnectTo(_adder7.V);
			_adder0.Carry.ConnectTo(_adder1.CarryIn);
			_adder1.Carry.ConnectTo(_adder2.CarryIn);
			_adder2.Carry.ConnectTo(_adder3.CarryIn);
			_adder3.Carry.ConnectTo(_adder4.CarryIn);
			_adder4.Carry.ConnectTo(_adder5.CarryIn);
			_adder5.Carry.ConnectTo(_adder6.CarryIn);
			_adder6.Carry.ConnectTo(_adder7.CarryIn);
		}

		private readonly FullAdder _adder0;
		private readonly FullAdder _adder1;
		private readonly FullAdder _adder2;
		private readonly FullAdder _adder3;
		private readonly FullAdder _adder4;
		private readonly FullAdder _adder5;
		private readonly FullAdder _adder6;
		private readonly FullAdder _adder7;

		public ConnectionPoint V => _adder0.V;

		public ConnectionPoint CarryIn => _adder0.CarryIn;

		public ConnectionPoint A0 => _adder0.A;
		public ConnectionPoint A1 => _adder1.A;
		public ConnectionPoint A2 => _adder2.A;
		public ConnectionPoint A3 => _adder3.A;
		public ConnectionPoint A4 => _adder4.A;
		public ConnectionPoint A5 => _adder5.A;
		public ConnectionPoint A6 => _adder6.A;
		public ConnectionPoint A7 => _adder7.A;

		public ConnectionPoint B0 => _adder0.B;
		public ConnectionPoint B1 => _adder1.B;
		public ConnectionPoint B2 => _adder2.B;
		public ConnectionPoint B3 => _adder3.B;
		public ConnectionPoint B4 => _adder4.B;
		public ConnectionPoint B5 => _adder5.B;
		public ConnectionPoint B6 => _adder6.B;
		public ConnectionPoint B7 => _adder7.B;

		public ConnectionPoint S0 => _adder0.Sum;
		public ConnectionPoint S1 => _adder1.Sum;
		public ConnectionPoint S2 => _adder2.Sum;
		public ConnectionPoint S3 => _adder3.Sum;
		public ConnectionPoint S4 => _adder4.Sum;
		public ConnectionPoint S5 => _adder5.Sum;
		public ConnectionPoint S6 => _adder6.Sum;
		public ConnectionPoint S7 => _adder7.Sum;

		public ConnectionPoint Carry => _adder7.Carry;

		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}:{1}{2}{3}{4}{5}{6}{7}{8}",
					Carry.V == VoltageSignal.HIGH ? 1 : 0,
					S7.V == VoltageSignal.HIGH ? 1 : 0,
					S6.V == VoltageSignal.HIGH ? 1 : 0,
					S5.V == VoltageSignal.HIGH ? 1 : 0,
					S4.V == VoltageSignal.HIGH ? 1 : 0,
					S3.V == VoltageSignal.HIGH ? 1 : 0,
					S2.V == VoltageSignal.HIGH ? 1 : 0,
					S1.V == VoltageSignal.HIGH ? 1 : 0,
					S0.V == VoltageSignal.HIGH ? 1 : 0);
	}
}
