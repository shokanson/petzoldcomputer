namespace PetzoldComputer
{
	public class NOR_2
	{
		public NOR_2(string name)
		{
			_not1 = new NOT_2($"{name}-nor.a");
			_not2 = new NOT_2($"{name}-nor.b");

			DoWireUp();
		}

		private readonly NOT_2 _not1;
		private readonly NOT_2 _not2;

		public ConnectionPoint V { get => _not1.V; }
		public ConnectionPoint A { get => _not1.Input; }
		public ConnectionPoint B { get => _not2.Input; }
		public ConnectionPoint O { get => _not2.Output; }

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_not1.Output.ConnectTo(_not2.V);
		}
	}
}
