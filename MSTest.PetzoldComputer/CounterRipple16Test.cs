using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System.Diagnostics;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class CounterRipple16Test
	{
		[TestMethod]
		public void Constructor()
		{
			// arrange, act
			var counter = new CounterRipple16("test");

			// assert
			Assert.AreEqual(VoltageSignal.LOW, counter.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, counter.Clk.V, "Constructor: Clk");
			Assert.AreEqual(VoltageSignal.LOW, counter.Clr.V, "Constructor: Clr");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q0.V, "Constructor: Q0");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q1.V, "Constructor: Q1");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q2.V, "Constructor: Q2");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q3.V, "Constructor: Q3");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q4.V, "Constructor: Q4");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q5.V, "Constructor: Q5");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q6.V, "Constructor: Q6");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q7.V, "Constructor: Q7");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q8.V, "Constructor: Q8");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q9.V, "Constructor: Q9");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q10.V, "Constructor: Q10");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q11.V, "Constructor: Q11");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q12.V, "Constructor: Q12");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q13.V, "Constructor: Q13");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q14.V, "Constructor: Q14");
			Assert.AreEqual(VoltageSignal.LOW, counter.Q15.V, "Constructor: Q15");
			Assert.AreEqual("0000000000000000", counter.ToString(), "Contructor: ToString()");
		}

		[TestMethod]
		public void Counter()
		{
			// arrange
			var counter = new CounterRipple16("test");
			counter.V.V = VoltageSignal.HIGH;

			// act, assert
			for (uint i = 0; i < 0x10000; ++i)
			{
				TestCount(counter, (ushort)i);
				counter.Clk.V = VoltageSignal.HIGH;
				counter.Clk.V = VoltageSignal.LOW;
			}
			Assert.AreEqual(0x0000, GetCount(counter), "wraparound; count all 0's");
		}

		[TestMethod]
		public void Counter_driven_by_Oscillator()
		{
			var counter = new CounterRipple16("test");
			var oscillator = new Oscillator_2("test", 0x10000);
			counter.V.V = oscillator.V.V = VoltageSignal.HIGH;

			oscillator.Output.ConnectTo(counter.Clk);
			//oscillator.Output.Changed += output => { if (output.V == VoltageSignal.LOW) Trace.TraceInformation(counter.ToString()); };

			oscillator.Start();
		}

		[TestMethod]
		public void TestClear()
		{
			// arrange
			var counter = new CounterRipple16("test");
			counter.V.V = VoltageSignal.HIGH;

			// act, assert
			counter.Clr.V = VoltageSignal.HIGH;
			counter.Clk.V = VoltageSignal.HIGH;
			counter.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(0x0000, GetCount(counter), "Counter on; Clr: H; Clk: ^v; count still all 0's");

			counter.Clr.V = VoltageSignal.LOW;
			counter.Clk.V = VoltageSignal.HIGH;
			counter.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(0x0001, GetCount(counter), "Counter on; Clr: L; Clk: ^v; count increments");

			counter.Clr.V = VoltageSignal.HIGH;
			counter.Clk.V = VoltageSignal.HIGH;
			counter.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(0x0000, GetCount(counter), "Counter on; Clr: H; Clk: ^v; count still all 0's");
		}

		private static void TestCount(CounterRipple16 counter, ushort count)
		{
			Assert.IsTrue((count & 0x0001) != 0 ? (counter.Q0.V == VoltageSignal.HIGH) : (counter.Q0.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0002) != 0 ? (counter.Q1.V == VoltageSignal.HIGH) : (counter.Q1.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0004) != 0 ? (counter.Q2.V == VoltageSignal.HIGH) : (counter.Q2.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0008) != 0 ? (counter.Q3.V == VoltageSignal.HIGH) : (counter.Q3.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0010) != 0 ? (counter.Q4.V == VoltageSignal.HIGH) : (counter.Q4.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0020) != 0 ? (counter.Q5.V == VoltageSignal.HIGH) : (counter.Q5.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0040) != 0 ? (counter.Q6.V == VoltageSignal.HIGH) : (counter.Q6.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0080) != 0 ? (counter.Q7.V == VoltageSignal.HIGH) : (counter.Q7.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0100) != 0 ? (counter.Q8.V == VoltageSignal.HIGH) : (counter.Q8.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0200) != 0 ? (counter.Q9.V == VoltageSignal.HIGH) : (counter.Q9.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0400) != 0 ? (counter.Q10.V == VoltageSignal.HIGH) : (counter.Q10.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x0800) != 0 ? (counter.Q11.V == VoltageSignal.HIGH) : (counter.Q11.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x1000) != 0 ? (counter.Q12.V == VoltageSignal.HIGH) : (counter.Q12.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x2000) != 0 ? (counter.Q13.V == VoltageSignal.HIGH) : (counter.Q13.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x4000) != 0 ? (counter.Q14.V == VoltageSignal.HIGH) : (counter.Q14.V == VoltageSignal.LOW));
			Assert.IsTrue((count & 0x8000) != 0 ? (counter.Q15.V == VoltageSignal.HIGH) : (counter.Q15.V == VoltageSignal.LOW));
		}

		private static ushort GetCount(CounterRipple16 counter)
		{
			ushort count = 0x00;

			count |= (counter.Q0.V == VoltageSignal.HIGH ? (ushort)0x0001 : (ushort)0x00);
			count |= (counter.Q1.V == VoltageSignal.HIGH ? (ushort)0x0002 : (ushort)0x00);
			count |= (counter.Q2.V == VoltageSignal.HIGH ? (ushort)0x0004 : (ushort)0x00);
			count |= (counter.Q3.V == VoltageSignal.HIGH ? (ushort)0x0008 : (ushort)0x00);
			count |= (counter.Q4.V == VoltageSignal.HIGH ? (ushort)0x0010 : (ushort)0x00);
			count |= (counter.Q5.V == VoltageSignal.HIGH ? (ushort)0x0020 : (ushort)0x00);
			count |= (counter.Q6.V == VoltageSignal.HIGH ? (ushort)0x0040 : (ushort)0x00);
			count |= (counter.Q7.V == VoltageSignal.HIGH ? (ushort)0x0080 : (ushort)0x00);
			count |= (counter.Q8.V == VoltageSignal.HIGH ? (ushort)0x0100 : (ushort)0x00);
			count |= (counter.Q9.V == VoltageSignal.HIGH ? (ushort)0x0200 : (ushort)0x00);
			count |= (counter.Q10.V == VoltageSignal.HIGH ? (ushort)0x0400 : (ushort)0x00);
			count |= (counter.Q11.V == VoltageSignal.HIGH ? (ushort)0x0800 : (ushort)0x00);
			count |= (counter.Q12.V == VoltageSignal.HIGH ? (ushort)0x1000 : (ushort)0x00);
			count |= (counter.Q13.V == VoltageSignal.HIGH ? (ushort)0x2000 : (ushort)0x00);
			count |= (counter.Q14.V == VoltageSignal.HIGH ? (ushort)0x4000 : (ushort)0x00);
			count |= (counter.Q15.V == VoltageSignal.HIGH ? (ushort)0x8000 : (ushort)0x00);

			return count;
		}
	}
}
