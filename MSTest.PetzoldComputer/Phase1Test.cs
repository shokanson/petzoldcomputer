using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class Phase1Test
	{
		[TestMethod]
		public void TestConstructor()
		{
			IPhase1Computer computer = new Phase1Computer();

			Assert.AreEqual(computer.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(computer.Clr, VoltageSignal.LOW, "Constructor: Clr");
			Assert.AreEqual(computer.D0, VoltageSignal.LOW, "Constructor: D0");
			Assert.AreEqual(computer.D1, VoltageSignal.LOW, "Constructor: D1");
			Assert.AreEqual(computer.D2, VoltageSignal.LOW, "Constructor: D2");
			Assert.AreEqual(computer.D3, VoltageSignal.LOW, "Constructor: D3");
			Assert.AreEqual(computer.D4, VoltageSignal.LOW, "Constructor: D4");
			Assert.AreEqual(computer.D5, VoltageSignal.LOW, "Constructor: D5");
			Assert.AreEqual(computer.D6, VoltageSignal.LOW, "Constructor: D6");
			Assert.AreEqual(computer.D7, VoltageSignal.LOW, "Constructor: D7");
			Assert.AreEqual(computer.ToString(), "00000000", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestComputer()
		{
			// Drive the computer as described on page 209...
			IPhase1Computer computer = new Phase1Computer
			{
				// turn on the computer
				Voltage = VoltageSignal.HIGH,
				// clear the latch, and set counter output to 0000h
				Clr = VoltageSignal.HIGH
			};

			int nBytes = 0x10000;	// do not set higher than 0x10000 (64K)

			LoadComputerRAM(computer, nBytes);

			// allow a clocking/oscillator input to drive the computer
			computer.Clr = VoltageSignal.LOW;

			byte expected = 1;	// expected is based on how we know we loaded the RAM above

			// have the computer burn through all 64K and do its thing
			for (uint i = 0; i < nBytes; ++i)
			{
				// mimic input from an oscillator, which quickly takes voltage high then low: _/‾\_/‾\_/‾\_/‾\_/‾\_/‾\_ ...
				computer.Clk = VoltageSignal.HIGH;
				computer.Clk = VoltageSignal.LOW;

				TestData(computer, expected);
				TestToString(computer, expected);

				expected += (byte)(i + 2);
			}
		}

		private static void TestData(IPhase1Computer computer, byte expected)
		{
			byte actual = 0x00;

			actual |= (computer.D0 == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			actual |= (computer.D1 == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			actual |= (computer.D2 == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			actual |= (computer.D3 == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			actual |= (computer.D4 == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			actual |= (computer.D5 == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			actual |= (computer.D6 == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			actual |= (computer.D7 == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			Assert.AreEqual(expected, actual, "Computer Data");
		}

		private static void TestToString(IPhase1Computer computer, byte expected)
		{
			string expectedString = Convert.ToString(expected, 2).PadLeft(8, '0'),
				actualString = computer.ToString();
			Assert.AreEqual(expectedString, actualString, "ToString()");
		}

		// This mimics operating the control panel on page 204, which Phase1Computer doesn't have
		private static void LoadComputerRAM(IPhase1Computer computer, int nBytes)
		{
			for (int i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}
	}
}
