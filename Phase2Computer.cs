namespace PetzoldComputer
{
	// adds an oscillator
	// see notes in Phase1Computer for info about the rest
	public class Phase2Computer : Phase1Computer
	{
		public Phase2Computer(uint nIterations)
		{
			_oscillator = new Oscillator(nIterations);

			// tie the computer's clock input to the oscillator's output
			Oscillator.AddOutputHandler(_ => Clk = Oscillator.Output);
		}
		private readonly Oscillator _oscillator;

		public Oscillator Oscillator => _oscillator;
	}
}
