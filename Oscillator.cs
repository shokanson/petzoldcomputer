namespace PetzoldComputer
{
	public class Oscillator
	{
		public Oscillator(string name)
			: this(name, 0)   // a never-ending oscillator
		{ }

		public Oscillator(string name, uint nOscillations)
		{
			V = new ConnectionPoint($"{name}-oscillator.v");
			Output = new ConnectionPoint($"{name}-oscillator.output");
			NOscillations = nOscillations;
		}

		public uint NOscillations { get; private set; }
		public readonly ConnectionPoint V;
		public readonly ConnectionPoint Output;

		public void Start()
		{
			if (V.V == VoltageSignal.LOW) return;

			uint nOscillations = 0;
			do
			{
				Output.V = VoltageSignal.HIGH;
				Output.V = VoltageSignal.LOW;
			} while (NOscillations == 0 || ++nOscillations < NOscillations);
		}
	}
}
