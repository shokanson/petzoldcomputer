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

	// adds an oscillator
	// see notes in Phase1Computer for info about the rest
	public class Phase2Computer_2 : Phase1Computer_2
	{
		public Phase2Computer_2(string name, uint nIterations)
			: base(name)
		{
			_oscillator = new Oscillator_2($"{name}-oscillator", nIterations);
			V.ConnectTo(_oscillator.V);
			// tie the computer's clock input to the oscillator's output
			_oscillator.Output.ConnectTo(Clk);
		}
		private readonly Oscillator_2 _oscillator;

		public Oscillator_2 Oscillator => _oscillator;
	}
}
