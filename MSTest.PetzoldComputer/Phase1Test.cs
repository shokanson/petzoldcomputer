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
			IPhase1Computer computer = new Phase1Computer
			{
				Clr = VoltageSignal.HIGH,
				Voltage = VoltageSignal.HIGH
			};

			int nBytes = 0x10000;	// do not set higher than 0x10000 (64KB)

			LoadComputerRAM(computer, nBytes);

			computer.Clr = VoltageSignal.LOW;

			byte data = 1;

			for (uint i = 0; i < nBytes; ++i)
			{
				computer.Clk = VoltageSignal.HIGH;
				computer.Clk = VoltageSignal.LOW;

				TestData(computer, data);

				data += (byte)(i + 2);
			}
		}

		private static void TestData(IPhase1Computer computer, byte data)
		{
			byte computerData = 0x00;

			computerData |= (computer.D0 == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			computerData |= (computer.D1 == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			computerData |= (computer.D2 == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			computerData |= (computer.D3 == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			computerData |= (computer.D4 == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			computerData |= (computer.D5 == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			computerData |= (computer.D6 == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			computerData |= (computer.D7 == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			if (data != 0)
			{
				Assert.AreEqual(Convert.ToString(data, 2), computer.ToString().TrimStart('0'), "ToString()");
			}
			else
			{
				Assert.AreEqual("00000000", computer.ToString(), "ToString()");
			}
			Assert.AreEqual(computerData, data, "Computer Data");
		}

		private static void LoadComputerRAM(IPhase1Computer computer, int nBytes)
		{
			for (int i = 0; i < nBytes; ++i)
			{
				computer.WriteByte((ushort)i, (byte)(i + 1));
			}
		}
	}
}
