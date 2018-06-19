namespace PetzoldComputer
{
	public class NOR3
	{
		public NOR3(string name)
		{
			_not1 = new NOT($"{name}-nor3.a");
			_not2 = new NOT($"{name}-nor3.b");
			_not3 = new NOT($"{name}-nor3.c");

			DoWireUp();

			Components.Record(nameof(NOR3));
		}

		private readonly NOT _not1;
		private readonly NOT _not2;
		private readonly NOT _not3;

		public ConnectionPoint V => _not1.V;
		public ConnectionPoint A => _not1.Input;
		public ConnectionPoint B => _not2.Input;
		public ConnectionPoint C => _not3.Input;
		public ConnectionPoint O => _not3.Output;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_not1.Output.ConnectTo(_not2.V);
			_not2.Output.ConnectTo(_not3.V);
		}
	}
}
