﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System;
using System.Diagnostics;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class Phase3ComputerTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			uint nBytes = 0x100;   // do not set higher than 0x10000 (64K)

			var computer = new Phase3Computer(nBytes)
			{
				// turn on the computer
				Voltage = VoltageSignal.HIGH,
				// clear the latch, and set counter output to 0000h
				Clr = VoltageSignal.HIGH
			};

			computer.AddOutputHandler(_ => Trace.TraceInformation($"PC: {computer.PC}; Output: {computer.ToString()}; Bulbs: {computer.Panel.Bulbs}"));

			LoadComputerRAM(computer, nBytes);

			// allow the oscillator to drive the computer
			computer.Clr = VoltageSignal.LOW;

			Trace.TraceInformation($"PC: {computer.PC}; Output: {computer.ToString()}; Bulbs: {computer.Panel.Bulbs}");

			// and...go!
			computer.Oscillator.Start();  // synchronous--doesn't return until done
		}

		private void LoadComputerRAM(Phase3Computer computer, uint nBytes)
		{
			for (uint i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}

		[TestMethod]
		public void Computer()
		{
			uint nBytes = 0x100;   // do not set higher than 0x10000 (64K)

			var computer = new Phase3Computer_2("test", nBytes);
			// turn on the computer
			computer.V.V = VoltageSignal.HIGH;
			// get ready to write to RAM
			computer.Clr.V = VoltageSignal.HIGH;

			LoadComputerRAM(computer, nBytes);

			// allow the oscillator to drive the computer
			computer.Clr.V = VoltageSignal.LOW;

			// at the end of a clock cycle, show computer output
			computer.Clk.Changed += clk => { if (clk.V == VoltageSignal.LOW) Trace.TraceInformation($"PC: {computer.PC}; Output: {computer.ToString()}; Bulbs: {computer.Panel.Bulbs}"); };

			// show starting state
			Trace.TraceInformation($"PC: {computer.PC}; Output: {computer.ToString()}; Bulbs: {computer.Panel.Bulbs}");
			// and...go!
			computer.Oscillator.Start();  // synchronous--doesn't return until done
		}

		private void LoadComputerRAM(Phase3Computer_2 computer, uint nBytes)
		{
			for (uint i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}
	}
}
