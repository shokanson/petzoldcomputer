using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class LatchEdge8Test
	{
		[TestMethod]
		public void TestConstructor()
		{
			ILatchEdge8 latch = new LatchEdge8();

			Assert.AreEqual(VoltageSignal.LOW, latch.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, latch.Clk, "Constructor: Clk");
			Assert.AreEqual(VoltageSignal.LOW, latch.D0, "Constructor: D0");
			Assert.AreEqual(VoltageSignal.LOW, latch.D1, "Constructor: D1");
			Assert.AreEqual(VoltageSignal.LOW, latch.D2, "Constructor: D2");
			Assert.AreEqual(VoltageSignal.LOW, latch.D3, "Constructor: D3");
			Assert.AreEqual(VoltageSignal.LOW, latch.D4, "Constructor: D4");
			Assert.AreEqual(VoltageSignal.LOW, latch.D5, "Constructor: D5");
			Assert.AreEqual(VoltageSignal.LOW, latch.D6, "Constructor: D6");
			Assert.AreEqual(VoltageSignal.LOW, latch.D7, "Constructor: D7");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q0, "Constructor: Q0");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q1, "Constructor: Q1");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q2, "Constructor: Q2");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q3, "Constructor: Q3");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q4, "Constructor: Q4");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q5, "Constructor: Q5");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q6, "Constructor: Q6");
			Assert.AreEqual(VoltageSignal.LOW, latch.Q7, "Constructor: Q7");
			Assert.AreEqual("00000000", latch.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestDataAndClock()
		{
			ILatchEdge8 latch = new LatchEdge8 { Voltage = VoltageSignal.HIGH };

			ushort oldData = 0;
			for (ushort data = 0; data < 0x100; ++data)
			{
				TestLatchEdge8Helper(latch, (byte)oldData, (byte)data);

				oldData = data;
			}
		}

		[TestMethod]
		public void TestOutputEvent()
		{
			ILatchEdge8 latch = new LatchEdge8();
			TestEventsHelper helper = new TestEventsHelper((IOutput)latch);

			latch.Voltage = VoltageSignal.HIGH;
			ushort oldData = 0;
			for (ushort data = 0; data < 0x100; ++data)
			{
				TestLatchEdge8EventHelper(latch, helper, (byte)oldData, (byte)data);

				oldData = data;
			}
		}

		[TestMethod]
		public void TestPreset()
		{
			ILatchEdge8 latch = new LatchEdge8 { Voltage = VoltageSignal.HIGH };
			((IPresetAndClear)latch).Pre = VoltageSignal.HIGH;

			SetData(latch, 0xAA);
			latch.Clk = VoltageSignal.HIGH;
			latch.Clk = VoltageSignal.LOW;
			TestData(latch, 0xFF);
		}

		[TestMethod]
		public void TestClear()
		{
			ILatchEdge8 latch = new LatchEdge8 { Voltage = VoltageSignal.HIGH };
			((IPresetAndClear)latch).Clr = VoltageSignal.HIGH;

			SetData(latch, 0xAA);
			latch.Clk = VoltageSignal.HIGH;
			latch.Clk = VoltageSignal.LOW;
			TestData(latch, 0x00);
		}

		private static void TestLatchEdge8Helper(ILatchEdge8 latch, byte oldData, byte newData)
		{
			SetData(latch, newData);

			TestData(latch, oldData);

			latch.Clk = VoltageSignal.HIGH;
			latch.Clk = VoltageSignal.LOW;

			TestData(latch, newData);
		}

		private static void TestData(ILatchEdge8 latch, byte data)
		{
			Assert.IsTrue((latch.Q0 == ((data & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q1 == ((data & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q2 == ((data & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q3 == ((data & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q4 == ((data & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q5 == ((data & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q6 == ((data & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Q7 == ((data & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
		}

		private static void SetData(ILatchEdge8 latch, byte data)
		{
			latch.D0 = ((data & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D1 = ((data & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D2 = ((data & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D3 = ((data & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D4 = ((data & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D5 = ((data & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D6 = ((data & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.D7 = ((data & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
		}

		private void TestLatchEdge8EventHelper(ILatchEdge8 latch, TestEventsHelper helper, byte oldData, byte newData)
		{
			latch.D0 = ((newData & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D0; no event");
			latch.D1 = ((newData & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D1; no event");
			latch.D2 = ((newData & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D2; no event");
			latch.D3 = ((newData & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "C3; no event");
			latch.D4 = ((newData & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D4; no event");
			latch.D5 = ((newData & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D5; no event");
			latch.D6 = ((newData & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D6; no event");
			latch.D7 = ((newData & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual("not fired", helper.EventStatus, "D7; no event");

			TestData(latch, oldData);

			byte oldOutput = GetOutput(latch);

			latch.Clk = VoltageSignal.HIGH;
			if (oldOutput != GetOutput(latch))
			{
				Assert.AreEqual("fired", helper.EventStatus, "Clk ^ with different output; event");
				helper.ResetStatus();
			}
			else
			{
				Assert.AreEqual("not fired", helper.EventStatus, "Clk ^ with same output; no event");
			}
			latch.Clk = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Clk v; no event");

			TestData(latch, newData);
		}

		private static byte GetOutput(ILatchEdge8 latch)
		{
			byte output = 0x00;

			output |= (latch.Q0 == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			output |= (latch.Q1 == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			output |= (latch.Q2 == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			output |= (latch.Q3 == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			output |= (latch.Q4 == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			output |= (latch.Q5 == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			output |= (latch.Q6 == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			output |= (latch.Q7 == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			return output;
		}
	}
}
