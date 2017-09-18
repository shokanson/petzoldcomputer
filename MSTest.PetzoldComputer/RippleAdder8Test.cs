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

			Assert.AreEqual(VoltageSignal.LOW, adder.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, adder.A0, "Constructor: A0");
			Assert.AreEqual(VoltageSignal.LOW, adder.A1, "Constructor: A1");
			Assert.AreEqual(VoltageSignal.LOW, adder.A2, "Constructor: A2");
			Assert.AreEqual(VoltageSignal.LOW, adder.A3, "Constructor: A3");
			Assert.AreEqual(VoltageSignal.LOW, adder.A4, "Constructor: A4");
			Assert.AreEqual(VoltageSignal.LOW, adder.A5, "Constructor: A5");
			Assert.AreEqual(VoltageSignal.LOW, adder.A6, "Constructor: A6");
			Assert.AreEqual(VoltageSignal.LOW, adder.A7, "Constructor: A7");
			Assert.AreEqual(VoltageSignal.LOW, adder.B0, "Constructor: B0");
			Assert.AreEqual(VoltageSignal.LOW, adder.B1, "Constructor: B1");
			Assert.AreEqual(VoltageSignal.LOW, adder.B2, "Constructor: B2");
			Assert.AreEqual(VoltageSignal.LOW, adder.B3, "Constructor: B3");
			Assert.AreEqual(VoltageSignal.LOW, adder.B4, "Constructor: B4");
			Assert.AreEqual(VoltageSignal.LOW, adder.B5, "Constructor: B5");
			Assert.AreEqual(VoltageSignal.LOW, adder.B6, "Constructor: B6");
			Assert.AreEqual(VoltageSignal.LOW, adder.B7, "Constructor: B7");
			Assert.AreEqual(VoltageSignal.LOW, adder.S0, "Constructor: S0");
			Assert.AreEqual(VoltageSignal.LOW, adder.S1, "Constructor: S1");
			Assert.AreEqual(VoltageSignal.LOW, adder.S2, "Constructor: S2");
			Assert.AreEqual(VoltageSignal.LOW, adder.S3, "Constructor: S3");
			Assert.AreEqual(VoltageSignal.LOW, adder.S4, "Constructor: S4");
			Assert.AreEqual(VoltageSignal.LOW, adder.S5, "Constructor: S5");
			Assert.AreEqual(VoltageSignal.LOW, adder.S6, "Constructor: S6");
			Assert.AreEqual(VoltageSignal.LOW, adder.S7, "Constructor: S7");
			Assert.AreEqual(VoltageSignal.LOW, adder.CarryIn, "Constructor: CarryIn");
			Assert.AreEqual(VoltageSignal.LOW, adder.Carry, "Constructor: Carry");
			Assert.AreEqual("0:00000000", adder.ToString(), "Constructor: ToString()");
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
