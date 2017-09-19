using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	class TestEventsHelper
	{
		public TestEventsHelper(IOutput oe)
		{
			ResetStatus();
			oe.AddOutputHandler(_ => { EventFired = true; });
		}

		public TestEventsHelper(ISum se)
		{
			ResetStatus();
			se.AddSumHandler(_ => { EventFired = true; });
		}

		public TestEventsHelper(ICarry ce)
		{
			ResetStatus();
			ce.AddCarryHandler(_ => { EventFired = true; });
		}

		public bool EventFired { get; private set; }

		public void ResetStatus()
		{
			EventFired = false;
		}
	}
}
