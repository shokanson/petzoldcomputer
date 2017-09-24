namespace PetzoldComputer
{
	public class XOR
	{
		public XOR(string name)
		{
			_or = new OR($"{name}-xor.in");
			_nand = new NAND($"{name}-xor.in");
			_and = new AND($"{name}-xor.o");

			DoWireUp();
		}

		private readonly OR _or;
		private readonly NAND _nand;
		private readonly AND _and;

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
