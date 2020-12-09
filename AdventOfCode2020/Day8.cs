using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
	class EightGraph {
		int[,] Graph;
		int dim;

		public void SetGraph(string[] input) {
			dim = input.Length;

			for (int i = 0; i < dim; i++) {
				for (int j = 0; j < dim; j++) {
					if (input[i].StartsWith("jmp") && Convert.ToInt32(input[i].Substring(4)) == j) {
						Graph[i, j] = 1;
					} else if (!input[i].StartsWith("acc") && i + 1 == j) {
						Graph[i, j] = 1;
					}
				}
			}
		}

		/// <summary>
		/// Find the node that creates a loop
		/// </summary>
		/// <returns></returns>
		public int DFS() {
			return 1;
		}
	}


	class Day8 {

		public Day8(bool Part2) {
			if (Part2) {
				Day8_2();
			} else {
				Day8_1();
			}
		}

		void Day8_1() {
			// Initialize variables
			string[] program = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input8.txt"));
			bool[] visited = new bool[program.Length];
			int acc = 0;

			for (int i = 0; !visited[i]; i++) {
				// Save history
				visited[i] = true;

				// Run appropriate command
				int amount = Convert.ToInt32(program[i].Substring(4));
				switch (program[i].Substring(0, 3)) {
					case "acc":
						acc += amount;
						break;
					case "jmp":
						i += amount - 1;
						break;
				}
			}

			Console.WriteLine(acc);
		}

		void Day8_2() {
			string[] program = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input8.txt"));

			// Brute Force, try every possibility
			for (int j = 0; j < program.Length; j++) {
				// Initialize current instance of variables
				bool[] visited = new bool[program.Length];
				int acc = 0;

				for (int i = 0; !visited[i]; i++) {
					// Save hitsory
					visited[i] = true;

					// Run appropriate instruction
					int amount = Convert.ToInt32(program[i].Substring(4));
					switch (program[i].Substring(0, 3)) {
						case "acc":
							acc += amount;
							break;
						case "jmp":
							if (i != j) {
								// This instruction isn't flipped
								i += amount - 1;
							}
							break;
						case "nop":
							if (i == j) {
								// This instruction is flipped
								i += amount - 1;
							}
							break;
					}

					// This iteration was the appropriate answer
					if (i == program.Length - 1) {
						Console.WriteLine(acc);
						return;
					}

					// Out of bounds
					if (i < 0 || i > program.Length - 1) {
						break;
					}
				}
			}
		}
	}
}
