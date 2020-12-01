using System;
using System.Collections.Generic;
using System.IO;

/*
Day 1 Challenges:

Part 1:
Given a list, Input1.txt, find two integer values that sum to 2020 and find their product.

Part 2:
Given the same list, find three integer values that sum to 2020 and find their product.

Solution:
My solution for each involves creating a list and look-up table for the inputs.

The first runs in a spacial-complexity of about O(n) and a time-complexity of O(n).
- The program loops through the list and checks if the difference is "true" in the array.
- If so, the pair was found.

The second rnus in a spacial-complexity of about O(n) and a time-complexity of about O(n^2).
- The only difference with the first is that an identical loop is created between the
	loop and the lookup section.

Limitations:
- If the number 1010 exists once, my code will assume it shows up more than once and use that as the result.
- My program breaks with negative numbers and sufficiently large datasets.
 */
namespace AdventOfCode2020 {
	class Day1 {

		// For easy manipulation
		int desired = 2020;

		/// <summary>
		/// Run the algorithm for Day 1
		/// </summary>
		/// <param name="Part2">Running with "true" will run the algorithm for Part 2.</param>
		public Day1(bool Part2) {
			if (Part2) {
				Day1_2();
			} else {
				Day1_1();
			}
		}

		public void Day1_1() {
			List<int> numbers = new List<int>();

			// Get the information from the file
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input1.txt"))) {
				while (!reader.EndOfStream) {
					int val = Convert.ToInt32(reader.ReadLine());
					numbers.Add(val);
				}
			}

			// Create a boolean array of all acceptable values
			bool[] arr = new bool[numbers[numbers.Count - 1] + 1];
			foreach (int i in numbers) {
				arr[i] = true;
			}

			foreach (int i in numbers) {
				if (arr[desired - i]) {
					// Look-up table
					Console.WriteLine(i * (desired - i));
					return;
				}
			}
		}

		public void Day1_2() {
			List<int> numbers = new List<int>();

			// Get the information from the file
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input1.txt"))) {
				while (!reader.EndOfStream) {
					int val = Convert.ToInt32(reader.ReadLine());
					numbers.Add(val);
				}
			}

			// Sort in increasing order
			numbers.Sort();

			// Create a boolean array of all acceptable values
			bool[] arr = new bool[numbers[numbers.Count - 1] + 1];
			foreach (int i in numbers) {
				arr[i] = true;
			}

			foreach (int i in numbers) {
				foreach (int j in numbers) {
					if (i + j >= desired) {
						// Skip all remainder values of j
						break;
					}

					if (arr[desired - i - j]) {
						// Look-up table
						Console.WriteLine(i * j * (desired - i - j));
						return;
					}
				}
			}
		}
	}
}
