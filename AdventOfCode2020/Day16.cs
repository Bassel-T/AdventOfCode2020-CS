using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace AdventOfCode2020 {
	class Day16 {

		// Run appropriate result
		public Day16(bool Part2) {
			if (Part2) {
				Day16_2();
			} else {
				Day16_1();
			}
		}

		void Day16_1() {
			int count = 0;
			List<Tuple<int, int>> rules = new List<Tuple<int,int>>();

			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input16.txt"))) {
				string input = "";
				do {
					// Get all the rules
					input = reader.ReadLine();
					List<string> lineRules = (Regex.Matches(input, @"\d+-\d+").Cast<Match>().Select(x => x.Value).ToList());
					foreach (string range in lineRules) {
						int[] bounds = range.Split('-').Select(x => int.Parse(x)).ToArray();
						rules.Add(new Tuple<int, int>(bounds[0], bounds[1]));
					}
				} while (!string.IsNullOrEmpty(input));

				do {
					// Skip non-ticket lines
					input = reader.ReadLine();
					if (string.IsNullOrEmpty(input)) { continue; }
					if (!char.IsDigit(input[0])) { continue; }

					// Read individual values
					int[] values = input.Split(',').Select(x => int.Parse(x)).ToArray();

					foreach (int item in values) {
						bool match = false;
						for (int i = 0, r = rules.Count; i < r; i++) {
							// Looking for one match
							if (item >= rules[i].Item1 && item <= rules[i].Item2) {
								match = true;
							}
						}
						if (!match) {
							count += item;
						}
					}

				} while (!reader.EndOfStream);
			}

			Console.WriteLine(count);
		}

		void Day16_2() {
			// Initialize appropriate lists
			List<Tuple<int, int>> rules = new List<Tuple<int, int>>();
			List<string> validTickets = new List<string>();
			List<List<List<int>>> matches = new List<List<List<int>>>();

			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input16.txt"))) {
				string input = "";
				do {
					// Get all the rules
					input = reader.ReadLine();
					List<string> lineRules = (Regex.Matches(input, @"\d+-\d+").Cast<Match>().Select(x => x.Value).ToList());
					foreach (string range in lineRules) {
						int[] bounds = range.Split('-').Select(x => int.Parse(x)).ToArray();
						rules.Add(new Tuple<int, int>(bounds[0], bounds[1]));
					}
				} while (!string.IsNullOrEmpty(input));

				do {
					// Skip non-ticket lines
					input = reader.ReadLine();
					if (string.IsNullOrEmpty(input)) { continue; }
					if (!char.IsDigit(input[0])) { continue; }

					// Check if valid and find its matches
					int[] values = input.Split(',').Select(x => int.Parse(x)).ToArray();
					List<List<int>> inputMatches = new List<List<int>>();
					bool broken = false;

					foreach (int item in values) {
						List<int> itemMatches = new List<int>();
						for (int i = 0, r = rules.Count; i < r; i++) {
							if (item >= rules[i].Item1 && item <= rules[i].Item2) {
								// Item mathces somewhere
								itemMatches.Add(i / 2);
							}
						}
						
						if (itemMatches.Count == 0) {
							// Item doesn't match anywhere
							broken = true;
							break;
						}

						inputMatches.Add(itemMatches);
					}

					if (inputMatches.Count != 0 && !broken) {
						// Entire ticket is valid
						matches.Add(inputMatches);
						validTickets.Add(input);
					}

				} while (!reader.EndOfStream);
			}

			List<List<int>> possibleIndices = new List<List<int>>();

			for (int i = 0; i < rules.Count / 2; i++) {
				// Find each index's matching options
				List<int> intersect = new List<int>(matches[0][i]);
				for (int j = 1; j < matches.Count; j++) {
					intersect = intersect.Intersect(matches[j][i]).ToList();
				}

				possibleIndices.Add(intersect);
			}

			// Find true points through set operations
			// NOTE: options have increasing amounts (1, 2, 3) and (1) --> (2) --> (3) --> etc.
			int[] trueIndices = new int[rules.Count / 2];

			List<int> lastIteration = new List<int>();
			for (int i = 1; i <= trueIndices.Count(); i++) {
				List<int> iteration = possibleIndices.Where(x => x.Count() == i).First();
				int item = iteration.Except(lastIteration).First();

				// Found appropriate index of item
				trueIndices[item] = possibleIndices.IndexOf(iteration);
				// Console.WriteLine($"Input item {i - 1} is {possibleIndices.IndexOf(iteration)}");

				lastIteration = new List<int>(iteration);
			}

			// Multiply first 6 (Departure)
			// More dynamic way to do this is assign string to rules
			ulong prod = 1;
			ulong[] myTicket = validTickets[0].Split(',').Select(x => ulong.Parse(x)).ToArray();

			for (int i = 0; i < 6; i++) {
				prod *= myTicket[trueIndices[i]];
			}

			Console.WriteLine($"After multiplying: {prod}");
		}
	}
}
