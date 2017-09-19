﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System.Diagnostics;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class Phase2ComputerTest
	{
		[TestMethod]
		public void TestComputer()
		{
			uint nBytes = 0x100;   // do not set higher than 0x10000 (64K)

			var computer = new Phase2Computer(nBytes)
			{
				// turn on the computer
				Voltage = VoltageSignal.HIGH,
				// clear the latch, and set counter output to 0000h
				Clr = VoltageSignal.HIGH
			};

			computer.AddOutputHandler(_ => Trace.TraceInformation($"PC: {computer.PC}; Output: {computer.ToString()}"));

			LoadComputerRAM(computer, nBytes);

			// allow the oscillator to drive the computer
			computer.Clr = VoltageSignal.LOW;

			Trace.TraceInformation($"PC: {computer.PC}; Output: {computer.ToString()}");
			// and...go!
			computer.Oscillator.Start();	// synchronous--doesn't return until done
		}

		// This mimics operating the control panel on page 204, which Phase2Computer doesn't have
		private static void LoadComputerRAM(IPhase1Computer computer, uint nBytes)
		{
			for (uint i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}
	}
}
