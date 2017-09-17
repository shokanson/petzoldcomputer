using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class RippleAdder8Test
	{
		[TestMethod]
		public void TestConstructor()
		{
			IRippleAdder8 adder = new RippleAdder8();

			Assert.AreEqual(adder.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(adder.A0, VoltageSignal.LOW, "Constructor: A0");
			Assert.AreEqual(adder.A1, VoltageSignal.LOW, "Constructor: A1");
			Assert.AreEqual(adder.A2, VoltageSignal.LOW, "Constructor: A2");
			Assert.AreEqual(adder.A3, VoltageSignal.LOW, "Constructor: A3");
			Assert.AreEqual(adder.A4, VoltageSignal.LOW, "Constructor: A4");
			Assert.AreEqual(adder.A5, VoltageSignal.LOW, "Constructor: A5");
			Assert.AreEqual(adder.A6, VoltageSignal.LOW, "Constructor: A6");
			Assert.AreEqual(adder.A7, VoltageSignal.LOW, "Constructor: A7");
			Assert.AreEqual(adder.B0, VoltageSignal.LOW, "Constructor: B0");
			Assert.AreEqual(adder.B1, VoltageSignal.LOW, "Constructor: B1");
			Assert.AreEqual(adder.B2, VoltageSignal.LOW, "Constructor: B2");
			Assert.AreEqual(adder.B3, VoltageSignal.LOW, "Constructor: B3");
			Assert.AreEqual(adder.B4, VoltageSignal.LOW, "Constructor: B4");
			Assert.AreEqual(adder.B5, VoltageSignal.LOW, "Constructor: B5");
			Assert.AreEqual(adder.B6, VoltageSignal.LOW, "Constructor: B6");
			Assert.AreEqual(adder.B7, VoltageSignal.LOW, "Constructor: B7");
			Assert.AreEqual(adder.S0, VoltageSignal.LOW, "Constructor: S0");
			Assert.AreEqual(adder.S1, VoltageSignal.LOW, "Constructor: S1");
			Assert.AreEqual(adder.S2, VoltageSignal.LOW, "Constructor: S2");
			Assert.AreEqual(adder.S3, VoltageSignal.LOW, "Constructor: S3");
			Assert.AreEqual(adder.S4, VoltageSignal.LOW, "Constructor: S4");
			Assert.AreEqual(adder.S5, VoltageSignal.LOW, "Constructor: S5");
			Assert.AreEqual(adder.S6, VoltageSignal.LOW, "Constructor: S6");
			Assert.AreEqual(adder.S7, VoltageSignal.LOW, "Constructor: S7");
			Assert.AreEqual(adder.CarryIn, VoltageSignal.LOW, "Constructor: CarryIn");
			Assert.AreEqual(adder.Carry, VoltageSignal.LOW, "Constructor: Carry");
			Assert.AreEqual(adder.ToString(), "0:00000000", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestSumAndCarry()
		{
			IRippleAdder8 adder = new RippleAdder8 { Voltage = VoltageSignal.HIGH };
			for (ushort a = 0; a < 0x100; ++a)
			{
				for (ushort b = 0; b < 0x100; ++b)
				{
					TestRippleAdder8Helper(adder, (byte)a, (byte)b, false);
					TestRippleAdder8Helper(adder, (byte)a, (byte)b, true);
				}
			}
		}

		private static void TestRippleAdder8Helper(IRippleAdder8 adder, byte a, byte b, bool carryIn)
		{
			adder.CarryIn = (carryIn ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A0 = ((a & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A1 = ((a & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A2 = ((a & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A3 = ((a & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A4 = ((a & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A5 = ((a & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A6 = ((a & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A7 = ((a & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B0 = ((b & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B1 = ((b & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B2 = ((b & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B3 = ((b & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B4 = ((b & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B5 = ((b & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B6 = ((b & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B7 = ((b & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			ushort s = (ushort)(a + b);
			if (carryIn)
				++s;

			Assert.IsTrue((s & 0x01) > 0 ? (adder.S0 == VoltageSignal.HIGH) : (adder.S0 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x02) > 0 ? (adder.S1 == VoltageSignal.HIGH) : (adder.S1 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x04) > 0 ? (adder.S2 == VoltageSignal.HIGH) : (adder.S2 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x08) > 0 ? (adder.S3 == VoltageSignal.HIGH) : (adder.S3 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x10) > 0 ? (adder.S4 == VoltageSignal.HIGH) : (adder.S4 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x20) > 0 ? (adder.S5 == VoltageSignal.HIGH) : (adder.S5 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x40) > 0 ? (adder.S6 == VoltageSignal.HIGH) : (adder.S6 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x80) > 0 ? (adder.S7 == VoltageSignal.HIGH) : (adder.S7 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x100) > 0 ? (adder.Carry == VoltageSignal.HIGH) : (adder.Carry == VoltageSignal.LOW));
		}
	}
}
