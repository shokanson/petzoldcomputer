using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

using PC = PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class PetzoldComputerTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			uint nBytes = 0x100;   // do not set higher than 0x10000 (64K)

			PC.IPetzoldComputer computer = new PC.PetzoldComputer(nBytes)
			{
				// turn on the computer
				Voltage = PC.VoltageSignal.HIGH,
				// clear the latch, and set counter output to 0000h
				Clr = PC.VoltageSignal.HIGH
			};

			((PC.IOutput)computer).AddOutputHandler(_ => Trace.TraceInformation($"Panel Bulbs: {computer.Panel.Bulbs}; Computer Bulbs: {computer.Bulbs}"));

			LoadComputer(computer, nBytes);

			Trace.TraceInformation($"Panel Bulbs: {computer.Panel.Bulbs}; Computer Bulbs: {computer.Bulbs}");
			computer.Clr = PC.VoltageSignal.LOW;	// synchronous call
		}

		private static void LoadComputer(PC.IPetzoldComputer computer, uint nBytes)
		{
			for (uint i = 0; i < nBytes; ++i)
			{
				WriteByte(computer.Panel, (ushort)i, (byte)(i + 1));
			}
		}

		public static void WriteByte(PC.IControlPanel panel, ushort address, byte data)
		{
			panel.Takeover = PC.VoltageSignal.HIGH;

			panel.A0_sw = ((address & 0x0001) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A1_sw = ((address & 0x0002) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A2_sw = ((address & 0x0004) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A3_sw = ((address & 0x0008) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A4_sw = ((address & 0x0010) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A5_sw = ((address & 0x0020) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A6_sw = ((address & 0x0040) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A7_sw = ((address & 0x0080) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A8_sw = ((address & 0x0100) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A9_sw = ((address & 0x0200) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A10_sw = ((address & 0x0400) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A11_sw = ((address & 0x0800) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A12_sw = ((address & 0x1000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A13_sw = ((address & 0x2000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A14_sw = ((address & 0x4000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.A15_sw = ((address & 0x8000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);

			panel.D0_sw = ((data & 0x01) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D1_sw = ((data & 0x02) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D2_sw = ((data & 0x04) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D3_sw = ((data & 0x08) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D4_sw = ((data & 0x10) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D5_sw = ((data & 0x20) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D6_sw = ((data & 0x40) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
			panel.D7_sw = ((data & 0x80) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);

			panel.Write_sw = PC.VoltageSignal.HIGH;
			panel.Write_sw = PC.VoltageSignal.LOW;

			panel.Takeover = PC.VoltageSignal.LOW;
		}
	}
}
