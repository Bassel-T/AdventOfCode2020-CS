using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020 {
	// Graph in Matrix Form
	class Graph {
		// Attributes
		public bool[,] Matrix;
		public int Dim;

		// Intializer
		public Graph(int[] input) {
			Dim = input.Length;
			Matrix = new bool[Dim, Dim];

			for (int i = 0; i < input.Length; i++) {
				for (int j = i; j < input.Length; j++) {
					int diff = input[j] - input[i];
					if (diff > 0 && diff < 4) {
						Matrix[i, j] = true;
					}
				}
			}
		}

		// DEBUG: Output the graph
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

		// Find all paths in graph
		public ulong Paths() {
			ulong paths = 0;

			// Create a stack for the previous layers
			Stack<Tuple<int, int>> trace = new Stack<Tuple<int, int>>();
			trace.Push(new Tuple<int, int>(0, -1));
			do {
				// Return to previous layer
				Tuple<int, int> tuple = trace.Pop();
				int layer = tuple.Item1;
				for (int i = tuple.Item2 + 1; i < Dim; i++) {
					// Find the next edge from the node
					if (Matrix[layer, i]) {
						// Go down a layer
						trace.Push(new Tuple<int, int>(layer, i));
						layer = i;
					}

					if (layer == Dim - 1) {
						// Last node found, exit
						paths++;
						break;
					}
				}
			} while (trace.Count > 0);

			return paths;
		}

	}

	class Day10 {

		// Run code based on desired output
		public Day10(bool Part2) {
			if (Part2) {
				Day10_2();
			} else {
				Day10_1();
			}
		}

		void Day10_1() {
			// Get input as integers
			int[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input10.txt"))
				.Select(x => int.Parse(x)).ToArray();

			// Sort in O(n log(n))
			Array.Sort(data);

			int oneCount = 0;
			int threeCount = 1;

			// First number is either 1 or 3
			if (data[0] == 1) { oneCount++; } else if (data[0] == 3) { threeCount++; }

			// Analyze every node
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

		void Day10_2() {
			// Get input as integer array
			int[] data = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "Input10.txt"))
				.Select(x => int.Parse(x)).ToArray();

			// Sort in O(n log(n))
			Array.Sort(data);

			// Create graph and find its paths
			Graph list = new Graph(data);
			Console.WriteLine(list.Paths());
		}
	}
}
