namespace PetzoldComputer
{
	public class Selector2to1_2
	{
		public Selector2to1_2(string name)
		{
			_v = new ConnectionPoint($"{name}-selector.v");
			_select = new ConnectionPoint($"{name}-selector.select");
			_not = new NOT_2($"{name}-selector.select");
			_andA = new AND_2($"{name}-selector.a");
			_andB = new AND_2($"{name}-selector.b");
			_or = new OR_2($"{name}-selector.out");

			DoWireUp();
		}

		private readonly ConnectionPoint _v;
		private readonly ConnectionPoint _select;
		private readonly NOT_2 _not;
		private readonly AND_2 _andA;
		private readonly AND_2 _andB;
		private readonly OR_2 _or;

		public ConnectionPoint V => _v;
		public ConnectionPoint Select => _select;
		public ConnectionPoint A => _andA.A;
		public ConnectionPoint B => _andB.A;
		public ConnectionPoint O => _or.O;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_v.ConnectTo(_or.V).ConnectTo(_andB.V).ConnectTo(_andA.V).ConnectTo(_not.V);
			_select.ConnectTo(_not.Input).ConnectTo(_andB.B);
			_not.Output.ConnectTo(_andA.B);
			_andA.O.ConnectTo(_or.A);
			_andB.O.ConnectTo(_or.B);
		}
	}
}
