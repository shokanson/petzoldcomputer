namespace PetzoldComputer
{
    public class FullAdder
    {
        public FullAdder(string name)
        {
            _halfAdder1 = new HalfAdder($"{name}-fulladder.in");
            _halfAdder2 = new HalfAdder($"{name}-fulladder.sum");
            _or = new OR($"{name}-fulladder.carry");

            DoWireUp();

            Components.Record(nameof(FullAdder));
        }

        private readonly HalfAdder _halfAdder1;
        private readonly HalfAdder _halfAdder2;
        private readonly OR _or;

        public ConnectionPoint V => _halfAdder1.V;
        public ConnectionPoint CarryIn => _halfAdder2.A;
        public ConnectionPoint A => _halfAdder1.A;
        public ConnectionPoint B => _halfAdder1.B;
        public ConnectionPoint Sum => _halfAdder2.Sum;
        public ConnectionPoint Carry => _or.O;

        public override string ToString() => $"Sum: {Sum}; Carry: {Carry}";

        private void DoWireUp()
        {
            _halfAdder1.V.ConnectTo(_halfAdder2.V).ConnectTo(_or.V);
            _halfAdder1.Sum.ConnectTo(_halfAdder2.B);
            _halfAdder1.Carry.ConnectTo(_or.B);
            _halfAdder2.Carry.ConnectTo(_or.A);
        }
    }
}
