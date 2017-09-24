using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System.Diagnostics;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class Phase2ComputerTest
	{
		[TestMethod]
		public void Computer()
		{
			uint nBytes = 0x100;   // do not set higher than 0x10000 (64K)

			var computer = new Phase2Computer("test", nBytes);
			// turn on the computer
			computer.V.V = VoltageSignal.HIGH;
			// get ready to load data
			computer.Clr.V = VoltageSignal.HIGH;

			LoadComputerRAM(computer, nBytes);

			// allow the oscillator to drive the computer
			computer.Clr.V = VoltageSignal.LOW;

			// at the end of a clock cycle, show computer output
			computer.Clk.Changed += clk => { if (clk.V == VoltageSignal.LOW) Trace.TraceInformation($"PC: {computer.PC}; Output: {computer}"); };

			// show starting state
			Trace.TraceInformation($"PC: {computer.PC}; Output: {computer}");
			// and...go!
			computer.Oscillator.Start();  // synchronous--doesn't return until done
		}

		// This mimics operating the control panel on page 204, which Phase2Computer doesn't have
		private static void LoadComputerRAM(Phase2Computer computer, uint nBytes)
		{
			for (uint i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}
	}
}
