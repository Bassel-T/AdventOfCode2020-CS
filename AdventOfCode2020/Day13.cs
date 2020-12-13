using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
	class Day13 {

		public Day13(bool Part2) {
			if (Part2) {
				Day13_2();
			} else {
				Day13_1();
			}
		}

		void Day13_1() {
			// Read data
			string[] data = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input13.txt"));
			int goal = int.Parse(data[0]);
			List<int> buses = new List<int>();
			string[] busString = data[1].Split(',');
			for (int i = 0; i < busString.Length; i++) {
				if (busString[i] == "x") { continue; }
				buses.Add(int.Parse(busString[i]));
			}

			// Find how long until each bus after position
			int[] nextBus = new int[buses.Count];
			for (int i = 0; i < buses.Count; i++) {
				nextBus[i] = buses[i] - (goal % buses[i]);
			}

			// Output minimum
			Console.WriteLine(buses[Array.IndexOf(nextBus, nextBus.Min())] * nextBus.Min());
		}

		void Day13_2() {
			// Read data into the list
			string[] data = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input13.txt"));
			List<Tuple<int,int>> buses = new List<Tuple<int,int>>();
			string[] busString = data[1].Split(',');
			for (int i = 0; i < busString.Length; i++) {
				if (busString[i] == "x") { continue; }
				buses.Add(new Tuple<int,int>(int.Parse(busString[i]), i));
			}

			// Match (t) and step
			double step = buses[0].Item1;
			double t = step;

			// Loop through the array
			for (int i = 1; i < buses.Count; i++) {
				while ((t + buses[i].Item2) % buses[i].Item1 != 0) {
					// Increment until match
					t += step;
				}

				// Reset step
				step = LeastComonMultiple(step, buses[i].Item1);
			}

			Console.WriteLine(t);
		}

		double GreatestCommonDenominator(double a, double b) {
			while (b > 0) {
				double t = b;
				b = a % b;
				a = t;
			}

			return a;
		}

		double LeastComonMultiple(double a, double b) {
			return a * b / GreatestCommonDenominator(a, b);
		}

	}
}
