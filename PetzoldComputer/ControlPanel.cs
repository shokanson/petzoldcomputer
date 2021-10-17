using System;

namespace PetzoldComputer
{
    public class ControlPanel
    {
        public ControlPanel(string name)
        {
            V = new ConnectionPoint($"{name}-controlpanel.v");
            Takeover = new ConnectionPoint($"{name}-controlpanel.takeover");
            _write = new Selector2to1($"{name}-controlpanel.write");

            _d0 = new Selector2to1($"{name}-controlpanel.d0");
            _d1 = new Selector2to1($"{name}-controlpanel.d1");
            _d2 = new Selector2to1($"{name}-controlpanel.d2");
            _d3 = new Selector2to1($"{name}-controlpanel.d3");
            _d4 = new Selector2to1($"{name}-controlpanel.d4");
            _d5 = new Selector2to1($"{name}-controlpanel.d5");
            _d6 = new Selector2to1($"{name}-controlpanel.d6");
            _d7 = new Selector2to1($"{name}-controlpanel.d7");

            _a0 = new Selector2to1($"{name}-controlpanel.a0");
            _a1 = new Selector2to1($"{name}-controlpanel.a1");
            _a2 = new Selector2to1($"{name}-controlpanel.a2");
            _a3 = new Selector2to1($"{name}-controlpanel.a3");
            _a4 = new Selector2to1($"{name}-controlpanel.a4");
            _a5 = new Selector2to1($"{name}-controlpanel.a5");
            _a6 = new Selector2to1($"{name}-controlpanel.a6");
            _a7 = new Selector2to1($"{name}-controlpanel.a7");
            _a8 = new Selector2to1($"{name}-controlpanel.a8");
            _a9 = new Selector2to1($"{name}-controlpanel.a9");
            _a10 = new Selector2to1($"{name}-controlpanel.a10");
            _a11 = new Selector2to1($"{name}-controlpanel.a11");
            _a12 = new Selector2to1($"{name}-controlpanel.a12");
            _a13 = new Selector2to1($"{name}-controlpanel.a13");
            _a14 = new Selector2to1($"{name}-controlpanel.a14");
            _a15 = new Selector2to1($"{name}-controlpanel.a15");

            B0 = new ConnectionPoint($"{name}-controlpanel.b0");
            B1 = new ConnectionPoint($"{name}-controlpanel.b1");
            B2 = new ConnectionPoint($"{name}-controlpanel.b2");
            B3 = new ConnectionPoint($"{name}-controlpanel.b3");
            B4 = new ConnectionPoint($"{name}-controlpanel.b4");
            B5 = new ConnectionPoint($"{name}-controlpanel.b5");
            B6 = new ConnectionPoint($"{name}-controlpanel.b6");
            B7 = new ConnectionPoint($"{name}-controlpanel.b7");

            DoWireUp();

            Components.Record(nameof(ControlPanel));
        }

        private readonly Selector2to1 _write;

        private readonly Selector2to1 _d0;
        private readonly Selector2to1 _d1;
        private readonly Selector2to1 _d2;
        private readonly Selector2to1 _d3;
        private readonly Selector2to1 _d4;
        private readonly Selector2to1 _d5;
        private readonly Selector2to1 _d6;
        private readonly Selector2to1 _d7;

        private readonly Selector2to1 _a0;
        private readonly Selector2to1 _a1;
        private readonly Selector2to1 _a2;
        private readonly Selector2to1 _a3;
        private readonly Selector2to1 _a4;
        private readonly Selector2to1 _a5;
        private readonly Selector2to1 _a6;
        private readonly Selector2to1 _a7;
        private readonly Selector2to1 _a8;
        private readonly Selector2to1 _a9;
        private readonly Selector2to1 _a10;
        private readonly Selector2to1 _a11;
        private readonly Selector2to1 _a12;
        private readonly Selector2to1 _a13;
        private readonly Selector2to1 _a14;
        private readonly Selector2to1 _a15;

        public ConnectionPoint V { get; }

        public ConnectionPoint Takeover { get; }

        public ConnectionPoint D0_in => _d0.A;
        public ConnectionPoint D1_in => _d1.A;
        public ConnectionPoint D2_in => _d2.A;
        public ConnectionPoint D3_in => _d3.A;
        public ConnectionPoint D4_in => _d4.A;
        public ConnectionPoint D5_in => _d5.A;
        public ConnectionPoint D6_in => _d6.A;
        public ConnectionPoint D7_in => _d7.A;

        public ConnectionPoint A0_in => _a0.A;
        public ConnectionPoint A1_in => _a1.A;
        public ConnectionPoint A2_in => _a2.A;
        public ConnectionPoint A3_in => _a3.A;
        public ConnectionPoint A4_in => _a4.A;
        public ConnectionPoint A5_in => _a5.A;
        public ConnectionPoint A6_in => _a6.A;
        public ConnectionPoint A7_in => _a7.A;
        public ConnectionPoint A8_in => _a8.A;
        public ConnectionPoint A9_in => _a9.A;
        public ConnectionPoint A10_in => _a10.A;
        public ConnectionPoint A11_in => _a11.A;
        public ConnectionPoint A12_in => _a12.A;
        public ConnectionPoint A13_in => _a13.A;
        public ConnectionPoint A14_in => _a14.A;
        public ConnectionPoint A15_in => _a15.A;

        public ConnectionPoint Write_in => _write.A;

        public ConnectionPoint D0_sw => _d0.B;
        public ConnectionPoint D1_sw => _d1.B;
        public ConnectionPoint D2_sw => _d2.B;
        public ConnectionPoint D3_sw => _d3.B;
        public ConnectionPoint D4_sw => _d4.B;
        public ConnectionPoint D5_sw => _d5.B;
        public ConnectionPoint D6_sw => _d6.B;
        public ConnectionPoint D7_sw => _d7.B;

        public ConnectionPoint A0_sw => _a0.B;
        public ConnectionPoint A1_sw => _a1.B;
        public ConnectionPoint A2_sw => _a2.B;
        public ConnectionPoint A3_sw => _a3.B;
        public ConnectionPoint A4_sw => _a4.B;
        public ConnectionPoint A5_sw => _a5.B;
        public ConnectionPoint A6_sw => _a6.B;
        public ConnectionPoint A7_sw => _a7.B;
        public ConnectionPoint A8_sw => _a8.B;
        public ConnectionPoint A9_sw => _a9.B;
        public ConnectionPoint A10_sw => _a10.B;
        public ConnectionPoint A11_sw => _a11.B;
        public ConnectionPoint A12_sw => _a12.B;
        public ConnectionPoint A13_sw => _a13.B;
        public ConnectionPoint A14_sw => _a14.B;
        public ConnectionPoint A15_sw => _a15.B;

        public ConnectionPoint Write_sw => _write.B;

        public ConnectionPoint D0 => _d0.O;
        public ConnectionPoint D1 => _d1.O;
        public ConnectionPoint D2 => _d2.O;
        public ConnectionPoint D3 => _d3.O;
        public ConnectionPoint D4 => _d4.O;
        public ConnectionPoint D5 => _d5.O;
        public ConnectionPoint D6 => _d6.O;
        public ConnectionPoint D7 => _d7.O;

        public ConnectionPoint A0 => _a0.O;
        public ConnectionPoint A1 => _a1.O;
        public ConnectionPoint A2 => _a2.O;
        public ConnectionPoint A3 => _a3.O;
        public ConnectionPoint A4 => _a4.O;
        public ConnectionPoint A5 => _a5.O;
        public ConnectionPoint A6 => _a6.O;
        public ConnectionPoint A7 => _a7.O;
        public ConnectionPoint A8 => _a8.O;
        public ConnectionPoint A9 => _a9.O;
        public ConnectionPoint A10 => _a10.O;
        public ConnectionPoint A11 => _a11.O;
        public ConnectionPoint A12 => _a12.O;
        public ConnectionPoint A13 => _a13.O;
        public ConnectionPoint A14 => _a14.O;
        public ConnectionPoint A15 => _a15.O;

        public ConnectionPoint Write => _write.O;

        public ConnectionPoint B0 { get; }
        public ConnectionPoint B1 { get; }
        public ConnectionPoint B2 { get; }
        public ConnectionPoint B3 { get; }
        public ConnectionPoint B4 { get; }
        public ConnectionPoint B5 { get; }
        public ConnectionPoint B6 { get; }
        public ConnectionPoint B7 { get; }

        public string Bulbs => $"{Bulb(B7)}{Bulb(B6)}{Bulb(B5)}{Bulb(B4)}{Bulb(B3)}{Bulb(B2)}{Bulb(B1)}{Bulb(B0)}";
        private static char Bulb(ConnectionPoint voltage) => voltage.V == VoltageSignal.HIGH ? 'Ȳ' : '.';

        // this is a case where String.Format is clearer than an interpolated string
        public override string ToString() => String.Format(
                    //         W   A15 ---------------------------------------------- A0   D7 ------------------------ D0
                    "{0}:{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}:{17}{18}{19}{20}{21}{22}{23}{24}",
                    _write.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a15.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a14.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a13.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a12.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a11.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a10.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a9.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a8.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a7.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a6.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a5.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a4.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a3.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a2.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a1.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _a0.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d7.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d6.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d5.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d4.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d3.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d2.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d1.O.V == VoltageSignal.HIGH ? 1 : 0,
                    _d0.O.V == VoltageSignal.HIGH ? 1 : 0);

        private void DoWireUp()
        {
            V.ConnectTo(_d0.V).ConnectTo(_d1.V).ConnectTo(_d2.V).ConnectTo(_d3.V)
             .ConnectTo(_d4.V).ConnectTo(_d5.V).ConnectTo(_d6.V).ConnectTo(_d7.V)
             .ConnectTo(_a0.V).ConnectTo(_a1.V).ConnectTo(_a2.V).ConnectTo(_a3.V)
             .ConnectTo(_a4.V).ConnectTo(_a5.V).ConnectTo(_a6.V).ConnectTo(_a7.V)
             .ConnectTo(_a8.V).ConnectTo(_a9.V).ConnectTo(_a10.V).ConnectTo(_a11.V)
             .ConnectTo(_a12.V).ConnectTo(_a13.V).ConnectTo(_a14.V).ConnectTo(_a15.V)
             .ConnectTo(_write.V);

            Takeover.ConnectTo(_d0.Select).ConnectTo(_d1.Select).ConnectTo(_d2.Select).ConnectTo(_d3.Select)
                    .ConnectTo(_d4.Select).ConnectTo(_d5.Select).ConnectTo(_d6.Select).ConnectTo(_d7.Select)
                    .ConnectTo(_a0.Select).ConnectTo(_a1.Select).ConnectTo(_a2.Select).ConnectTo(_a3.Select)
                    .ConnectTo(_a4.Select).ConnectTo(_a5.Select).ConnectTo(_a6.Select).ConnectTo(_a7.Select)
                    .ConnectTo(_a8.Select).ConnectTo(_a9.Select).ConnectTo(_a10.Select).ConnectTo(_a11.Select)
                    .ConnectTo(_a12.Select).ConnectTo(_a13.Select).ConnectTo(_a14.Select).ConnectTo(_a15.Select)
                    .ConnectTo(_write.Select);
        }
    }
}
