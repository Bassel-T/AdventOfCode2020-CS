using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
	class Day9 {

		public Day9(bool Part2) {
			if (Part2) {
				Day9_2();
			} else {
				Day9_1();
			}
		}

		void Day9_1() {
			// Get all the data as an array
			long[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input9.txt"))
				.Select(x => long.Parse(x)).ToArray();

			for (long i = 25; i < data.Length; i++) {
				// Initialize appropriate data
				long check = data[i];
				long[] prev = new long[25];
				long[] diff = new long[25];
				for (long j = 0; j < 25; j++) {
					prev[j] = data[i - j - 1];
					diff[j] = check - prev[j];
				}

				// Not the sum of two unique numbers in last 25
				if (prev.Intersect(diff).Count() < 2) {
					Console.WriteLine(check);
					return;
				}
			}

		}

		void Day9_2() {
			// Initialize data (sumCheck is the result from Part 1)
			long sumCheck = 675280050;
			long[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input9.txt"))
				.Select(x => long.Parse(x)).ToArray();

			for (int i = 0; data[i] < sumCheck; i++) {
				// Initialize check data
				long sum = data[i];
				long max = 0;
				for (int j = 1; sum < sumCheck; j++) {
					// Continuously add numbers until equal to or greater than sumCheck
					sum += data[i + j];
					max = Math.Max(max, data[i + j]);
				}
				
				// The values are equal, output them
				if (sum == sumCheck) {
					Console.WriteLine(data[i] + max);
					return;
				}
			}
		}
	}
}
