using System;

namespace PetzoldComputer
{
	public class PetzoldComputer : IPetzoldComputer, IOutput
	{
		public PetzoldComputer()
			: this(0)
		{ }

		public PetzoldComputer(uint nIterations)
		{
			_computer = new Phase3Computer(nIterations);
		}

		private readonly Phase3Computer _computer;

		public VoltageSignal Voltage { get => _computer.Voltage; set => _computer.Voltage = value; }
		// transitioning Clr from HIGH to LOW starts the computer
		public VoltageSignal Clr
		{
			get => _computer.Clr;
			set
			{
				VoltageSignal origValue = _computer.Clr;
				_computer.Clr = value;
				if (origValue == VoltageSignal.HIGH && value == VoltageSignal.LOW)
					// synchrous call--returns only if oscillator has finite # iterations
					_computer.Oscillator.Start();
			}
		}
		public IControlPanel Panel => _computer.Panel;

		public VoltageSignal B0 => _computer.D0;
		public VoltageSignal B1 => _computer.D1;
		public VoltageSignal B2 => _computer.D2;
		public VoltageSignal B3 => _computer.D3;
		public VoltageSignal B4 => _computer.D4;
		public VoltageSignal B5 => _computer.D5;
		public VoltageSignal B6 => _computer.D6;
		public VoltageSignal B7 => _computer.D7;

		public string Bulbs => $"{bulb(B7)}{bulb(B6)}{bulb(B5)}{bulb(B4)}{bulb(B3)}{bulb(B2)}{bulb(B1)}{bulb(B0)}";
		private char bulb(VoltageSignal voltage) => voltage == VoltageSignal.HIGH ? 'Ȳ' : '.';

		public void AddOutputHandler(Action<object> handler) => _computer.AddOutputHandler(handler);
	}

	public class PetzoldComputer_2
	{
		public PetzoldComputer_2(string name)
			: this(name, 0)
		{ }

		public PetzoldComputer_2(string name, uint nIterations)
		{
			_computer = new Phase3Computer_2($"{name}-computer", nIterations);
			_prevClockVoltage = _computer.Clr.V;

			// transitioning Clr from HIGH to LOW starts the computer
			_computer.Clr.Changed += clr =>
			{
				_computer.Clr.V = clr.V;
				if (_prevClockVoltage == VoltageSignal.HIGH && clr.V == VoltageSignal.LOW)
				{
					// synchrous call--returns only if oscillator has finite # iterations
					_computer.Oscillator.Start();
				}
				_prevClockVoltage = _computer.Clr.V;
			};

			DoWireUp();
		}

		private readonly Phase3Computer_2 _computer;
		private VoltageSignal _prevClockVoltage;

		public ConnectionPoint Voltage => _computer.V;
		public ConnectionPoint Clr => _computer.Clr;
		public ControlPanel_2 Panel => _computer.Panel;

		public VoltageSignal B0 => _computer.D0.V;
		public VoltageSignal B1 => _computer.D1.V;
		public VoltageSignal B2 => _computer.D2.V;
		public VoltageSignal B3 => _computer.D3.V;
		public VoltageSignal B4 => _computer.D4.V;
		public VoltageSignal B5 => _computer.D5.V;
		public VoltageSignal B6 => _computer.D6.V;
		public VoltageSignal B7 => _computer.D7.V;

		public Action OutputChanged;

		public string Bulbs => $"{bulb(B7)}{bulb(B6)}{bulb(B5)}{bulb(B4)}{bulb(B3)}{bulb(B2)}{bulb(B1)}{bulb(B0)}";
		private char bulb(VoltageSignal voltage) => voltage == VoltageSignal.HIGH ? 'Ȳ' : '.';

		private void DoWireUp()
		{
			_computer.D0.Changed += _ => OutputChanged?.Invoke();
			_computer.D1.Changed += _ => OutputChanged?.Invoke();
			_computer.D2.Changed += _ => OutputChanged?.Invoke();
			_computer.D3.Changed += _ => OutputChanged?.Invoke();
			_computer.D4.Changed += _ => OutputChanged?.Invoke();
			_computer.D5.Changed += _ => OutputChanged?.Invoke();
			_computer.D6.Changed += _ => OutputChanged?.Invoke();
			_computer.D7.Changed += _ => OutputChanged?.Invoke();
		}
	}
}
