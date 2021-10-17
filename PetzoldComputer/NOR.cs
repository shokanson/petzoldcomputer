namespace PetzoldComputer
{
    public class NOR
    {
        public NOR(string name)
        {
            _not1 = new NOT($"{name}-nor.a");
            _not2 = new NOT($"{name}-nor.b");

            DoWireUp();

            Components.Record(nameof(NOR));
        }

        private readonly NOT _not1;
        private readonly NOT _not2;

        public ConnectionPoint V => _not1.V;
        public ConnectionPoint A => _not1.Input;
        public ConnectionPoint B => _not2.Input;
        public ConnectionPoint O => _not2.Output;

        public override string ToString() => O.ToString();

        private void DoWireUp()
        {
            _not1.Output.ConnectTo(_not2.V);
        }
    }
}
