using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class RippleAdder8Test
	{
		[TestMethod]
		public void RippleAdder8_2_Constructor()
		{
			// arrage, act
			var adder = new RippleAdder8_2("test");

			// assert
			Assert.AreEqual(VoltageSignal.LOW, adder.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, adder.A0.V, "Constructor: A0");
			Assert.AreEqual(VoltageSignal.LOW, adder.A1.V, "Constructor: A1");
			Assert.AreEqual(VoltageSignal.LOW, adder.A2.V, "Constructor: A2");
			Assert.AreEqual(VoltageSignal.LOW, adder.A3.V, "Constructor: A3");
			Assert.AreEqual(VoltageSignal.LOW, adder.A4.V, "Constructor: A4");
			Assert.AreEqual(VoltageSignal.LOW, adder.A5.V, "Constructor: A5");
			Assert.AreEqual(VoltageSignal.LOW, adder.A6.V, "Constructor: A6");
			Assert.AreEqual(VoltageSignal.LOW, adder.A7.V, "Constructor: A7");
			Assert.AreEqual(VoltageSignal.LOW, adder.B0.V, "Constructor: B0");
			Assert.AreEqual(VoltageSignal.LOW, adder.B1.V, "Constructor: B1");
			Assert.AreEqual(VoltageSignal.LOW, adder.B2.V, "Constructor: B2");
			Assert.AreEqual(VoltageSignal.LOW, adder.B3.V, "Constructor: B3");
			Assert.AreEqual(VoltageSignal.LOW, adder.B4.V, "Constructor: B4");
			Assert.AreEqual(VoltageSignal.LOW, adder.B5.V, "Constructor: B5");
			Assert.AreEqual(VoltageSignal.LOW, adder.B6.V, "Constructor: B6");
			Assert.AreEqual(VoltageSignal.LOW, adder.B7.V, "Constructor: B7");
			Assert.AreEqual(VoltageSignal.LOW, adder.S0.V, "Constructor: S0");
			Assert.AreEqual(VoltageSignal.LOW, adder.S1.V, "Constructor: S1");
			Assert.AreEqual(VoltageSignal.LOW, adder.S2.V, "Constructor: S2");
			Assert.AreEqual(VoltageSignal.LOW, adder.S3.V, "Constructor: S3");
			Assert.AreEqual(VoltageSignal.LOW, adder.S4.V, "Constructor: S4");
			Assert.AreEqual(VoltageSignal.LOW, adder.S5.V, "Constructor: S5");
			Assert.AreEqual(VoltageSignal.LOW, adder.S6.V, "Constructor: S6");
			Assert.AreEqual(VoltageSignal.LOW, adder.S7.V, "Constructor: S7");
			Assert.AreEqual(VoltageSignal.LOW, adder.CarryIn.V, "Constructor: CarryIn");
			Assert.AreEqual(VoltageSignal.LOW, adder.Carry.V, "Constructor: Carry");
			Assert.AreEqual("0:00000000", adder.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void RippleAdder8_2_SumAndCarry()
		{
			// arrange
			var adder = new RippleAdder8_2("test");
			adder.V.V = VoltageSignal.HIGH;
			for (ushort a = 0; a < 0x100; ++a)
			{
				for (ushort b = 0; b < 0x100; ++b)
				{
					RippleAdder8_2_TestHelper(adder, (byte)a, (byte)b, false);
					RippleAdder8_2_TestHelper(adder, (byte)a, (byte)b, true);
				}
			}
		}

		private static void RippleAdder8_2_TestHelper(RippleAdder8_2 adder, byte a, byte b, bool carryIn)
		{
			adder.CarryIn.V = (carryIn ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A0.V = ((a & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A1.V = ((a & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A2.V = ((a & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A3.V = ((a & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A4.V = ((a & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A5.V = ((a & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A6.V = ((a & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.A7.V = ((a & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B0.V = ((b & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B1.V = ((b & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B2.V = ((b & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B3.V = ((b & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B4.V = ((b & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B5.V = ((b & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B6.V = ((b & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			adder.B7.V = ((b & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			ushort s = (ushort)(a + b);
			if (carryIn)
				++s;

			Assert.IsTrue((s & 0x01) > 0 ? (adder.S0.V == VoltageSignal.HIGH) : (adder.S0.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x02) > 0 ? (adder.S1.V == VoltageSignal.HIGH) : (adder.S1.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x04) > 0 ? (adder.S2.V == VoltageSignal.HIGH) : (adder.S2.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x08) > 0 ? (adder.S3.V == VoltageSignal.HIGH) : (adder.S3.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x10) > 0 ? (adder.S4.V == VoltageSignal.HIGH) : (adder.S4.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x20) > 0 ? (adder.S5.V == VoltageSignal.HIGH) : (adder.S5.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x40) > 0 ? (adder.S6.V == VoltageSignal.HIGH) : (adder.S6.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x80) > 0 ? (adder.S7.V == VoltageSignal.HIGH) : (adder.S7.V == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x100) > 0 ? (adder.Carry.V == VoltageSignal.HIGH) : (adder.Carry.V == VoltageSignal.LOW));
		}
	}
}
