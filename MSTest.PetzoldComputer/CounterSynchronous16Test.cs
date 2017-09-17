using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class CounterSynchronous16Test
	{
		[TestMethod]
		public void TestConstructor()
		{
			ICounterSynchronous16 counter = new CounterSynchronous16();

			Assert.AreEqual(counter.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(counter.Clk, VoltageSignal.LOW, "Constructor: Clk");
			Assert.AreEqual(counter.Clr, VoltageSignal.LOW, "Constructor: Clr");
			Assert.AreEqual(counter.Q0, VoltageSignal.LOW, "Constructor: Q0");
			Assert.AreEqual(counter.Q1, VoltageSignal.LOW, "Constructor: Q1");
			Assert.AreEqual(counter.Q2, VoltageSignal.LOW, "Constructor: Q2");
			Assert.AreEqual(counter.Q3, VoltageSignal.LOW, "Constructor: Q3");
			Assert.AreEqual(counter.Q4, VoltageSignal.LOW, "Constructor: Q4");
			Assert.AreEqual(counter.Q5, VoltageSignal.LOW, "Constructor: Q5");
			Assert.AreEqual(counter.Q6, VoltageSignal.LOW, "Constructor: Q6");
			Assert.AreEqual(counter.Q7, VoltageSignal.LOW, "Constructor: Q7");
			Assert.AreEqual(counter.Q8, VoltageSignal.LOW, "Constructor: Q8");
			Assert.AreEqual(counter.Q9, VoltageSignal.LOW, "Constructor: Q9");
			Assert.AreEqual(counter.Q10, VoltageSignal.LOW, "Constructor: Q10");
			Assert.AreEqual(counter.Q11, VoltageSignal.LOW, "Constructor: Q11");
			Assert.AreEqual(counter.Q12, VoltageSignal.LOW, "Constructor: Q12");
			Assert.AreEqual(counter.Q13, VoltageSignal.LOW, "Constructor: Q13");
			Assert.AreEqual(counter.Q14, VoltageSignal.LOW, "Constructor: Q14");
			Assert.AreEqual(counter.Q15, VoltageSignal.LOW, "Constructor: Q15");
			Assert.AreEqual(counter.ToString(), "0000000000000000", "Contructor: ToString()");
		}

		[TestMethod]
		public void TestCounter()
		{
			ICounterSynchronous16 counter = new CounterSynchronous16 { Voltage = VoltageSignal.HIGH };
			for (uint i = 0; i < 0x10000; ++i)
			{
				TestCount(counter, (ushort)i);
				counter.Clk = VoltageSignal.HIGH;
				counter.Clk = VoltageSignal.LOW;
			}
			Assert.AreEqual(0x0000, GetCount(counter), "wraparound; count all 0's");
		}

		[TestMethod]
		public void TestClear()
		{
			ICounterSynchronous16 counter = new CounterSynchronous16 { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(0x0000, GetCount(counter), "Counter on; count all 0's");

			counter.Clr = VoltageSignal.HIGH;
			counter.Clk = VoltageSignal.HIGH;
			counter.Clk = VoltageSignal.LOW;
			Assert.AreEqual(0x0000, GetCount(counter), "Counter on; Clr: H; Clk: ^v; count still all 0's");

			counter.Clr = VoltageSignal.LOW;
			counter.Clk = VoltageSignal.HIGH;
			counter.Clk = VoltageSignal.LOW;
			Assert.AreEqual(0x0001, GetCount(counter), "Counter on; Clr: L; Clk: ^v; count increments");

			counter.Clr = VoltageSignal.HIGH;
			counter.Clk = VoltageSignal.HIGH;
			counter.Clk = VoltageSignal.LOW;
			Assert.AreEqual(0x0000, GetCount(counter), "Counter on; Clr: H; Clk: ^v; count still all 0's");
		}

		private static ushort GetCount(ICounterSynchronous16 counter)
		{
			ushort count = 0x00;

			count |= (counter.Q0 == VoltageSignal.HIGH ? (ushort)0x0001 : (ushort)0x00);
			count |= (counter.Q1 == VoltageSignal.HIGH ? (ushort)0x0002 : (ushort)0x00);
			count |= (counter.Q2 == VoltageSignal.HIGH ? (ushort)0x0004 : (ushort)0x00);
			count |= (counter.Q3 == VoltageSignal.HIGH ? (ushort)0x0008 : (ushort)0x00);
			count |= (counter.Q4 == VoltageSignal.HIGH ? (ushort)0x0010 : (ushort)0x00);
			count |= (counter.Q5 == VoltageSignal.HIGH ? (ushort)0x0020 : (ushort)0x00);
			count |= (counter.Q6 == VoltageSignal.HIGH ? (ushort)0x0040 : (ushort)0x00);
			count |= (counter.Q7 == VoltageSignal.HIGH ? (ushort)0x0080 : (ushort)0x00);
			count |= (counter.Q8 == VoltageSignal.HIGH ? (ushort)0x0100 : (ushort)0x00);
			count |= (counter.Q9 == VoltageSignal.HIGH ? (ushort)0x0200 : (ushort)0x00);
			count |= (counter.Q10 == VoltageSignal.HIGH ? (ushort)0x0400 : (ushort)0x00);
			count |= (counter.Q11 == VoltageSignal.HIGH ? (ushort)0x0800 : (ushort)0x00);
			count |= (counter.Q12 == VoltageSignal.HIGH ? (ushort)0x1000 : (ushort)0x00);
			count |= (counter.Q13 == VoltageSignal.HIGH ? (ushort)0x2000 : (ushort)0x00);
			count |= (counter.Q14 == VoltageSignal.HIGH ? (ushort)0x4000 : (ushort)0x00);
			count |= (counter.Q15 == VoltageSignal.HIGH ? (ushort)0x8000 : (ushort)0x00);

			return count;
		}

		private static void TestCount(ICounterSynchronous16 counter, ushort count)
		{
			Assert.IsTrue((count & 0x0001) != 0 ? (counter.Q0 == VoltageSignal.HIGH) : (counter.Q0 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0002) != 0 ? (counter.Q1 == VoltageSignal.HIGH) : (counter.Q1 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0004) != 0 ? (counter.Q2 == VoltageSignal.HIGH) : (counter.Q2 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0008) != 0 ? (counter.Q3 == VoltageSignal.HIGH) : (counter.Q3 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0010) != 0 ? (counter.Q4 == VoltageSignal.HIGH) : (counter.Q4 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0020) != 0 ? (counter.Q5 == VoltageSignal.HIGH) : (counter.Q5 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0040) != 0 ? (counter.Q6 == VoltageSignal.HIGH) : (counter.Q6 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0080) != 0 ? (counter.Q7 == VoltageSignal.HIGH) : (counter.Q7 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0100) != 0 ? (counter.Q8 == VoltageSignal.HIGH) : (counter.Q8 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0200) != 0 ? (counter.Q9 == VoltageSignal.HIGH) : (counter.Q9 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0400) != 0 ? (counter.Q10 == VoltageSignal.HIGH) : (counter.Q10 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0800) != 0 ? (counter.Q11 == VoltageSignal.HIGH) : (counter.Q11 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x1000) != 0 ? (counter.Q12 == VoltageSignal.HIGH) : (counter.Q12 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x2000) != 0 ? (counter.Q13 == VoltageSignal.HIGH) : (counter.Q13 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x4000) != 0 ? (counter.Q14 == VoltageSignal.HIGH) : (counter.Q14 == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x8000) != 0 ? (counter.Q15 == VoltageSignal.HIGH) : (counter.Q15 == VoltageSignal.LOW));
		}
	}
}
