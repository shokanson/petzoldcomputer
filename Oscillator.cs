using System;

namespace PetzoldComputer
{
	public class Oscillator : IOutput
	{
		public Oscillator()
			: this(0)   // a never-ending oscillator
		{ }

		public Oscillator(uint nOscillations) => NOscillations = nOscillations;

		public uint NOscillations { get; private set; }

		public void Start()
		{
			uint nOscillations = 0;
			do
			{
				Output = VoltageSignal.HIGH;
				_handler?.Invoke(this);
				Output = VoltageSignal.LOW;
				_handler?.Invoke(this);
			} while (NOscillations == 0 || ++nOscillations < NOscillations);
		}

		private Action<object> _handler;
		public void AddOutputHandler(Action<object> handler)
		{
			_handler = handler;
		}

		public VoltageSignal Output { get; private set; }
	}
}
