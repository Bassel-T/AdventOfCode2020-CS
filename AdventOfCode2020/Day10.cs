using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020 {
	class Graph {
		public int[,] Matrix;
		public int Dim;

		public Graph(int[] input) {
			Dim = input.Length;
			Matrix = new int[Dim, Dim];

			for (int i = 0; i < input.Length; i++) {
				for (int j = i; j < input.Length; j++) {
					int diff = input[j] - input[i];
					if (diff > 0 && diff < 4) {
						Matrix[i, j] = 1;
					}
				}
			}
		}

		public override string ToString() {
			string output = "[\n";
			for (int i = 0; i < Dim; i++) {
				for (int j = 0; j < Dim; j++) {
					output += Matrix[i, j] + ", ";
				}
				output += "\n";
			}

			output += "]";

			return output;
		}

		public ulong Paths() {
			ulong paths = 0;
			Stack<Tuple<int, int>> trace = new Stack<Tuple<int, int>>();
			trace.Push(new Tuple<int, int>(0, -1));
			do {
				Tuple<int, int> tuple = trace.Pop();
				int layer = tuple.Item1;
				for (int i = tuple.Item2 + 1; i < Dim; i++) {
					if (Matrix[layer, i] > 0) {
						trace.Push(new Tuple<int, int>(layer, i));
						layer = i;
					}

					if (layer == Dim - 1) {
						paths++;
						break;
					}
				}
			} while (trace.Count > 0);

			return paths;
		}

	}

	class Day10 {

		public Day10(bool Part2) {
			if (Part2) {
				Day10_2();
			} else {
				Day10_1();
			}
		}

		void Day10_1() {
			int[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input10.txt"))
				.Select(x => int.Parse(x)).ToArray();

			Array.Sort(data);

			int oneCount = 0;
			int threeCount = 1;

			if (data[0] == 1) { oneCount++; } else if (data[0] == 3) { threeCount++; }

			for (int i = 1; i < data.Length; i++) {
				switch (data[i] - data[i - 1]) {
					case 1:
						oneCount++;
						break;
					case 3:
						threeCount++;
						break;
				}
			}

			Console.WriteLine(oneCount * threeCount);
		}

		// Takes forever to run on the input
		void Day10_2() {
			int[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input10.txt"))
				.Select(x => int.Parse(x)).ToArray();

			Array.Sort(data);

			Graph list = new Graph(data);
			File.WriteAllLines("OutputGraph.txt", (list.ToString()).Split('\n'));
			Console.WriteLine(list.Paths());
		}

		// Doesn't work.
		void Day10_2_Faster() {
			int[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input10.txt"))
				.Select(x => int.Parse(x)).ToArray();
			Array.Sort(data);
			ulong[] steps = new ulong[] {1, 1, 1, 2, 4, 7, 13, 24};

			ulong count = 1;
			for (int i = 0; i < data.Length; i++) {
				int step = 1;
				for (int j = i + 1; j < data.Length; j++) {
					int diff = data[j] - data[i];
					if (diff == step) {
						step++;
					} else {
						i = j - 1;
						break;
					}
				}

				count *= steps[step];

			}

			Console.WriteLine(count);
		}
	}
}
