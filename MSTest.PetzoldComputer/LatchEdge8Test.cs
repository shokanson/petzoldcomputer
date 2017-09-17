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

			Assert.AreEqual(latch.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(latch.Clk, VoltageSignal.LOW, "Constructor: Clk");
			Assert.AreEqual(latch.D0, VoltageSignal.LOW, "Constructor: D0");
			Assert.AreEqual(latch.D1, VoltageSignal.LOW, "Constructor: D1");
			Assert.AreEqual(latch.D2, VoltageSignal.LOW, "Constructor: D2");
			Assert.AreEqual(latch.D3, VoltageSignal.LOW, "Constructor: D3");
			Assert.AreEqual(latch.D4, VoltageSignal.LOW, "Constructor: D4");
			Assert.AreEqual(latch.D5, VoltageSignal.LOW, "Constructor: D5");
			Assert.AreEqual(latch.D6, VoltageSignal.LOW, "Constructor: D6");
			Assert.AreEqual(latch.D7, VoltageSignal.LOW, "Constructor: D7");
			Assert.AreEqual(latch.Q0, VoltageSignal.LOW, "Constructor: Q0");
			Assert.AreEqual(latch.Q1, VoltageSignal.LOW, "Constructor: Q1");
			Assert.AreEqual(latch.Q2, VoltageSignal.LOW, "Constructor: Q2");
			Assert.AreEqual(latch.Q3, VoltageSignal.LOW, "Constructor: Q3");
			Assert.AreEqual(latch.Q4, VoltageSignal.LOW, "Constructor: Q4");
			Assert.AreEqual(latch.Q5, VoltageSignal.LOW, "Constructor: Q5");
			Assert.AreEqual(latch.Q6, VoltageSignal.LOW, "Constructor: Q6");
			Assert.AreEqual(latch.Q7, VoltageSignal.LOW, "Constructor: Q7");
			Assert.AreEqual(latch.ToString(), "00000000", "Constructor: ToString()");
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
			Assert.AreEqual(helper.EventStatus, "not fired", "D0; no event");
			latch.D1 = ((newData & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "D1; no event");
			latch.D2 = ((newData & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "D2; no event");
			latch.D3 = ((newData & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "C3; no event");
			latch.D4 = ((newData & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "D4; no event");
			latch.D5 = ((newData & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "D5; no event");
			latch.D6 = ((newData & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "D6; no event");
			latch.D7 = ((newData & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.AreEqual(helper.EventStatus, "not fired", "D7; no event");

			TestData(latch, oldData);

			byte oldOutput = GetOutput(latch);

			latch.Clk = VoltageSignal.HIGH;
			if (oldOutput != GetOutput(latch))
			{
				Assert.AreEqual(helper.EventStatus, "fired", "Clk ^ with different output; event");
				helper.ResetStatus();
			}
			else
			{
				Assert.AreEqual(helper.EventStatus, "not fired", "Clk ^ with same output; no event");
			}
			latch.Clk = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Clk v; no event");

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
