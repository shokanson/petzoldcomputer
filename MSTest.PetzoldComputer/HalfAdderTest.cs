using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class HalfAdderTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IHalfAdder halfAdder = new HalfAdder();

			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Voltage, "Constructor: V");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.A, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.B, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Sum, "Constructor: Sum");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Carry, "Constructor: Carry");
			Assert.AreEqual("Sum: LOW; Carry: LOW", halfAdder.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestSum()
		{
			IHalfAdder halfAdder = new HalfAdder { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Sum, "Sum -- A: L; B: L; Sum: L");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, halfAdder.Sum, "Sum -- A: H; B: L; Sum: H");
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Sum, "Sum -- A: H; B: H; Sum: L");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, halfAdder.Sum, "Sum -- A: L; B: H; Sum: H");
		}

		[TestMethod]
		public void TestCarry()
		{
			IHalfAdder halfAdder = new HalfAdder { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Carry, "Carry -- A: L; B: L; Carry: L");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Carry, "Carry -- A: H; B: L; Carry: L");
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, halfAdder.Carry, "Carry -- A: H; B: H; Carry: H");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Carry, "Carry -- A: L; B: H; Carry: L");
		}

		[TestMethod]
		public void TestSumEvent()
		{
			IHalfAdder halfAdder = new HalfAdder();

			TestEventsHelper helper = new TestEventsHelper((ISum)halfAdder);

			halfAdder.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; no event");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.HIGH;
			halfAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.LOW;
			halfAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestCarryEvent()
		{
			IHalfAdder halfAdder = new HalfAdder();

			TestEventsHelper helper = new TestEventsHelper((ICarry)halfAdder);

			// setup
			halfAdder.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: ^; no event");
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: v; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; no event");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: ^; no event");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: v; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.HIGH;
			halfAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.LOW;
			halfAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void HalfAdder_2_Constructor()
		{
			var halfAdder = new HalfAdder_2();

			Assert.AreEqual(VoltageSignal.LOW, halfAdder.V.V, "Constructor: V");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.A.V, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.B.V, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Sum.V, "Constructor: Sum");
			Assert.AreEqual(VoltageSignal.LOW, halfAdder.Carry.V, "Constructor: Carry");
			Assert.AreEqual("Sum: LOW; Carry: LOW", halfAdder.ToString(), "Constructor: ToString()");
		}

		[DataTestMethod]
		#region data
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		#endregion
		public void XOR_2_Sum(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
		{
			// arrage
			var halfAdder = new HalfAdder_2();

			// act
			halfAdder.V.V = voltage;
			halfAdder.A.V = a;
			halfAdder.B.V = b;

			// assert
			Assert.AreEqual(expected, halfAdder.Sum.V, $"V:{halfAdder.V}; A:{halfAdder.A}; B:{halfAdder.B}");
		}

		[DataTestMethod]
		#region data
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		#endregion
		public void HalfAdder_2_Carry(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
		{
			// arrage
			var halfAdder = new HalfAdder_2();

			// act
			halfAdder.V.V = voltage;
			halfAdder.A.V = a;
			halfAdder.B.V = b;

			// assert
			Assert.AreEqual(expected, halfAdder.Carry.V, $"V:{halfAdder.V.V}; A:{halfAdder.A.V}; B:{halfAdder.B.V}");
		}

		[TestMethod]
		public void HalfAdder_2_Sum_Event()
		{
			// arrange
			var halfAdder = new HalfAdder_2();
			halfAdder.V.V = VoltageSignal.HIGH;
			bool fired = false;
			halfAdder.Sum.Changed += _ => fired = true;

			// act, assert
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: L; B: ^; event");
			fired = false;
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: L; B: v; event");
			fired = false;
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: L; A: ^; event");
			fired = false;
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: L; A: v; event");
			fired = false;
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

			// setup
			halfAdder.A.V = VoltageSignal.HIGH;
			halfAdder.B.V = VoltageSignal.LOW;
			fired = false;
			// test
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: H; B: ^; event");
			fired = false;
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: H; B: v; event");
			fired = false;
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

			// setup
			halfAdder.A.V = VoltageSignal.LOW;
			halfAdder.B.V = VoltageSignal.HIGH;
			fired = false;
			// test
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: H; A: ^; event");
			fired = false;
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: H; A: v; event");
			fired = false;
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void HalfAdder_2_Carry_Event()
		{
			// arrange
			var halfAdder = new HalfAdder_2();
			halfAdder.V.V = VoltageSignal.HIGH;
			bool fired = false;
			halfAdder.Carry.Changed += _ => fired = true;

			// act, assert
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: ^; no event");
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: v; no event");
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: L; A: ^; no event");
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: L; A: v; no event");
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

			// setup
			halfAdder.A.V = VoltageSignal.HIGH;
			halfAdder.B.V = VoltageSignal.LOW;
			fired = false;
			// test
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: H; B: ^; event");
			fired = false;
			halfAdder.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: H; B: v; event");
			fired = false;
			halfAdder.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

			// setup
			halfAdder.A.V = VoltageSignal.LOW;
			halfAdder.B.V = VoltageSignal.HIGH;
			fired = false;
			// test
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: H; A: ^; event");
			fired = false;
			halfAdder.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: H; A: v; event");
			fired = false;
			halfAdder.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
		}
	}
}
