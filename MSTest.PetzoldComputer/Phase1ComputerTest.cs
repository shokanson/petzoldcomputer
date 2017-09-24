using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class Phase1ComputerTest
	{
		[TestMethod]
		public void Constructor()
		{
			// arrange
			var computer = new Phase1Computer_2("test");

			Assert.AreEqual(VoltageSignal.LOW, computer.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, computer.Clr.V, "Constructor: Clr");
			Assert.AreEqual(VoltageSignal.LOW, computer.D0.V, "Constructor: D0");
			Assert.AreEqual(VoltageSignal.LOW, computer.D1.V, "Constructor: D1");
			Assert.AreEqual(VoltageSignal.LOW, computer.D2.V, "Constructor: D2");
			Assert.AreEqual(VoltageSignal.LOW, computer.D3.V, "Constructor: D3");
			Assert.AreEqual(VoltageSignal.LOW, computer.D4.V, "Constructor: D4");
			Assert.AreEqual(VoltageSignal.LOW, computer.D5.V, "Constructor: D5");
			Assert.AreEqual(VoltageSignal.LOW, computer.D6.V, "Constructor: D6");
			Assert.AreEqual(VoltageSignal.LOW, computer.D7.V, "Constructor: D7");
			Assert.AreEqual("00000000", computer.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void Computer()
		{
			// Drive the computer as described on page 209...
			var computer = new Phase1Computer_2("test");

			// turn on the computer
			computer.V.V = VoltageSignal.HIGH;
			// clear the latch, and set counter output to 0000h
			computer.Clr.V = VoltageSignal.HIGH;

			int nBytes = 0x100;  // do not set higher than 0x10000 (64K)

			LoadComputerRAM(computer, nBytes);

			// allow a clocking/oscillator input to drive the computer
			computer.Clr.V = VoltageSignal.LOW;

			byte expected = 1;   // expected is based on how we know we loaded the RAM above

			// have the computer burn through all 64K and do its thing
			for (uint i = 0; i < nBytes; ++i)
			{
				// mimic input from an oscillator, which quickly takes voltage high then low: _/‾\_/‾\_/‾\_/‾\_/‾\_/‾\_ ...
				computer.Clk.V = VoltageSignal.HIGH;
				computer.Clk.V = VoltageSignal.LOW;

				TestData(computer, expected);
				TestToString(computer, expected);

				expected += (byte)(i + 2);
			}
		}

		// This mimics operating the control panel on page 204, which Phase1Computer doesn't have
		private static void LoadComputerRAM(Phase1Computer_2 computer, int nBytes)
		{
			for (int i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}

		private static void TestData(Phase1Computer_2 computer, byte expected)
		{
			byte actual = 0x00;

			actual |= (computer.D0.V == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			actual |= (computer.D1.V == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			actual |= (computer.D2.V == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			actual |= (computer.D3.V == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			actual |= (computer.D4.V == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			actual |= (computer.D5.V == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			actual |= (computer.D6.V == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			actual |= (computer.D7.V == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			Assert.AreEqual(expected, actual, "Computer Data");
		}

		private static void TestToString(Phase1Computer_2 computer, byte expected)
		{
			string expectedString = Convert.ToString(expected, 2).PadLeft(8, '0');
			Assert.AreEqual(expectedString, computer.ToString(), "ToString()");
		}
	}
}
