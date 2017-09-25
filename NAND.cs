namespace PetzoldComputer
{
	public class NAND
	{
		public NAND(string name)
		{
			_not1 = new NOT($"{name}-nand.a");
			_not2 = new NOT($"{name}-nand.b");
			_output = new ConnectionPoint($"{name}-nand.out");

			DoWireUp();

			Components.Record(nameof(NAND));
		}

		private readonly NOT _not1;
		private readonly NOT _not2;
		private readonly ConnectionPoint _output;

		public ConnectionPoint V => _not1.V;
		public ConnectionPoint A => _not1.Input;
		public ConnectionPoint B => _not2.Input;
		public ConnectionPoint O => _output;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_not1.V.ConnectTo(_not2.V);
			// wiring it up this way doesn't really make sense
			//_not1.Output.ConnectTo(_output);
			//_not2.Output.ConnectTo(_output);
			// ...so we'll do some manual wiring
			_not1.Output.Changed += OnNotOutputChanged;
			_not2.Output.Changed += OnNotOutputChanged;
		}

		private void OnNotOutputChanged(ConnectionPoint cp)
		{
			_output.V = _not1.Output.V == VoltageSignal.HIGH || _not2.Output.V == VoltageSignal.HIGH
				? VoltageSignal.HIGH
				: VoltageSignal.LOW;
		}
	}
}