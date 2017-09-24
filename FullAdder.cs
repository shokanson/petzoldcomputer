namespace PetzoldComputer
{
	public class FullAdder_2
	{
		public FullAdder_2(string name)
		{
			_halfAdder1 = new HalfAdder_2($"{name}-fulladder.in");
			_halfAdder2 = new HalfAdder_2($"{name}-fulladder.sum");
			_or = new OR_2($"{name}-fulladder.carry");

			DoWireUp();
		}

		private readonly HalfAdder_2 _halfAdder1;
		private readonly HalfAdder_2 _halfAdder2;
		private readonly OR_2 _or;

		public ConnectionPoint V => _halfAdder1.V;
		public ConnectionPoint CarryIn => _halfAdder2.A;
		public ConnectionPoint A => _halfAdder1.A;
		public ConnectionPoint B => _halfAdder1.B;
		public ConnectionPoint Sum => _halfAdder2.Sum;
		public ConnectionPoint Carry => _or.O;

		public override string ToString() => $"Sum: {Sum}; Carry: {Carry}";

		private void DoWireUp()
		{
			_halfAdder1.V.ConnectTo(_halfAdder2.V).ConnectTo(_or.V);
			_halfAdder1.Sum.ConnectTo(_halfAdder2.B);
			_halfAdder1.Carry.ConnectTo(_or.B);
			_halfAdder2.Carry.ConnectTo(_or.A);
		}
	}
}
