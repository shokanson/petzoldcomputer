namespace PetzoldComputer
{
	// adds an oscillator
	// see notes in Phase1Computer for info about the rest of the components
	public class Phase2Computer : Phase1Computer
	{
		public Phase2Computer(string name, uint nIterations)
			: base(name)
		{
			_oscillator = new Oscillator($"{name}-oscillator", nIterations);

			DoWireUp();

			Components.Record(nameof(Phase2Computer));
		}

		private readonly Oscillator _oscillator;

		public Oscillator Oscillator => _oscillator;

		private void DoWireUp()
		{
			V.ConnectTo(_oscillator.V);
			// tie the computer's clock input to the oscillator's output
			_oscillator.Output.ConnectTo(Clk);
		}
	}
}
