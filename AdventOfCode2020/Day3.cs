using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;

/*
Day 1 Challenges:

Part 1:
Given a graph, Input1.txt, find the number of trees (# symbols) along a diagonal.

Part 2:
Given the same graph, find the number of trees (# symbols) across five diagonals and multiply the numbers.

Solution:
My solution for each involves looking up positions in each line and counting those instances.

The first runs in a time-complexity of O(n).
- The program only looks at a given character within the line and compares it to #. If found, increment trees.

The second rnus in a time-complexity of about O(n).
- This has the same algorithm, but checks for all parameters to avoid looping multiple times.

Limitations:
- The program isn't very flexible. I don't use secondary functions to check for the n across and m down.
	- Part Two also can't change the number of inputs easily.
 */
namespace AdventOfCode2020 {
	class Day3 {

		public Day3(bool Part2) {
			if (Part2) {
				Day3_2();
			} else {
				Day3_1();
			}
		}

		void Day3_1() {
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input3.txt"))) {
				int row = 0;
				int trees = 0;

				// Required to get the right number
				bool end = false;

				for (string line = reader.ReadLine(); !end; line = reader.ReadLine()) {
					if (line.Substring((row * 3) % line.Length, 1) == "#") {
						trees++;
					}
					row++;

					end = reader.EndOfStream;

				}

				Console.WriteLine(trees);
			}
		}

		void Day3_2() {
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input3.txt"))) {
				int[] trees = new int[5] { 0, 0, 0, 0, 0 };

				// Row variable
				int i = 0;

				// Required to get the right number
				bool end = false;

				for (string line = reader.ReadLine(); !end; line = reader.ReadLine()) {

					// Right 1, Down 1
					if (line.Substring(i % line.Length, 1) == "#") {
						trees[0]++;
					}

					// Right 3, Down 1
					if (line.Substring((i * 3) % line.Length, 1) == "#") {
						trees[1]++;
					}

					// Right 5, Down 1
					if (line.Substring((i * 5) % line.Length, 1) == "#") {
						trees[2]++;
					}

					// Right 7, Down 1
					if (line.Substring((i * 7) % line.Length, 1) == "#") {
						trees[3]++;
					}

					// Right 1, Down 2
					if (i % 2 == 0) {
						if (line.Substring((i / 2) % line.Length, 1) == "#") {
							trees[4]++;
						}
					}

					i++;
					end = reader.EndOfStream;
				}

				// Calculate the product
				ulong prod = (ulong)trees[0];
				for (i = 1; i < trees.Length; i++) {
					prod *= (ulong)trees[i];
				}

				Console.WriteLine(prod);
			}
		}
	}
}
