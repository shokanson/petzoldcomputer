using System;
using System.Collections.Generic;
using System.Linq;

namespace PetzoldComputer
{
    public enum VoltageSignal { LOW, HIGH };

    public static class Components
    {
        private static readonly Dictionary<string, uint> ComponentCount = new Dictionary<string, uint>();

        public static void Record(string typeName)
        {
            if (ComponentCount.TryGetValue(typeName, out uint value)) ComponentCount[typeName] = ++value;
            else ComponentCount[typeName] = 1;
        }

        public static ulong NumComponents => (ulong)ComponentCount.Sum(p => p.Value);

        public static new string ToString() => String.Join(";", ComponentCount.OrderByDescending(p => p.Value).ThenBy(p => p.Key).Select(p => $"{p.Key}:{p.Value}"));
    }
}
