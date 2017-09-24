using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class RAM64KBTest
	{
		[TestMethod]
		public void RAM64KB_2_Constructor()
		{
			// arrange, act
			var ram = new RAM64KB_2("test");

			// assert
			Assert.AreEqual(VoltageSignal.LOW, ram.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, ram.Write.V, "Constructor: Write");
			Assert.AreEqual(VoltageSignal.LOW, ram.A0.V, "Constructor: A0");
			Assert.AreEqual(VoltageSignal.LOW, ram.A1.V, "Constructor: A1");
			Assert.AreEqual(VoltageSignal.LOW, ram.A2.V, "Constructor: A2");
			Assert.AreEqual(VoltageSignal.LOW, ram.A3.V, "Constructor: A3");
			Assert.AreEqual(VoltageSignal.LOW, ram.A4.V, "Constructor: A4");
			Assert.AreEqual(VoltageSignal.LOW, ram.A5.V, "Constructor: A5");
			Assert.AreEqual(VoltageSignal.LOW, ram.A6.V, "Constructor: A6");
			Assert.AreEqual(VoltageSignal.LOW, ram.A7.V, "Constructor: A7");
			Assert.AreEqual(VoltageSignal.LOW, ram.A8.V, "Constructor: A8");
			Assert.AreEqual(VoltageSignal.LOW, ram.A9.V, "Constructor: A9");
			Assert.AreEqual(VoltageSignal.LOW, ram.A10.V, "Constructor: A10");
			Assert.AreEqual(VoltageSignal.LOW, ram.A11.V, "Constructor: A11");
			Assert.AreEqual(VoltageSignal.LOW, ram.A12.V, "Constructor: A12");
			Assert.AreEqual(VoltageSignal.LOW, ram.A13.V, "Constructor: A13");
			Assert.AreEqual(VoltageSignal.LOW, ram.A14.V, "Constructor: A14");
			Assert.AreEqual(VoltageSignal.LOW, ram.A15.V, "Constructor: A15");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din0.V, "Constructor: Din0");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din1.V, "Constructor: Din1");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din2.V, "Constructor: Din2");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din3.V, "Constructor: Din3");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din4.V, "Constructor: Din4");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din5.V, "Constructor: Din5");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din6.V, "Constructor: Din6");
			Assert.AreEqual(VoltageSignal.LOW, ram.Din7.V, "Constructor: Din7");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout0.V, "Constructor: Dout0");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout1.V, "Constructor: Dout1");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout2.V, "Constructor: Dout2");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout3.V, "Constructor: Dout3");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout4.V, "Constructor: Dout4");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout5.V, "Constructor: Dout5");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout6.V, "Constructor: Dout6");
			Assert.AreEqual(VoltageSignal.LOW, ram.Dout7.V, "Constructor: Dout7");
		}

		[TestMethod]
		public void RAM()
		{
			// arrange
			var ram = new RAM64KB_2("test");
			ram.V.V = VoltageSignal.HIGH;
			int address;
			byte data;

			for (address = 0, data = 0; address < 65536; ++address, ++data)
			{
				WriteByte(ram, (ushort)address, data);
			}

			// act, assert
			for (address = 0, data = 0; address < 65536; ++address, ++data)
			{
				TestByte(ram, (ushort)address, data);
			}

			ram.V.V = VoltageSignal.LOW;
			for (address = 0; address < 65536; ++address)
			{
				TestByte(ram, (ushort)address, 0);
			}

			ram.V.V = VoltageSignal.HIGH;
			for (address = 0; address < 65536; ++address)
			{
				TestByte(ram, (ushort)address, 0);
			}
		}

		private static void WriteByte(RAM64KB_2 ram, ushort address, byte data)
		{
			SetAddress(ram, address);

			ram.Din0.V = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din1.V = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din2.V = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din3.V = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din4.V = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din5.V = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din6.V = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.Din7.V = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			ram.Write.V = VoltageSignal.HIGH;
			ram.Write.V = VoltageSignal.LOW;
		}

		private static void TestByte(RAM64KB_2 ram, ushort address, byte expected)
		{
			SetAddress(ram, address);

			byte actual = 0;

			actual |= (ram.Dout0.V == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			actual |= (ram.Dout1.V == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			actual |= (ram.Dout2.V == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			actual |= (ram.Dout3.V == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			actual |= (ram.Dout4.V == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			actual |= (ram.Dout5.V == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			actual |= (ram.Dout6.V == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			actual |= (ram.Dout7.V == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			Assert.AreEqual(expected, actual, "Data out");
		}

		private static void SetAddress(RAM64KB_2 ram, ushort address)
		{
			ram.A0.V = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A1.V = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A2.V = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A3.V = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A4.V = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A5.V = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A6.V = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A7.V = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A8.V = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A9.V = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A10.V = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A11.V = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A12.V = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A13.V = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A14.V = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ram.A15.V = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
		}
	}
}
