using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace AdventOfCode2020 {
	class Run {
		static void Main(string[] args) {
			Stopwatch sw = new Stopwatch();
			sw.Start();
			new Day15(true);
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
		}
	}
}
