namespace PetzoldComputer
{
    public class CounterRipple16
    {
        public CounterRipple16(string name)
        {
            V = new ConnectionPoint($"{name}-counter16.v");
            Clr = new ConnectionPoint($"{name}-counter16.clr");

            _flop0 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q0");
            _flop1 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q1");
            _flop2 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q2");
            _flop3 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q3");
            _flop4 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q4");
            _flop5 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q5");
            _flop6 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q6");
            _flop7 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q7");
            _flop8 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q8");
            _flop9 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q9");
            _flop10 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q10");
            _flop11 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q11");
            _flop12 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q12");
            _flop13 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q13");
            _flop14 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q14");
            _flop15 = new DFlipFlopEdgeWithPresetAndClear($"{name}-counter16.q15");

            DoWireUp();

            Components.Record(nameof(CounterRipple16));
        }

        private readonly DFlipFlopEdgeWithPresetAndClear _flop0;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop1;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop2;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop3;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop4;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop5;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop6;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop7;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop8;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop9;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop10;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop11;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop12;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop13;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop14;
        private readonly DFlipFlopEdgeWithPresetAndClear _flop15;

        public ConnectionPoint V { get; }
        public ConnectionPoint Clr { get; }
        public ConnectionPoint Clk => _flop0.Clk;

        public ConnectionPoint Q0 => _flop0.Q;
        public ConnectionPoint Q1 => _flop1.Q;
        public ConnectionPoint Q2 => _flop2.Q;
        public ConnectionPoint Q3 => _flop3.Q;
        public ConnectionPoint Q4 => _flop4.Q;
        public ConnectionPoint Q5 => _flop5.Q;
        public ConnectionPoint Q6 => _flop6.Q;
        public ConnectionPoint Q7 => _flop7.Q;
        public ConnectionPoint Q8 => _flop8.Q;
        public ConnectionPoint Q9 => _flop9.Q;
        public ConnectionPoint Q10 => _flop10.Q;
        public ConnectionPoint Q11 => _flop11.Q;
        public ConnectionPoint Q12 => _flop12.Q;
        public ConnectionPoint Q13 => _flop13.Q;
        public ConnectionPoint Q14 => _flop14.Q;
        public ConnectionPoint Q15 => _flop15.Q;

        // this is a case where string.Format is clearer than an interpolated string
        public override string ToString() => string.Format(
                    "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                    _flop15.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop14.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop13.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop12.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop11.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop10.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop9.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop8.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop7.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop6.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop5.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop4.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop3.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop2.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop1.Q.V == VoltageSignal.HIGH ? 1 : 0,
                    _flop0.Q.V == VoltageSignal.HIGH ? 1 : 0);

        private void DoWireUp()
        {
            V.ConnectTo(_flop15.V)
                .ConnectTo(_flop14.V)
                .ConnectTo(_flop13.V)
                .ConnectTo(_flop12.V)
                .ConnectTo(_flop11.V)
                .ConnectTo(_flop10.V)
                .ConnectTo(_flop9.V)
                .ConnectTo(_flop8.V)
                .ConnectTo(_flop7.V)
                .ConnectTo(_flop6.V)
                .ConnectTo(_flop5.V)
                .ConnectTo(_flop4.V)
                .ConnectTo(_flop3.V)
                .ConnectTo(_flop2.V)
                .ConnectTo(_flop1.V)
                .ConnectTo(_flop0.V);

            V.Changed += v =>
            {
                // a little bump to get things started
                if (v.V == VoltageSignal.HIGH)
                {
                    Clr.V = VoltageSignal.HIGH;
                    Clr.V = VoltageSignal.LOW;
                    _flop0.D.V = _flop0.Qnot.V;
                }
            };

            Clr.ConnectTo(_flop15.Clr)
                 .ConnectTo(_flop14.Clr)
                 .ConnectTo(_flop13.Clr)
                 .ConnectTo(_flop12.Clr)
                 .ConnectTo(_flop11.Clr)
                 .ConnectTo(_flop10.Clr)
                 .ConnectTo(_flop9.Clr)
                 .ConnectTo(_flop8.Clr)
                 .ConnectTo(_flop7.Clr)
                 .ConnectTo(_flop6.Clr)
                 .ConnectTo(_flop5.Clr)
                 .ConnectTo(_flop4.Clr)
                 .ConnectTo(_flop3.Clr)
                 .ConnectTo(_flop2.Clr)
                 .ConnectTo(_flop1.Clr)
                 .ConnectTo(_flop0.Clr);

            _flop0.Qnot.ConnectTo(_flop0.D).ConnectTo(_flop1.Clk);
            _flop1.Qnot.ConnectTo(_flop1.D).ConnectTo(_flop2.Clk);
            _flop2.Qnot.ConnectTo(_flop2.D).ConnectTo(_flop3.Clk);
            _flop3.Qnot.ConnectTo(_flop3.D).ConnectTo(_flop4.Clk);
            _flop4.Qnot.ConnectTo(_flop4.D).ConnectTo(_flop5.Clk);
            _flop5.Qnot.ConnectTo(_flop5.D).ConnectTo(_flop6.Clk);
            _flop6.Qnot.ConnectTo(_flop6.D).ConnectTo(_flop7.Clk);
            _flop7.Qnot.ConnectTo(_flop7.D).ConnectTo(_flop8.Clk);
            _flop8.Qnot.ConnectTo(_flop8.D).ConnectTo(_flop9.Clk);
            _flop9.Qnot.ConnectTo(_flop9.D).ConnectTo(_flop10.Clk);
            _flop10.Qnot.ConnectTo(_flop10.D).ConnectTo(_flop11.Clk);
            _flop11.Qnot.ConnectTo(_flop11.D).ConnectTo(_flop12.Clk);
            _flop12.Qnot.ConnectTo(_flop12.D).ConnectTo(_flop13.Clk);
            _flop13.Qnot.ConnectTo(_flop13.D).ConnectTo(_flop14.Clk);
            _flop14.Qnot.ConnectTo(_flop14.D).ConnectTo(_flop15.Clk);
            _flop15.Qnot.ConnectTo(_flop15.D);
        }
    }
}
