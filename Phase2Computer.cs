namespace PetzoldComputer
{
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
