using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2020 {
	class Day7 {

		public Day7(bool Part2) { 
			if (Part2) {
				Day7_2();
			} else {
				Day7_1();
			}
		}

		void Day7_1() {
			// Initialize all variables
			int count = 0;
			string[] input = (File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input7.txt")));
			
			// List of bag colors to find
			List<string> possible = new List<string>() { "shiny gold" };
			
			// Avoid double counting
			bool[] visited = new bool[input.Length];

			do {
				List<string> next = new List<string>();
				for (int i = 0; i < input.Length; i++) {
					foreach (string item in possible) {
						// Find unvisited parents
						if (input[i].Contains(item) && !input[i].StartsWith(item) && !visited[i]) {
							count++;
							next.Add(input[i].Split(" bags ", StringSplitOptions.None)[0]);
							visited[i] = true;
						}
					}
				}

				// Reset the loop with next values
				possible = next;
			} while (possible.Count > 0);

			Console.WriteLine(count);

		}

		void Day7_2() {
			// Initialize variables
			int count = 0;
			string[] input = (File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input7.txt")));
			List<string> possible = new List<string>() { "shiny gold" };

			Dictionary<string, Tuple<int, string>[]> data = new Dictionary<string, Tuple<int, string>[]>();
			
			// Create the data entries
			foreach (string line in input) {
				string[] fragment = line.Split(' ');
				string name = fragment[0] + " " + fragment[1];
				List<Tuple<int, string>> toAdd = new List<Tuple<int, string>>();
			
				if (fragment[4] != "no") {
					for (int i = 4; i < fragment.Length; i += 4) {
						toAdd.Add(Tuple.Create(Convert.ToInt32(fragment[i]), fragment[i + 1] + " " + fragment[i + 2]));
					}
				}
			
				data.Add(name, toAdd.ToArray());
			}
			
			do {
				List<string> next = new List<string>();
			
				foreach (string item in possible) {
					Tuple<int, string>[] factors = data[item];
			
					foreach (Tuple<int, string> factor in factors) {
						for (int i = 0; i < factor.Item1; i++) {
							next.Add(factor.Item2);
							count++;
						}
					}
				}
			
				possible = next;
			} while (possible.Count > 0);

			Console.WriteLine(count);
		}

	}
}
