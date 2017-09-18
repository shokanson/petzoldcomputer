using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class Selector2to1Test
	{
		[TestMethod]
		public void Constructor()
		{
			// arrange, act
			ISelector2to1 selector = new Selector2to1();

			// assert
			Assert.AreEqual(selector.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(selector.A, VoltageSignal.LOW, "Constructor: A");
			Assert.AreEqual(selector.B, VoltageSignal.LOW, "Constructor: B");
			Assert.AreEqual(selector.Select, VoltageSignal.LOW, "Constructor: Select");
			Assert.AreEqual(selector.O, VoltageSignal.LOW, "Constructor: O");
			Assert.AreEqual(selector.ToString(), "LOW", "Constructor: ToString()");
		}

		[DataTestMethod]
		//                     Voltage             A                  B                  Select             expected
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		public void Output(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal select, VoltageSignal expected)
		{
			// arrange
			ISelector2to1 selector = new Selector2to1
			{
				// act
				Voltage = voltage,
				A = a,
				B = b,
				Select = select
			};

			// assert
			Assert.AreEqual(expected, selector.O);
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			// arrange
			ISelector2to1 selector = new Selector2to1();

			TestEventsHelper helper = new TestEventsHelper((IOutput)selector);

			// act,assert
			// when A selected
			selector.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			selector.A = VoltageSignal.HIGH;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: ^; B: L; Select: L; event");
			helper.ResetStatus();
			selector.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: -->H; B: L; Select: L; no event");
			selector.A = VoltageSignal.LOW;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: v; B: L; Select: L; event");
			helper.ResetStatus();

			// when B selected
			selector.Select = VoltageSignal.HIGH;
			helper.ResetStatus();
			selector.B = VoltageSignal.HIGH;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: L; B: ^; Select: H; event");
			helper.ResetStatus();
			selector.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: L; B: -->H; Select: H; no event");
			selector.B = VoltageSignal.LOW;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: L; B: v; Select: H; event");
			helper.ResetStatus();

			// when A Hi and B Lo, and selected changes
			selector.A = VoltageSignal.HIGH;
			selector.B = VoltageSignal.LOW;
			selector.Select = VoltageSignal.LOW;
			helper.ResetStatus();
			selector.Select = VoltageSignal.HIGH;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: H; B: L; Select: ^; event");
			helper.ResetStatus();
			selector.Select = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: L; Select: -->H; no event");
			selector.Select = VoltageSignal.LOW;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: H; B: L; Select: v; event");

			// when A Lo and B Hi, and selected changes
			selector.A = VoltageSignal.LOW;
			selector.B = VoltageSignal.HIGH;
			selector.Select = VoltageSignal.LOW;
			helper.ResetStatus();
			selector.Select = VoltageSignal.HIGH;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: L; B: H; Select: ^; event");
			helper.ResetStatus();
			selector.Select = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: L; B: H; Select: -->H; no event");
			selector.Select = VoltageSignal.LOW;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on --A: L; B: H; Select: v; event");

			// when A and B Lo, and selected changes
			selector.A = selector.B = VoltageSignal.LOW;
			selector.Select = VoltageSignal.LOW;
			helper.ResetStatus();
			selector.Select = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: L; B: L; Select: ^; no event");
			selector.Select = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: L; B: L; Select: v; no event");

			// when A and B Hi, and selected changes
			selector.A = selector.B = VoltageSignal.HIGH;
			selector.Select = VoltageSignal.LOW;
			helper.ResetStatus();
			selector.Select = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: H; Select: ^; no event");
			selector.Select = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: H; Select: v; no event");
		}
	}
}
