using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class OnesComplementTest
	{
		[TestMethod]
		public void OnesComplement8_Constructor()
		{
			// arrange, act
			var ones = new OnesComplement8("test");
			               
			// assert
			Assert.AreEqual(VoltageSignal.LOW, ones.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, ones.Invert.V, "Constructor: Invert");
			Assert.AreEqual(VoltageSignal.LOW, ones.I0.V, "Constructor: I0");
			Assert.AreEqual(VoltageSignal.LOW, ones.I1.V, "Constructor: I1");
			Assert.AreEqual(VoltageSignal.LOW, ones.I2.V, "Constructor: I2");
			Assert.AreEqual(VoltageSignal.LOW, ones.I3.V, "Constructor: I3");
			Assert.AreEqual(VoltageSignal.LOW, ones.I4.V, "Constructor: I4");
			Assert.AreEqual(VoltageSignal.LOW, ones.I5.V, "Constructor: I5");
			Assert.AreEqual(VoltageSignal.LOW, ones.I6.V, "Constructor: I6");
			Assert.AreEqual(VoltageSignal.LOW, ones.I7.V, "Constructor: I7");
			Assert.AreEqual(VoltageSignal.LOW, ones.O0.V, "Constructor: O0");
			Assert.AreEqual(VoltageSignal.LOW, ones.O1.V, "Constructor: O1");
			Assert.AreEqual(VoltageSignal.LOW, ones.O2.V, "Constructor: O2");
			Assert.AreEqual(VoltageSignal.LOW, ones.O3.V, "Constructor: O3");
			Assert.AreEqual(VoltageSignal.LOW, ones.O4.V, "Constructor: O4");
			Assert.AreEqual(VoltageSignal.LOW, ones.O5.V, "Constructor: O5");
			Assert.AreEqual(VoltageSignal.LOW, ones.O6.V, "Constructor: O6");
			Assert.AreEqual(VoltageSignal.LOW, ones.O7.V, "Constructor: O7");
			Assert.AreEqual("00000000", ones.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void OnesComplement8()
		{
			// arrange
			var ones = new OnesComplement8("test");
			ones.V.V = VoltageSignal.HIGH;
			for (ushort input = 0; input < 0x100; ++input)
			{
				// act, assert
				OnesComplement8_TestHelper(ones, (byte)input, false);
				OnesComplement8_TestHelper(ones, (byte)input, true);
			}
		}

		private static void OnesComplement8_TestHelper(OnesComplement8 ones, byte input, bool invert)
		{
			ones.Invert.V = (invert ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I0.V = ((input & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I1.V = ((input & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I2.V = ((input & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I3.V = ((input & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I4.V = ((input & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I5.V = ((input & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I6.V = ((input & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			ones.I7.V = ((input & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			byte output = (invert ? (byte)~input : input);

			Assert.IsTrue((output & 0x01) > 0 ? (ones.O0.V == VoltageSignal.HIGH) : (ones.O0.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x02) > 0 ? (ones.O1.V == VoltageSignal.HIGH) : (ones.O1.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x04) > 0 ? (ones.O2.V == VoltageSignal.HIGH) : (ones.O2.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x08) > 0 ? (ones.O3.V == VoltageSignal.HIGH) : (ones.O3.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x10) > 0 ? (ones.O4.V == VoltageSignal.HIGH) : (ones.O4.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x20) > 0 ? (ones.O5.V == VoltageSignal.HIGH) : (ones.O5.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x40) > 0 ? (ones.O6.V == VoltageSignal.HIGH) : (ones.O6.V == VoltageSignal.LOW));
			Assert.IsTrue((output & 0x80) > 0 ? (ones.O7.V == VoltageSignal.HIGH) : (ones.O7.V == VoltageSignal.LOW));
		}
	}
}
