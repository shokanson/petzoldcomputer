namespace PetzoldComputer
{
	// adds an oscillator
	// see notes in Phase1Computer for info about the rest of the components
	public class Phase2Computer : Phase1Computer
	{
		public Phase2Computer(string name, uint nIterations)
			: base(name)
		{
			Oscillator = new Oscillator($"{name}-oscillator", nIterations);

			DoWireUp();

			Components.Record(nameof(Phase2Computer));
		}

        public Oscillator Oscillator { get; }

        private void DoWireUp()
		{
			V.ConnectTo(Oscillator.V);
			// tie the computer's clock input to the oscillator's output
			Oscillator.Output.ConnectTo(Clk);
		}
	}
}
