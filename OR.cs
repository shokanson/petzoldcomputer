namespace PetzoldComputer
{
	public class OR
	{
		public OR(string name)
		{
			_relay1 = new Relay($"{name}-or.a");
			_relay2 = new Relay($"{name}-or.b");
			O = new ConnectionPoint($"{name}-or.out");

			DoWireUp();

			Components.Record(nameof(OR));
		}

		private readonly Relay _relay1;
		private readonly Relay _relay2;

        public ConnectionPoint V => _relay1.Voltage;
		public ConnectionPoint A => _relay1.Input;
		public ConnectionPoint B => _relay2.Input;
        public ConnectionPoint O { get; }

        public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_relay1.Voltage.ConnectTo(_relay2.Voltage);
			// wiring it up this way causes extra events to fire...
			//_relay1.Output.ConnectTo(_output);
			//_relay2.Output.ConnectTo(_output);
			// ...so we'll do some manual wiring
			_relay1.Output.Changed += OnRelayOutputChanged;
			_relay2.Output.Changed += OnRelayOutputChanged;
		}

		private void OnRelayOutputChanged(ConnectionPoint cp)
		{
			O.V = _relay1.Output.V == VoltageSignal.HIGH || _relay2.Output.V == VoltageSignal.HIGH
				? VoltageSignal.HIGH
				: VoltageSignal.LOW;
		}
	}
}
