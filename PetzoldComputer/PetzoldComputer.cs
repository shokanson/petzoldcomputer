using System;

namespace PetzoldComputer
{
    public class PetzoldComputer
    {
        public PetzoldComputer(string name)
            : this(name, 0)
        { }

        public PetzoldComputer(string name, uint nIterations)
        {
            _computer = new Phase3Computer($"{name}-computer", nIterations);
            _prevClockVoltage = _computer.Clr.V;

            // transitioning Clr from HIGH to LOW starts the computer
            _computer.Clr.Changed += clr =>
            {
                if (_prevClockVoltage == VoltageSignal.HIGH && clr.V == VoltageSignal.LOW)
                {
                    // synchronous call--returns only if oscillator has finite # iterations
                    _computer.Oscillator.Start();
                }
                _prevClockVoltage = _computer.Clr.V;
            };

            DoWireUp();

            Components.Record(nameof(PetzoldComputer));
        }

        private readonly Phase3Computer _computer;
        private VoltageSignal _prevClockVoltage;

        public ConnectionPoint Voltage => _computer.V;
        public ConnectionPoint Clr => _computer.Clr;
        public ControlPanel Panel => _computer.Panel;

        public VoltageSignal B0 => _computer.D0.V;
        public VoltageSignal B1 => _computer.D1.V;
        public VoltageSignal B2 => _computer.D2.V;
        public VoltageSignal B3 => _computer.D3.V;
        public VoltageSignal B4 => _computer.D4.V;
        public VoltageSignal B5 => _computer.D5.V;
        public VoltageSignal B6 => _computer.D6.V;
        public VoltageSignal B7 => _computer.D7.V;

        public Action OutputChanged;

        public string Bulbs => $"{Bulb(B7)}{Bulb(B6)}{Bulb(B5)}{Bulb(B4)}{Bulb(B3)}{Bulb(B2)}{Bulb(B1)}{Bulb(B0)}";
        private static char Bulb(VoltageSignal voltage) => voltage == VoltageSignal.HIGH ? 'Ȳ' : '.';

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
