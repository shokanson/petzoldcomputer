namespace PetzoldComputer
{
	public class AND_2
	{
		public AND_2(string name)
		{
			_relay1 = new Relay_2($"{name}-and.a");
			_relay2 = new Relay_2($"{name}-and.b");

			DoWireUp();
		}

		private readonly Relay_2 _relay1;
		private readonly Relay_2 _relay2;

		public ConnectionPoint V { get => _relay1.Voltage; }
		public ConnectionPoint A { get => _relay1.Input; }
		public ConnectionPoint B { get => _relay2.Input; }
		public ConnectionPoint O { get => _relay2.Output; }

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_relay1.Output.ConnectTo(_relay2.Voltage);
		}
	}
}
