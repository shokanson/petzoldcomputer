namespace PetzoldComputer
{
	public interface IAddAndSub8
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A0 { get; set; }
		VoltageSignal A1 { get; set; }
		VoltageSignal A2 { get; set; }
		VoltageSignal A3 { get; set; }
		VoltageSignal A4 { get; set; }
		VoltageSignal A5 { get; set; }
		VoltageSignal A6 { get; set; }
		VoltageSignal A7 { get; set; }
		VoltageSignal B0 { get; set; }
		VoltageSignal B1 { get; set; }
		VoltageSignal B2 { get; set; }
		VoltageSignal B3 { get; set; }
		VoltageSignal B4 { get; set; }
		VoltageSignal B5 { get; set; }
		VoltageSignal B6 { get; set; }
		VoltageSignal B7 { get; set; }
		VoltageSignal Sub { get; set; }
		VoltageSignal S0 { get; }
		VoltageSignal S1 { get; }
		VoltageSignal S2 { get; }
		VoltageSignal S3 { get; }
		VoltageSignal S4 { get; }
		VoltageSignal S5 { get; }
		VoltageSignal S6 { get; }
		VoltageSignal S7 { get; }
		VoltageSignal OverUnderFlow { get; }
	}

	public interface IAnd
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal O { get; }
	}

	public interface ICounterSynchronous16
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal Clk { get; set; }
		VoltageSignal Clr { get; set; }
		VoltageSignal Q0 { get; }
		VoltageSignal Q1 { get; }
		VoltageSignal Q2 { get; }
		VoltageSignal Q3 { get; }
		VoltageSignal Q4 { get; }
		VoltageSignal Q5 { get; }
		VoltageSignal Q6 { get; }
		VoltageSignal Q7 { get; }
		VoltageSignal Q8 { get; }
		VoltageSignal Q9 { get; }
		VoltageSignal Q10 { get; }
		VoltageSignal Q11 { get; }
		VoltageSignal Q12 { get; }
		VoltageSignal Q13 { get; }
		VoltageSignal Q14 { get; }
		VoltageSignal Q15 { get; }
	}

	public interface IDFlipFlop
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal D { get; set; }
		VoltageSignal Clk { get; set; }
		VoltageSignal Q { get; }
		VoltageSignal Qnot { get; }
	}

	public interface IFullAdder
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal CarryIn { get; set; }
		VoltageSignal Sum { get; }
		VoltageSignal Carry { get; }
	}

	public interface IHalfAdder
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal Sum { get; }
		VoltageSignal Carry { get; }
	}

	public interface ILatchEdge8
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal Clk { get; set; }
		VoltageSignal D0 { get; set; }
		VoltageSignal D1 { get; set; }
		VoltageSignal D2 { get; set; }
		VoltageSignal D3 { get; set; }
		VoltageSignal D4 { get; set; }
		VoltageSignal D5 { get; set; }
		VoltageSignal D6 { get; set; }
		VoltageSignal D7 { get; set; }
		VoltageSignal Q0 { get; }
		VoltageSignal Q1 { get; }
		VoltageSignal Q2 { get; }
		VoltageSignal Q3 { get; }
		VoltageSignal Q4 { get; }
		VoltageSignal Q5 { get; }
		VoltageSignal Q6 { get; }
		VoltageSignal Q7 { get; }
	}

	public interface INand
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal O { get; }
	}

	public interface INor
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal O { get; }
	}

	public interface INor3 : INor
	{
		VoltageSignal C { get; set; }
	}

	public interface INot : IRelay
	{
		// intentionally left blank--just renaming the interface
	}

	public interface IOnesComplement8
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal Invert { get; set; }
		VoltageSignal I0 { get; set; }
		VoltageSignal I1 { get; set; }
		VoltageSignal I2 { get; set; }
		VoltageSignal I3 { get; set; }
		VoltageSignal I4 { get; set; }
		VoltageSignal I5 { get; set; }
		VoltageSignal I6 { get; set; }
		VoltageSignal I7 { get; set; }
		VoltageSignal O0 { get; }
		VoltageSignal O1 { get; }
		VoltageSignal O2 { get; }
		VoltageSignal O3 { get; }
		VoltageSignal O4 { get; }
		VoltageSignal O5 { get; }
		VoltageSignal O6 { get; }
		VoltageSignal O7 { get; }
	}

	public interface IOr
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal O { get; }
	}

	public interface IPhase1Computer
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal Clr { get; set; }
		VoltageSignal Clk { get; set; }
		VoltageSignal D0 { get; }
		VoltageSignal D1 { get; }
		VoltageSignal D2 { get; }
		VoltageSignal D3 { get; }
		VoltageSignal D4 { get; }
		VoltageSignal D5 { get; }
		VoltageSignal D6 { get; }
		VoltageSignal D7 { get; }
		void WriteByte(ushort address, byte data);
	}

	public interface IPresetAndClear
	{
		VoltageSignal Pre { get; set; }
		VoltageSignal Clr { get; set; }
	}

	public interface IRam64KB
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal Write { get; set; }
		VoltageSignal A0 { get; set; }
		VoltageSignal A1 { get; set; }
		VoltageSignal A2 { get; set; }
		VoltageSignal A3 { get; set; }
		VoltageSignal A4 { get; set; }
		VoltageSignal A5 { get; set; }
		VoltageSignal A6 { get; set; }
		VoltageSignal A7 { get; set; }
		VoltageSignal A8 { get; set; }
		VoltageSignal A9 { get; set; }
		VoltageSignal A10 { get; set; }
		VoltageSignal A11 { get; set; }
		VoltageSignal A12 { get; set; }
		VoltageSignal A13 { get; set; }
		VoltageSignal A14 { get; set; }
		VoltageSignal A15 { get; set; }
		VoltageSignal Din0 { get; set; }
		VoltageSignal Din1 { get; set; }
		VoltageSignal Din2 { get; set; }
		VoltageSignal Din3 { get; set; }
		VoltageSignal Din4 { get; set; }
		VoltageSignal Din5 { get; set; }
		VoltageSignal Din6 { get; set; }
		VoltageSignal Din7 { get; set; }
		VoltageSignal Dout0 { get; }
		VoltageSignal Dout1 { get; }
		VoltageSignal Dout2 { get; }
		VoltageSignal Dout3 { get; }
		VoltageSignal Dout4 { get; }
		VoltageSignal Dout5 { get; }
		VoltageSignal Dout6 { get; }
		VoltageSignal Dout7 { get; }
	}

	public interface IRelay
	{
		VoltageSignal Input { get; set; }
		VoltageSignal Voltage { get; set; }
		VoltageSignal Output { get; }
	}

	public interface IRippleAdder8
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A0 { get; set; }
		VoltageSignal A1 { get; set; }
		VoltageSignal A2 { get; set; }
		VoltageSignal A3 { get; set; }
		VoltageSignal A4 { get; set; }
		VoltageSignal A5 { get; set; }
		VoltageSignal A6 { get; set; }
		VoltageSignal A7 { get; set; }
		VoltageSignal B0 { get; set; }
		VoltageSignal B1 { get; set; }
		VoltageSignal B2 { get; set; }
		VoltageSignal B3 { get; set; }
		VoltageSignal B4 { get; set; }
		VoltageSignal B5 { get; set; }
		VoltageSignal B6 { get; set; }
		VoltageSignal B7 { get; set; }
		VoltageSignal CarryIn { get; set; }
		VoltageSignal S0 { get; }
		VoltageSignal S1 { get; }
		VoltageSignal S2 { get; }
		VoltageSignal S3 { get; }
		VoltageSignal S4 { get; }
		VoltageSignal S5 { get; }
		VoltageSignal S6 { get; }
		VoltageSignal S7 { get; }
		VoltageSignal Carry { get; }
	}

	public interface IXor
	{
		VoltageSignal Voltage { get; set; }
		VoltageSignal A { get; set; }
		VoltageSignal B { get; set; }
		VoltageSignal O { get; }
	}
}

/*
$Log: /PetzoldComputer/Interfaces.cs $ $NoKeyWords:$
 * 
 * 4     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
