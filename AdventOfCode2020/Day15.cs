using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
	class Day15 {

		// Complete the desired Day 15 question
		public Day15(bool Part2) {
			if (Part2) {
				Day15_2();
			} else {
				Day15_1();
			}
		}

		// Add the appropriate item to the array
		Dictionary<int, Tuple<int, int>> SayValue(Dictionary<int, Tuple<int, int>> dict, int value, int turn) {
			if (dict.ContainsKey(value)) {
				if (dict[value].Item2 == -1) {
					// Second time adding it
					dict[value] = new Tuple<int, int>(dict[value].Item1, turn);
				} else {
					// Shift memory over
					dict[value] = new Tuple<int, int>(dict[value].Item2, turn);
				}
			} else {
				// New value altogether
				dict.Add(value, new Tuple<int, int>(turn, -1));
			}

			return dict;
		}

		void Day15_1() {
			// Instantiate base data
			Dictionary<int, Tuple<int, int>> memory = new Dictionary<int, Tuple<int, int>>();
			int[] data = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input15.txt"))[0]
				.Split(',')
				.Select(x => int.Parse(x))
				.ToArray();
			int turn = 1;
			int lastSpoken = 0;

			for (; turn < data.Length + 1; turn++) {
				// Read from the input
				memory.Add(data[turn - 1], new Tuple<int, int>(turn, -1));
				lastSpoken = data[turn - 1];
			}

			for (; turn <= 2020; turn++) {
				// Count up values
				if (memory.ContainsKey(lastSpoken)) {
					if (memory[lastSpoken].Item2 == -1) {
						// That was the first time it was said
						memory = SayValue(memory, 0, turn);
						lastSpoken = 0;
					} else {
						// Find the difference
						int temp = memory[lastSpoken].Item2 - memory[lastSpoken].Item1;
						memory = SayValue(memory, memory[lastSpoken].Item2 - memory[lastSpoken].Item1, turn);
						lastSpoken = temp;
					}
				} else {
					// Add new value to memory
					memory = SayValue(memory, lastSpoken, turn);
				}
			}

			// Find the appropriate value
			int[] answers = memory.Where(x => x.Value.Item1 == 2020 || x.Value.Item2 == 2020).Select(y => y.Key).ToArray();
			foreach (int num in answers) {
				Console.WriteLine(num);
			}
		}

		void Day15_2() {
			// Initialize data
			Dictionary<int, Tuple<int, int>> memory = new Dictionary<int, Tuple<int, int>>();
			int[] data = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input15.txt"))[0]
				.Split(',')
				.Select(x => int.Parse(x))
				.ToArray();
			int turn = 1;
			int lastSpoken = 0;

			for (; turn < data.Length + 1; turn++) {
				// Loop through input
				memory.Add(data[turn - 1], new Tuple<int, int>(turn, -1));
				lastSpoken = data[turn - 1];
			}

			for (; turn <= 30000000; turn++) {
				if (memory.ContainsKey(lastSpoken)) {
					if (memory[lastSpoken].Item2 == -1) {
						// That was the first time it was said
						memory = SayValue(memory, 0, turn);
						lastSpoken = 0;
					} else {
						// Find difference and speak it
						int temp = memory[lastSpoken].Item2 - memory[lastSpoken].Item1;
						memory = SayValue(memory, memory[lastSpoken].Item2 - memory[lastSpoken].Item1, turn);
						lastSpoken = temp;
					}
				} else {
					// Add new data point
					memory = SayValue(memory, lastSpoken, turn);
				}
			}

			// Find appropriate result
			int[] answers = memory.Where(x => x.Value.Item1 == 30000000 || x.Value.Item2 == 30000000).Select(y => y.Key).ToArray();
			foreach (int num in answers) {
				Console.WriteLine(num);
			}
		}

	}
}
