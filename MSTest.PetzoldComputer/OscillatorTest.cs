using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System;

namespace MSTest.PetzoldComputer
{
    [TestClass]
    public class OscillatorTest
    {
        [TestMethod]
        public void Constructor_Name()
        {
            // arrange, act
            var oscillator = new Oscillator(String.Empty);

            // assert
            Assert.AreEqual(VoltageSignal.LOW, oscillator.V.V, "Constructor: Voltage");
            Assert.AreEqual(VoltageSignal.LOW, oscillator.Output.V, "Constructor: Output");
        }

        [TestMethod]
        public void Constructor_Name_Num_Oscillations()
        {
            // arrange, act
            var oscillator = new Oscillator(String.Empty, 0);

            // assert
            Assert.AreEqual(VoltageSignal.LOW, oscillator.V.V, "Constructor: Voltage");
            Assert.AreEqual(VoltageSignal.LOW, oscillator.Output.V, "Constructor: Output");
        }

        [TestMethod]
        public void Start_Does_Nothing_When_Oscillator_Off()
        {
            // arrange
            var oscillator = new Oscillator(String.Empty);

            int nHighs = 0, nLows = 0;
            oscillator.Output.Changed += (cp) => {
                if (cp.V == VoltageSignal.HIGH) nHighs++;
                if (cp.V == VoltageSignal.LOW) nLows++;
            };

            // act
            oscillator.Start();

            // assert
            Assert.AreEqual(0, nHighs, "oscillator output should have never gone high");
            Assert.AreEqual(0, nLows, "oscillator output should have never gone low");
        }

        [TestMethod]
        public void Start_Goes_High_Low_When_Oscillator_On()
        {
            // arrange
            var oscillator = new Oscillator(String.Empty, 1);   // one cycle
            oscillator.V.V = VoltageSignal.HIGH;

            int nHighs = 0, nLows = 0;
            oscillator.Output.Changed += (cp) => {
                if (cp.V == VoltageSignal.HIGH) nHighs++;
                if (cp.V == VoltageSignal.LOW) nLows++;
            };

            // act
            oscillator.Start();

            // assert
            Assert.AreEqual(1, nHighs, "oscillator output should have gone high");
            Assert.AreEqual(1, nLows, "oscillator output should have gone low");
        }

        [TestMethod]
        public void Start_Oscillates_Correct_Number_Of_Times()
        {
            // arrange
            int nExpected = 42;
            var oscillator = new Oscillator(String.Empty, (uint)nExpected);
            oscillator.V.V = VoltageSignal.HIGH;

            int nHighs = 0, nLows = 0;
            oscillator.Output.Changed += (cp) => {
                if (cp.V == VoltageSignal.HIGH) nHighs++;
                if (cp.V == VoltageSignal.LOW) nLows++;
            };

            // act
            oscillator.Start();

            // assert
            Assert.AreEqual(nExpected, nHighs, "# times oscillator output should have gone high");
            Assert.AreEqual(nExpected, nLows, "# times oscillator output should have gone low");
        }
    }
}
