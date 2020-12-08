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
				System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
				sw.Start();
				Day7_2();
				sw.Stop();
				Console.WriteLine(sw.Elapsed);
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

		// This is inefficient. I plan on developing a more efficient algorithm
		void Day7_2() {
			// Initialize variables
			int count = 0;
			string[] input = (File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input7.txt")));
			List<string> possible = new List<string>() { "shiny gold" };

			do {
				List<string> next = new List<string>();
				for (int i = 0; i < input.Length; i++) {
					foreach (string item in possible) {
						// Find where item has children
						if (input[i].StartsWith(item)) {
							// Last iteration, no children
							if (input[i].Contains("no other bags")) { break; }
							
							// Add all its children
							string[] fragment = input[i].Split(' ');
							for (int j = 4; j < fragment.Length; j += 4) {
								int amount = Convert.ToInt32(fragment[j]);
								for (int k = 0; k < amount; k++) {
									next.Add(fragment[j + 1] + " " + fragment[j + 2]);
									count++;
								}
							}
						}
					}
				}

				// Reset for next loop
				possible = next;
			} while (possible.Count > 0);

			Console.WriteLine(count);
		}

	}
}
