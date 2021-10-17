using System;

namespace PetzoldComputer
{
    public class OnesComplement8
    {
        public OnesComplement8(string name)
        {
            V = new ConnectionPoint($"{name}-onescomplement8.v");
            Invert = new ConnectionPoint($"{name}-onescomplement8.invert");
            _xor0 = new XOR($"{name}-onescomplement8.0");
            _xor1 = new XOR($"{name}-onescomplement8.1");
            _xor2 = new XOR($"{name}-onescomplement8.2");
            _xor3 = new XOR($"{name}-onescomplement8.3");
            _xor4 = new XOR($"{name}-onescomplement8.4");
            _xor5 = new XOR($"{name}-onescomplement8.5");
            _xor6 = new XOR($"{name}-onescomplement8.6");
            _xor7 = new XOR($"{name}-onescomplement8.7");

            DoWireUp();

            Components.Record(nameof(OnesComplement8));
        }

        private readonly XOR _xor0;
        private readonly XOR _xor1;
        private readonly XOR _xor2;
        private readonly XOR _xor3;
        private readonly XOR _xor4;
        private readonly XOR _xor5;
        private readonly XOR _xor6;
        private readonly XOR _xor7;

        public ConnectionPoint V { get; }
        public ConnectionPoint Invert { get; }

        public ConnectionPoint I0 => _xor0.B;
        public ConnectionPoint I1 => _xor1.B;
        public ConnectionPoint I2 => _xor2.B;
        public ConnectionPoint I3 => _xor3.B;
        public ConnectionPoint I4 => _xor4.B;
        public ConnectionPoint I5 => _xor5.B;
        public ConnectionPoint I6 => _xor6.B;
        public ConnectionPoint I7 => _xor7.B;

        public ConnectionPoint O0 => _xor0.O;
        public ConnectionPoint O1 => _xor1.O;
        public ConnectionPoint O2 => _xor2.O;
        public ConnectionPoint O3 => _xor3.O;
        public ConnectionPoint O4 => _xor4.O;
        public ConnectionPoint O5 => _xor5.O;
        public ConnectionPoint O6 => _xor6.O;
        public ConnectionPoint O7 => _xor7.O;

        // this is a case where String.Format is clearer than an interpolated string
        public override string ToString() => String.Format(
                    "{0}{1}{2}{3}{4}{5}{6}{7}",
                    _xor7.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor6.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor5.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor4.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor3.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor2.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor1.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _xor0.O.V == VoltageSignal.HIGH ? 1 : 0);

        private void DoWireUp()
        {
            V.ConnectTo(_xor7.V)
             .ConnectTo(_xor6.V)
             .ConnectTo(_xor5.V)
             .ConnectTo(_xor4.V)
             .ConnectTo(_xor3.V)
             .ConnectTo(_xor2.V)
             .ConnectTo(_xor1.V)
             .ConnectTo(_xor0.V);
            Invert.ConnectTo(_xor7.A)
                  .ConnectTo(_xor6.A)
                  .ConnectTo(_xor5.A)
                  .ConnectTo(_xor4.A)
                  .ConnectTo(_xor3.A)
                  .ConnectTo(_xor2.A)
                  .ConnectTo(_xor1.A)
                  .ConnectTo(_xor0.A);
        }
    }
}
