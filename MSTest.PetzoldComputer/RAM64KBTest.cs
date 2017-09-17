using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class RAM64KBTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IRam64KB ram = new RAM64KB();

			Assert.AreEqual(ram.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(ram.Write, VoltageSignal.LOW, "Constructor: Write");
			Assert.AreEqual(ram.A0, VoltageSignal.LOW, "Constructor: A0");
			Assert.AreEqual(ram.A1, VoltageSignal.LOW, "Constructor: A1");
			Assert.AreEqual(ram.A2, VoltageSignal.LOW, "Constructor: A2");
			Assert.AreEqual(ram.A3, VoltageSignal.LOW, "Constructor: A3");
			Assert.AreEqual(ram.A4, VoltageSignal.LOW, "Constructor: A4");
			Assert.AreEqual(ram.A5, VoltageSignal.LOW, "Constructor: A5");
			Assert.AreEqual(ram.A6, VoltageSignal.LOW, "Constructor: A6");
			Assert.AreEqual(ram.A7, VoltageSignal.LOW, "Constructor: A7");
			Assert.AreEqual(ram.A8, VoltageSignal.LOW, "Constructor: A8");
			Assert.AreEqual(ram.A9, VoltageSignal.LOW, "Constructor: A9");
			Assert.AreEqual(ram.A10, VoltageSignal.LOW, "Constructor: A10");
			Assert.AreEqual(ram.A11, VoltageSignal.LOW, "Constructor: A11");
			Assert.AreEqual(ram.A12, VoltageSignal.LOW, "Constructor: A12");
			Assert.AreEqual(ram.A13, VoltageSignal.LOW, "Constructor: A13");
			Assert.AreEqual(ram.A14, VoltageSignal.LOW, "Constructor: A14");
			Assert.AreEqual(ram.A15, VoltageSignal.LOW, "Constructor: A15");
			Assert.AreEqual(ram.Din0, VoltageSignal.LOW, "Constructor: Din0");
			Assert.AreEqual(ram.Din1, VoltageSignal.LOW, "Constructor: Din1");
			Assert.AreEqual(ram.Din2, VoltageSignal.LOW, "Constructor: Din2");
			Assert.AreEqual(ram.Din3, VoltageSignal.LOW, "Constructor: Din3");
			Assert.AreEqual(ram.Din4, VoltageSignal.LOW, "Constructor: Din4");
			Assert.AreEqual(ram.Din5, VoltageSignal.LOW, "Constructor: Din5");
			Assert.AreEqual(ram.Din6, VoltageSignal.LOW, "Constructor: Din6");
			Assert.AreEqual(ram.Din7, VoltageSignal.LOW, "Constructor: Din7");
			Assert.AreEqual(ram.Dout0, VoltageSignal.LOW, "Constructor: Dout0");
			Assert.AreEqual(ram.Dout1, VoltageSignal.LOW, "Constructor: Dout1");
			Assert.AreEqual(ram.Dout2, VoltageSignal.LOW, "Constructor: Dout2");
			Assert.AreEqual(ram.Dout3, VoltageSignal.LOW, "Constructor: Dout3");
			Assert.AreEqual(ram.Dout4, VoltageSignal.LOW, "Constructor: Dout4");
			Assert.AreEqual(ram.Dout5, VoltageSignal.LOW, "Constructor: Dout5");
			Assert.AreEqual(ram.Dout6, VoltageSignal.LOW, "Constructor: Dout6");
			Assert.AreEqual(ram.Dout7, VoltageSignal.LOW, "Constructor: Dout7");
		}

		[TestMethod]
		public void TestRAM()
		{
			IRam64KB ram = new RAM64KB { Voltage = VoltageSignal.HIGH };
			int address;
			byte data;
			for (address = 0, data = 0; address < 65536; ++address, ++data)
			{
				WriteByte(ram, (ushort)address, data);
			}

			for (address = 0, data = 0; address < 65536; ++address, ++data)
			{
				TestByte(ram, (ushort)address, data);
			}

			ram.Voltage = VoltageSignal.LOW;
			for (address = 0; address < 65536; ++address)
			{
				TestByte(ram, (ushort)address, 0);
			}

			ram.Voltage = VoltageSignal.HIGH;
			for (address = 0; address < 65536; ++address)
			{
				TestByte(ram, (ushort)address, 0);
			}
		}

		private static void WriteByte(IRam64KB ram, ushort address, byte data)
		{
			SetAddress(ram, address);

			ram.Din0 = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din1 = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din2 = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din3 = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din4 = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din5 = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din6 = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din7 = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			ram.Write = VoltageSignal.HIGH;
			ram.Write = VoltageSignal.LOW;
		}

		private static void TestByte(IRam64KB ram, ushort address, byte data)
		{
			SetAddress(ram, address);

			byte ramData = 0;

			ramData |= (ram.Dout0 == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			ramData |= (ram.Dout1 == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			ramData |= (ram.Dout2 == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			ramData |= (ram.Dout3 == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			ramData |= (ram.Dout4 == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			ramData |= (ram.Dout5 == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			ramData |= (ram.Dout6 == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			ramData |= (ram.Dout7 == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			Assert.AreEqual(ramData, data, "Data out");
		}

		private static void SetAddress(IRam64KB ram, ushort address)
		{
			ram.A0 = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A1 = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A2 = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A3 = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A4 = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A5 = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A6 = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A7 = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A8 = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A9 = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A10 = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A11 = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A12 = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A13 = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A14 = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A15 = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
		}
	}
}
