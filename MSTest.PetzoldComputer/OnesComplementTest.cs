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

			Assert.AreEqual(VoltageSignal.LOW, ones.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, ones.Invert, "Constructor: Invert");
			Assert.AreEqual(VoltageSignal.LOW, ones.I0, "Constructor: I0");
			Assert.AreEqual(VoltageSignal.LOW, ones.I1, "Constructor: I1");
			Assert.AreEqual(VoltageSignal.LOW, ones.I2, "Constructor: I2");
			Assert.AreEqual(VoltageSignal.LOW, ones.I3, "Constructor: I3");
			Assert.AreEqual(VoltageSignal.LOW, ones.I4, "Constructor: I4");
			Assert.AreEqual(VoltageSignal.LOW, ones.I5, "Constructor: I5");
			Assert.AreEqual(VoltageSignal.LOW, ones.I6, "Constructor: I6");
			Assert.AreEqual(VoltageSignal.LOW, ones.I7, "Constructor: I7");
			Assert.AreEqual(VoltageSignal.LOW, ones.O0, "Constructor: O0");
			Assert.AreEqual(VoltageSignal.LOW, ones.O1, "Constructor: O1");
			Assert.AreEqual(VoltageSignal.LOW, ones.O2, "Constructor: O2");
			Assert.AreEqual(VoltageSignal.LOW, ones.O3, "Constructor: O3");
			Assert.AreEqual(VoltageSignal.LOW, ones.O4, "Constructor: O4");
			Assert.AreEqual(VoltageSignal.LOW, ones.O5, "Constructor: O5");
			Assert.AreEqual(VoltageSignal.LOW, ones.O6, "Constructor: O6");
			Assert.AreEqual(VoltageSignal.LOW, ones.O7, "Constructor: O7");
			Assert.AreEqual("00000000", ones.ToString(), "Constructor: ToString()");
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
