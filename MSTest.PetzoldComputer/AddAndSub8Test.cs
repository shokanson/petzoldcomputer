using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class AddAndSub8Test
	{
		[TestMethod]
		public void TestConstructor()
		{
			IAddAndSub8 aas = new AddAndSub8();

			Assert.AreEqual(VoltageSignal.LOW, aas.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, aas.A0, "Constructor: A0");
			Assert.AreEqual(VoltageSignal.LOW, aas.A1, "Constructor: A1");
			Assert.AreEqual(VoltageSignal.LOW, aas.A2, "Constructor: A2");
			Assert.AreEqual(VoltageSignal.LOW, aas.A3, "Constructor: A3");
			Assert.AreEqual(VoltageSignal.LOW, aas.A4, "Constructor: A4");
			Assert.AreEqual(VoltageSignal.LOW, aas.A5, "Constructor: A5");
			Assert.AreEqual(VoltageSignal.LOW, aas.A6, "Constructor: A6");
			Assert.AreEqual(VoltageSignal.LOW, aas.A7, "Constructor: A7");
			Assert.AreEqual(VoltageSignal.LOW, aas.B0, "Constructor: B0");
			Assert.AreEqual(VoltageSignal.LOW, aas.B1, "Constructor: B1");
			Assert.AreEqual(VoltageSignal.LOW, aas.B2, "Constructor: B2");
			Assert.AreEqual(VoltageSignal.LOW, aas.B3, "Constructor: B3");
			Assert.AreEqual(VoltageSignal.LOW, aas.B4, "Constructor: B4");
			Assert.AreEqual(VoltageSignal.LOW, aas.B5, "Constructor: B5");
			Assert.AreEqual(VoltageSignal.LOW, aas.B6, "Constructor: B6");
			Assert.AreEqual(VoltageSignal.LOW, aas.B7, "Constructor: B7");
			Assert.AreEqual(VoltageSignal.LOW, aas.S0, "Constructor: S0");
			Assert.AreEqual(VoltageSignal.LOW, aas.S1, "Constructor: S1");
			Assert.AreEqual(VoltageSignal.LOW, aas.S2, "Constructor: S2");
			Assert.AreEqual(VoltageSignal.LOW, aas.S3, "Constructor: S3");
			Assert.AreEqual(VoltageSignal.LOW, aas.S4, "Constructor: S4");
			Assert.AreEqual(VoltageSignal.LOW, aas.S5, "Constructor: S5");
			Assert.AreEqual(VoltageSignal.LOW, aas.S6, "Constructor: S6");
			Assert.AreEqual(VoltageSignal.LOW, aas.S7, "Constructor: S7");
			Assert.AreEqual(VoltageSignal.LOW, aas.Sub, "Constructor: Sub");
			Assert.AreEqual(VoltageSignal.LOW, aas.OverUnderFlow, "Constructor: OverUnderFlow");
			Assert.AreEqual("0:00000000", aas.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestAddAndSub()
		{
			IAddAndSub8 aas = new AddAndSub8 { Voltage = VoltageSignal.HIGH };
			for (ushort a = 0; a < 0x100; ++a)
			{
				for (ushort b = 0; b < 0x100; ++b)
				{
					TestAddAndSub8Helper(aas, (byte)a, (byte)b, false);
					TestAddAndSub8Helper(aas, (byte)a, (byte)b, true);
				}
			}
		}

		private static void TestAddAndSub8Helper(IAddAndSub8 aas, byte a, byte b, bool sub)
		{
			aas.Sub = (sub ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A0 = ((a & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A1 = ((a & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A2 = ((a & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A3 = ((a & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A4 = ((a & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A5 = ((a & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A6 = ((a & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.A7 = ((a & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B0 = ((b & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B1 = ((b & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B2 = ((b & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B3 = ((b & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B4 = ((b & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B5 = ((b & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B6 = ((b & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			aas.B7 = ((b & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			bool overUnder = false;
			ushort s = sub ? (ushort)(a - b) : (ushort)(a + b);

			if (sub && b > a)
			{
				overUnder = true;
			}
			else if (!sub && (s & 0x100) != 0)
			{
				overUnder = true;
			}

			Assert.IsTrue((s & 0x01) > 0 ? (aas.S0 == VoltageSignal.HIGH) : (aas.S0 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x02) > 0 ? (aas.S1 == VoltageSignal.HIGH) : (aas.S1 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x04) > 0 ? (aas.S2 == VoltageSignal.HIGH) : (aas.S2 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x08) > 0 ? (aas.S3 == VoltageSignal.HIGH) : (aas.S3 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x10) > 0 ? (aas.S4 == VoltageSignal.HIGH) : (aas.S4 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x20) > 0 ? (aas.S5 == VoltageSignal.HIGH) : (aas.S5 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x40) > 0 ? (aas.S6 == VoltageSignal.HIGH) : (aas.S6 == VoltageSignal.LOW));
			Assert.IsTrue((s & 0x80) > 0 ? (aas.S7 == VoltageSignal.HIGH) : (aas.S7 == VoltageSignal.LOW));
			Assert.IsTrue(overUnder ? (aas.OverUnderFlow == VoltageSignal.HIGH) : (aas.OverUnderFlow == VoltageSignal.LOW));
		}
	}
}
