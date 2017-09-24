using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class LatchEdge8Test
	{
		[TestMethod]
		public void Constructor()
		{
			// arrange, act
			var latch = new LatchEdge8("test");

			Assert.AreEqual(VoltageSignal.LOW, latch.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, latch.Clk.V, "Constructor: Clk");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din0.V, "Constructor: Din0");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din1.V, "Constructor: Din1");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din2.V, "Constructor: Din2");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din3.V, "Constructor: Din3");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din4.V, "Constructor: Din4");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din5.V, "Constructor: Din5");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din6.V, "Constructor: Din6");
			Assert.AreEqual(VoltageSignal.LOW, latch.Din7.V, "Constructor: Din7");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout0.V, "Constructor: Dout0");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout1.V, "Constructor: Dout1");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout2.V, "Constructor: Dout2");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout3.V, "Constructor: Dout3");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout4.V, "Constructor: Dout4");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout5.V, "Constructor: Dout5");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout6.V, "Constructor: Dout6");
			Assert.AreEqual(VoltageSignal.LOW, latch.Dout7.V, "Constructor: Dout7");
			Assert.AreEqual("00000000", latch.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void DataAndClock()
		{
			// arrange
			var latch = new LatchEdge8("test");
			latch.V.V = VoltageSignal.HIGH;

			// act, assert
			ushort oldData = 0;
			for (ushort data = 0; data < 0x100; ++data)
			{
				LatchEdge8_TestHelper(latch, (byte)oldData, (byte)data);

				oldData = data;
			}
		}

		[TestMethod]
		public void Preset()
		{
			// arrange
			var latch = new LatchEdge8("test");
			latch.V.V = VoltageSignal.HIGH;
			latch.Pre.V = VoltageSignal.HIGH;

			// act
			SetData(latch, 0xAA);
			latch.Clk.V = VoltageSignal.HIGH;
			latch.Clk.V = VoltageSignal.LOW;

			// assert
			TestData(latch, 0xFF);
		}

		private static void LatchEdge8_TestHelper(LatchEdge8 latch, byte oldData, byte newData)
		{
			SetData(latch, newData);

			TestData(latch, oldData);

			latch.Clk.V = VoltageSignal.HIGH;
			latch.Clk.V = VoltageSignal.LOW;

			TestData(latch, newData);
		}

		[TestMethod]
		public void Clear()
		{
			// arrange
			var latch = new LatchEdge8("test");
			latch.V.V = VoltageSignal.HIGH;
			latch.Clr.V = VoltageSignal.HIGH;

			SetData(latch, 0xAA);
			latch.Clk.V = VoltageSignal.HIGH;
			latch.Clk.V = VoltageSignal.LOW;
			TestData(latch, 0x00);
		}

		private static bool fired;
		[TestMethod]
		public void OutputEvent()
		{
			// arrange
			var latch = new LatchEdge8("test");
			latch.V.V = VoltageSignal.HIGH;

			latch.Dout0.Changed += _ => fired = true;
			latch.Dout1.Changed += _ => fired = true;
			latch.Dout2.Changed += _ => fired = true;
			latch.Dout3.Changed += _ => fired = true;
			latch.Dout4.Changed += _ => fired = true;
			latch.Dout5.Changed += _ => fired = true;
			latch.Dout6.Changed += _ => fired = true;
			latch.Dout7.Changed += _ => fired = true;

			ushort oldData = 0;
			for (ushort data = 0; data < 0x100; ++data)
			{
				LatchEdge8_Event_TestHelper(latch, (byte)oldData, (byte)data);

				oldData = data;
			}
		}

		private static void SetData(LatchEdge8 latch, byte data)
		{
			latch.Din0.V = ((data & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din1.V = ((data & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din2.V = ((data & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din3.V = ((data & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din4.V = ((data & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din5.V = ((data & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din6.V = ((data & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			latch.Din7.V = ((data & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
		}

		private static void TestData(LatchEdge8 latch, byte data)
		{
			Assert.IsTrue((latch.Dout0.V == ((data & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout1.V == ((data & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout2.V == ((data & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout3.V == ((data & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout4.V == ((data & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout5.V == ((data & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout6.V == ((data & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
			Assert.IsTrue((latch.Dout7.V == ((data & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW)));
		}

		private void LatchEdge8_Event_TestHelper(LatchEdge8 latch, byte oldData, byte newData)
		{
			latch.Din0.V = ((newData & 0x01) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din0; no event");
			latch.Din1.V = ((newData & 0x02) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din1; no event");
			latch.Din2.V = ((newData & 0x04) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din2; no event");
			latch.Din3.V = ((newData & 0x08) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din3; no event");
			latch.Din4.V = ((newData & 0x10) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din4; no event");
			latch.Din5.V = ((newData & 0x20) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din5; no event");
			latch.Din6.V = ((newData & 0x40) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din6; no event");
			latch.Din7.V = ((newData & 0x80) > 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			Assert.IsFalse(fired, "Din7; no event");

			TestData(latch, oldData);

			byte oldOutput = GetOutput(latch);

			latch.Clk.V = VoltageSignal.HIGH;
			if (oldOutput != GetOutput(latch))
			{
				Assert.IsTrue(fired, "Clk ^ with different output; event");
				fired = false;
			}
			else
			{
				Assert.IsFalse(fired, "Clk ^ with same output; no event");
			}
			latch.Clk.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Clk v; no event");

			TestData(latch, newData);
		}

		private static byte GetOutput(LatchEdge8 latch)
		{
			byte output = 0x00;

			output |= (latch.Dout0.V == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			output |= (latch.Dout1.V == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			output |= (latch.Dout2.V == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			output |= (latch.Dout3.V == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			output |= (latch.Dout4.V == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			output |= (latch.Dout5.V == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			output |= (latch.Dout6.V == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			output |= (latch.Dout7.V == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);

			return output;
		}
	}
}
