namespace PetzoldComputer
{
	public class HalfAdder
	{
		public HalfAdder(string name)
		{
			_xor = new XOR($"{name}-halfadder.sum");
			_and = new AND($"{name}-halfadder.carry");

			DoWireUp();
		}

		private readonly XOR _xor;
		private readonly AND _and;

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
