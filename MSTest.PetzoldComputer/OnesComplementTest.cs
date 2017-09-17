using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class OnesComplementTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IOnesComplement8 ones = new OnesComplement8();

			Assert.AreEqual(ones.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(ones.Invert, VoltageSignal.LOW, "Constructor: Invert");
			Assert.AreEqual(ones.I0, VoltageSignal.LOW, "Constructor: I0");
			Assert.AreEqual(ones.I1, VoltageSignal.LOW, "Constructor: I1");
			Assert.AreEqual(ones.I2, VoltageSignal.LOW, "Constructor: I2");
			Assert.AreEqual(ones.I3, VoltageSignal.LOW, "Constructor: I3");
			Assert.AreEqual(ones.I4, VoltageSignal.LOW, "Constructor: I4");
			Assert.AreEqual(ones.I5, VoltageSignal.LOW, "Constructor: I5");
			Assert.AreEqual(ones.I6, VoltageSignal.LOW, "Constructor: I6");
			Assert.AreEqual(ones.I7, VoltageSignal.LOW, "Constructor: I7");
			Assert.AreEqual(ones.O0, VoltageSignal.LOW, "Constructor: O0");
			Assert.AreEqual(ones.O1, VoltageSignal.LOW, "Constructor: O1");
			Assert.AreEqual(ones.O2, VoltageSignal.LOW, "Constructor: O2");
			Assert.AreEqual(ones.O3, VoltageSignal.LOW, "Constructor: O3");
			Assert.AreEqual(ones.O4, VoltageSignal.LOW, "Constructor: O4");
			Assert.AreEqual(ones.O5, VoltageSignal.LOW, "Constructor: O5");
			Assert.AreEqual(ones.O6, VoltageSignal.LOW, "Constructor: O6");
			Assert.AreEqual(ones.O7, VoltageSignal.LOW, "Constructor: O7");
			Assert.AreEqual(ones.ToString(), "00000000", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestNoInversion()
		{
			IOnesComplement8 ones = new OnesComplement8 { Voltage = VoltageSignal.HIGH };
			for (ushort input = 0; input < 0x100; ++input)
			{
				TestOnesComplementHelper(ones, (byte)input, false);
				TestOnesComplementHelper(ones, (byte)input, true);
			}
		}

		private static void TestOnesComplementHelper(IOnesComplement8 ones, byte input, bool invert)
		{
			ones.Invert = (invert ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I0 = ((input & 0x1) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I1 = ((input & 0x2) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I2 = ((input & 0x4) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I3 = ((input & 0x8) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I4 = ((input & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I5 = ((input & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I6 = ((input & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I7 = ((input & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			byte output = (invert ? (byte)~input : input);

			Assert.IsTrue((output & 0x1) > 0 ? (ones.O0 == VoltageSignal.HIGH) : (ones.O0 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x2) > 0 ? (ones.O1 == VoltageSignal.HIGH) : (ones.O1 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x4) > 0 ? (ones.O2 == VoltageSignal.HIGH) : (ones.O2 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x8) > 0 ? (ones.O3 == VoltageSignal.HIGH) : (ones.O3 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x10) > 0 ? (ones.O4 == VoltageSignal.HIGH) : (ones.O4 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x20) > 0 ? (ones.O5 == VoltageSignal.HIGH) : (ones.O5 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x40) > 0 ? (ones.O6 == VoltageSignal.HIGH) : (ones.O6 == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x80) > 0 ? (ones.O7 == VoltageSignal.HIGH) : (ones.O7 == VoltageSignal.LOW));
		}
	}
}
