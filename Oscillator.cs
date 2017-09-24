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
			_handler += handler;
		}

		public VoltageSignal Output { get; private set; }
	}

	public class Oscillator_2
	{
		public Oscillator_2(string name)
			: this(name, 0)   // a never-ending oscillator
		{ }

		public Oscillator_2(string name, uint nOscillations)
		{
			Output = new ConnectionPoint($"{name}-oscillator");
			NOscillations = nOscillations;
		}

		public uint NOscillations { get; private set; }
		public VoltageSignal V { get; set; }
		public readonly ConnectionPoint Output;

		public void Start()
		{
			if (V == VoltageSignal.LOW) return;

			uint nOscillations = 0;
			do
			{
				Output.V = VoltageSignal.HIGH;
				Output.V = VoltageSignal.LOW;
			} while (NOscillations == 0 || ++nOscillations < NOscillations);
		}
	}
}
