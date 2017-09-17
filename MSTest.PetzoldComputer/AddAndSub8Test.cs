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

			Assert.AreEqual(aas.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(aas.A0, VoltageSignal.LOW, "Constructor: A0");
			Assert.AreEqual(aas.A1, VoltageSignal.LOW, "Constructor: A1");
			Assert.AreEqual(aas.A2, VoltageSignal.LOW, "Constructor: A2");
			Assert.AreEqual(aas.A3, VoltageSignal.LOW, "Constructor: A3");
			Assert.AreEqual(aas.A4, VoltageSignal.LOW, "Constructor: A4");
			Assert.AreEqual(aas.A5, VoltageSignal.LOW, "Constructor: A5");
			Assert.AreEqual(aas.A6, VoltageSignal.LOW, "Constructor: A6");
			Assert.AreEqual(aas.A7, VoltageSignal.LOW, "Constructor: A7");
			Assert.AreEqual(aas.B0, VoltageSignal.LOW, "Constructor: B0");
			Assert.AreEqual(aas.B1, VoltageSignal.LOW, "Constructor: B1");
			Assert.AreEqual(aas.B2, VoltageSignal.LOW, "Constructor: B2");
			Assert.AreEqual(aas.B3, VoltageSignal.LOW, "Constructor: B3");
			Assert.AreEqual(aas.B4, VoltageSignal.LOW, "Constructor: B4");
			Assert.AreEqual(aas.B5, VoltageSignal.LOW, "Constructor: B5");
			Assert.AreEqual(aas.B6, VoltageSignal.LOW, "Constructor: B6");
			Assert.AreEqual(aas.B7, VoltageSignal.LOW, "Constructor: B7");
			Assert.AreEqual(aas.S0, VoltageSignal.LOW, "Constructor: S0");
			Assert.AreEqual(aas.S1, VoltageSignal.LOW, "Constructor: S1");
			Assert.AreEqual(aas.S2, VoltageSignal.LOW, "Constructor: S2");
			Assert.AreEqual(aas.S3, VoltageSignal.LOW, "Constructor: S3");
			Assert.AreEqual(aas.S4, VoltageSignal.LOW, "Constructor: S4");
			Assert.AreEqual(aas.S5, VoltageSignal.LOW, "Constructor: S5");
			Assert.AreEqual(aas.S6, VoltageSignal.LOW, "Constructor: S6");
			Assert.AreEqual(aas.S7, VoltageSignal.LOW, "Constructor: S7");
			Assert.AreEqual(aas.Sub, VoltageSignal.LOW, "Constructor: Sub");
			Assert.AreEqual(aas.OverUnderFlow, VoltageSignal.LOW, "Constructor: OverUnderFlow");
			Assert.AreEqual(aas.ToString(), "0:00000000", "Constructor: ToString()");
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
