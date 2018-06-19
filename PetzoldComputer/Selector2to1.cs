namespace PetzoldComputer
{
    public class Selector2to1
    {
        public Selector2to1(string name)
        {
            V = new ConnectionPoint($"{name}-selector.v");
            Select = new ConnectionPoint($"{name}-selector.select");
            _not = new NOT($"{name}-selector.select");
            _andA = new AND($"{name}-selector.a");
            _andB = new AND($"{name}-selector.b");
            _or = new OR($"{name}-selector.out");

            DoWireUp();

            Components.Record(nameof(Selector2to1));
        }

        private readonly NOT _not;
        private readonly AND _andA;
        private readonly AND _andB;
        private readonly OR _or;

        public ConnectionPoint V { get; }
        public ConnectionPoint Select { get; }
        public ConnectionPoint A => _andA.A;
        public ConnectionPoint B => _andB.A;
        public ConnectionPoint O => _or.O;

        public override string ToString() => O.ToString();

        private void DoWireUp()
        {
            V.ConnectTo(_or.V).ConnectTo(_andB.V).ConnectTo(_andA.V).ConnectTo(_not.V);
            Select.ConnectTo(_not.Input).ConnectTo(_andB.B);
            _not.Output.ConnectTo(_andA.B);
            _andA.O.ConnectTo(_or.A);
            _andB.O.ConnectTo(_or.B);
        }
    }
}
