using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

using PC = PetzoldComputer;

namespace MSTest.PetzoldComputer
{
    [TestClass]
    public class PetzoldComputerTest
    {
        [TestMethod]
        public void Computer()
        {
            uint nBytes = 0x100;   // do not set higher than 0x10000 (64K)

            var computer = new PC.PetzoldComputer("test", nBytes);
            // turn on the computer
            computer.Voltage.V = PC.VoltageSignal.HIGH;
            // prepare to load data to RAM
            computer.Clr.V = PC.VoltageSignal.HIGH;

            LoadComputer(computer, nBytes);

            computer.OutputChanged += () => Trace.TraceInformation($"Panel Bulbs: {computer.Panel.Bulbs}; Computer Bulbs: {computer.Bulbs}");

            Trace.TraceInformation($"Panel Bulbs: {computer.Panel.Bulbs}; Computer Bulbs: {computer.Bulbs}");
            computer.Clr.V = PC.VoltageSignal.LOW;   // synchronous call
        }

        private static void LoadComputer(PC.PetzoldComputer computer, uint nBytes)
        {
            for (uint i = 0; i < nBytes; ++i)
            {
                WriteByte(computer.Panel, (ushort)i, (byte)(i + 1));
            }
        }

        public static void WriteByte(PC.ControlPanel panel, ushort address, byte data)
        {
            panel.Takeover.V = PC.VoltageSignal.HIGH;

            panel.A0_sw.V = ((address & 0x0001) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A1_sw.V = ((address & 0x0002) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A2_sw.V = ((address & 0x0004) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A3_sw.V = ((address & 0x0008) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A4_sw.V = ((address & 0x0010) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A5_sw.V = ((address & 0x0020) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A6_sw.V = ((address & 0x0040) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A7_sw.V = ((address & 0x0080) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A8_sw.V = ((address & 0x0100) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A9_sw.V = ((address & 0x0200) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A10_sw.V = ((address & 0x0400) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A11_sw.V = ((address & 0x0800) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A12_sw.V = ((address & 0x1000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A13_sw.V = ((address & 0x2000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A14_sw.V = ((address & 0x4000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.A15_sw.V = ((address & 0x8000) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);

            panel.D0_sw.V = ((data & 0x01) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D1_sw.V = ((data & 0x02) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D2_sw.V = ((data & 0x04) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D3_sw.V = ((data & 0x08) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D4_sw.V = ((data & 0x10) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D5_sw.V = ((data & 0x20) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D6_sw.V = ((data & 0x40) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);
            panel.D7_sw.V = ((data & 0x80) != 0 ? PC.VoltageSignal.HIGH : PC.VoltageSignal.LOW);

            panel.Write_sw.V = PC.VoltageSignal.HIGH;
            panel.Write_sw.V = PC.VoltageSignal.LOW;

            panel.Takeover.V = PC.VoltageSignal.LOW;
        }
    }
}
