using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class DFlipFlopEdgeWithPresetAndClearTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IDFlipFlop flip = new DFlipFlopEdgeWithPresetAndClear();
			IPresetAndClear flipPC = (IPresetAndClear)flip;

			Assert.AreEqual(VoltageSignal.LOW, flip.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, flip.D, "Constructor: D");
			Assert.AreEqual(VoltageSignal.LOW, flip.Clk, "Constructor: Clk");
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Constructor: Q");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Constructor: Qnot");
			Assert.AreEqual(VoltageSignal.LOW, flipPC.Pre, "Constructor: Pre");
			Assert.AreEqual(VoltageSignal.LOW, flipPC.Clr, "Constructor: Clr");
			Assert.AreEqual("Q: LOW; Qnot: LOW", flip.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestClockAndData()
		{
			IDFlipFlop flip = new DFlipFlopEdgeWithPresetAndClear();

			flip.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Qnot H");

			flip.Clk = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: D: L; Clk: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: D: L; Clk: ^: Qnot H");

			flip.Clk = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: D: L; Clk: v: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: D: L; Clk: v: Qnot H");

			flip.D = VoltageSignal.HIGH;
			VoltageSignal oldQ = flip.Q, oldQnot = flip.Qnot;
			Assert.AreEqual(oldQ, flip.Q, "Gate on: D: ^; Clk: L: Q Q");
			Assert.AreEqual(oldQnot, flip.Qnot, "Gate on: D: ^; Clk: L: Qnot Qnot");

			flip.Clk = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: D: H; Clk: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: D: H; Clk: ^: Qnot L");

			flip.Clk = VoltageSignal.LOW;
			oldQ = flip.Q;
			oldQnot = flip.Qnot;
			Assert.AreEqual(oldQ, flip.Q, "Gate on: D: H; Clk: v: Q Q");
			Assert.AreEqual(oldQnot, flip.Qnot, "Gate on: D: H; Clk: v: Qnot Qnot");

			flip.D = VoltageSignal.LOW;
			flip.Clk = VoltageSignal.HIGH;
			flip.D = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: D: L; Clk: ^: D: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: D: L; Clk: ^: D: ^: Qnot H");

			flip.Clk = VoltageSignal.LOW;
			flip.Clk = VoltageSignal.HIGH;
			flip.D = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: D: H; Clk: ^: D: v; Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: D: H; Clk: ^: D: v; Qnot L");
		}

		[TestMethod]
		public void TestPreset()
		{
			IDFlipFlop flip = new DFlipFlopEdgeWithPresetAndClear();
			IPresetAndClear flipPC = (IPresetAndClear)flip;

			flip.Voltage = VoltageSignal.HIGH;

			flipPC.Pre = VoltageSignal.HIGH;
			// at this point, nothing we do should change Q or Qnot			

			flip.Clk = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: L; Clk: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: L; Clk: ^: Qnot L");

			flip.Clk = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: L; Clk: v: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: L; Clk: v: Qnot L");

			flip.D = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: ^; Clk: L: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: ^; Clk: L: Qnot L");

			flip.Clk = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: H; Clk: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: H; Clk: ^: Qnot L");

			flip.Clk = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: H; Clk: v: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: H; Clk: v: Qnot L");

			flip.D = VoltageSignal.LOW;
			flip.Clk = VoltageSignal.HIGH;
			flip.D = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: L; Clk: ^: D: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: L; Clk: ^: D: ^: Qnot L");

			flip.Clk = VoltageSignal.LOW;
			flip.Clk = VoltageSignal.HIGH;
			flip.D = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q, "Gate on: Pre: H; D: H; Clk: ^: D: v; Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot, "Gate on: Pre: H; D: H; Clk: ^: D: v; Qnot L");
		}

		[TestMethod]
		public void TestClear()
		{
			IDFlipFlop flip = new DFlipFlopEdgeWithPresetAndClear();
			IPresetAndClear flipPC = (IPresetAndClear)flip;

			flip.Voltage = VoltageSignal.HIGH;

			flipPC.Clr = VoltageSignal.HIGH;
			// at this point, nothing we do should change Q or Qnot			

			flip.Clk = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: L; Clk: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: L; Clk: ^: Qnot H");

			flip.Clk = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: L; Clk: v: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: L; Clk: v: Qnot H");

			flip.D = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: ^; Clk: L: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: ^; Clk: L: Qnot H");

			flip.Clk = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: H; Clk: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: H; Clk: ^: Qnot H");

			flip.Clk = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: H; Clk: v: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: H; Clk: v: Qnot H");

			flip.D = VoltageSignal.LOW;
			flip.Clk = VoltageSignal.HIGH;
			flip.D = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: L; Clk: ^: D: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: L; Clk: ^: D: ^: Qnot H");

			flip.Clk = VoltageSignal.LOW;
			flip.Clk = VoltageSignal.HIGH;
			flip.D = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q, "Gate on: Clr: H; D: H; Clk: ^: D: v; Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot, "Gate on: Clr: H; D: H; Clk: ^: D: v; Qnot H");
		}

		[TestMethod]
		public void DFlipFlopEdgeWithPresetAndClear_2_Constructor()
		{
			// arrange, act
			var flip = new DFlipFlopEdgeWithPresetAndClear_2("test");

			// assert
			Assert.AreEqual(VoltageSignal.LOW, flip.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, flip.Clr.V, "Constructor: Clr");
			Assert.AreEqual(VoltageSignal.LOW, flip.Pre.V, "Constructor: Pre");
			Assert.AreEqual(VoltageSignal.LOW, flip.Clk.V, "Constructor: Clk");
			Assert.AreEqual(VoltageSignal.LOW, flip.D.V, "Constructor: D");
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Constructor: Q");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Constructor: Qnot");
			Assert.AreEqual("Q: LOW; Qnot: LOW", flip.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void ClockAndData()
		{
			// arrange
			var flip = new DFlipFlopEdgeWithPresetAndClear_2("test");

			// act, assert
			flip.V.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Qnot H");

			flip.Clk.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: D: L; Clk: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: D: L; Clk: ^: Qnot H");

			flip.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: D: L; Clk: v: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: D: L; Clk: v: Qnot H");

			flip.D.V = VoltageSignal.HIGH;
			VoltageSignal oldQ = flip.Q.V, oldQnot = flip.Qnot.V;
			Assert.AreEqual(oldQ, flip.Q.V, "Gate on: D: ^; Clk: L: Q Q");
			Assert.AreEqual(oldQnot, flip.Qnot.V, "Gate on: D: ^; Clk: L: Qnot Qnot");

			flip.Clk.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: D: H; Clk: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: D: H; Clk: ^: Qnot L");

			flip.Clk.V = VoltageSignal.LOW;
			oldQ = flip.Q.V;
			oldQnot = flip.Qnot.V;
			Assert.AreEqual(oldQ, flip.Q.V, "Gate on: D: H; Clk: v: Q Q");
			Assert.AreEqual(oldQnot, flip.Qnot.V, "Gate on: D: H; Clk: v: Qnot Qnot");

			flip.D.V = VoltageSignal.LOW;
			flip.Clk.V = VoltageSignal.HIGH;
			flip.D.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: D: L; Clk: ^: D: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: D: L; Clk: ^: D: ^: Qnot H");

			flip.Clk.V = VoltageSignal.LOW;
			flip.Clk.V = VoltageSignal.HIGH;
			flip.D.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: D: H; Clk: ^: D: v; Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: D: H; Clk: ^: D: v; Qnot L");
		}

		[TestMethod]
		public void Preset()
		{
			// arrange
			var flip = new DFlipFlopEdgeWithPresetAndClear_2("test");
			flip.V.V = VoltageSignal.HIGH;
			flip.Pre.V = VoltageSignal.HIGH;
			// at this point, nothing we do should change Q or Qnot			

			// act, assert
			flip.Clk.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: L; Clk: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: L; Clk: ^: Qnot L");

			flip.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: L; Clk: v: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: L; Clk: v: Qnot L");

			flip.D.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: ^; Clk: L: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: ^; Clk: L: Qnot L");

			flip.Clk.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: H; Clk: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: H; Clk: ^: Qnot L");

			flip.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: H; Clk: v: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: H; Clk: v: Qnot L");

			flip.D.V = VoltageSignal.LOW;
			flip.Clk.V = VoltageSignal.HIGH;
			flip.D.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: L; Clk: ^: D: ^: Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: L; Clk: ^: D: ^: Qnot L");

			flip.Clk.V = VoltageSignal.LOW;
			flip.Clk.V = VoltageSignal.HIGH;
			flip.D.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, flip.Q.V, "Gate on: Pre: H; D: H; Clk: ^: D: v; Q H");
			Assert.AreEqual(VoltageSignal.LOW, flip.Qnot.V, "Gate on: Pre: H; D: H; Clk: ^: D: v; Qnot L");
		}

		[TestMethod]
		public void Clear()
		{
			// arrange
			var flip = new DFlipFlopEdgeWithPresetAndClear_2("test");
			flip.V.V = VoltageSignal.HIGH;
			flip.Clr.V = VoltageSignal.HIGH;
			// at this point, nothing we do should change Q or Qnot			

			// act, assert
			flip.Clk.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: L; Clk: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: L; Clk: ^: Qnot H");

			flip.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: L; Clk: v: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: L; Clk: v: Qnot H");

			flip.D.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: ^; Clk: L: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: ^; Clk: L: Qnot H");

			flip.Clk.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: H; Clk: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: H; Clk: ^: Qnot H");

			flip.Clk.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: H; Clk: v: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: H; Clk: v: Qnot H");

			flip.D.V = VoltageSignal.LOW;
			flip.Clk.V = VoltageSignal.HIGH;
			flip.D.V = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: L; Clk: ^: D: ^: Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: L; Clk: ^: D: ^: Qnot H");

			flip.Clk.V = VoltageSignal.LOW;
			flip.Clk.V = VoltageSignal.HIGH;
			flip.D.V = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, flip.Q.V, "Gate on: Clr: H; D: H; Clk: ^: D: v; Q L");
			Assert.AreEqual(VoltageSignal.HIGH, flip.Qnot.V, "Gate on: Clr: H; D: H; Clk: ^: D: v; Qnot H");
		}
	}
}
