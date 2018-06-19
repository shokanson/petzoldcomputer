namespace PetzoldComputer
{
	public class AND
	{
		public AND(string name)
		{
			_relay1 = new Relay($"{name}-and.a");
			_relay2 = new Relay($"{name}-and.b");

			DoWireUp();

			Components.Record(nameof(AND));
		}

		private readonly Relay _relay1;
		private readonly Relay _relay2;

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
