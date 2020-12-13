using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode2020 {
	class Day12 {

		// Run proper algorithm
		public Day12(bool Part2) {
			if (Part2) {
				Day12_2();
			} else {
				Day12_1();
			}
		}

		void Day12_1() {
			//Initialize values
			int dir = 0;
			int x = 0;
			int y = 0;

			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input12.txt"))) {
				do {
					string input = reader.ReadLine();
					// Get input
					if (input.StartsWith('N')) {
						y += int.Parse(input.Substring(1));
					} else if (input.StartsWith('W')) {
						x -= int.Parse(input.Substring(1));
					} else if (input.StartsWith('S')) {
						y -= int.Parse(input.Substring(1));
					} else if (input.StartsWith('E')) {
						x += int.Parse(input.Substring(1));
					} else if (input.StartsWith('L')) {
						dir += int.Parse(input.Substring(1));

						while (dir >= 360) {
							// Bounds checking
							dir -= 360;
						}

					} else if (input.StartsWith('R')) {
						dir -= int.Parse(input.Substring(1));

						while (dir < 0) {
							// Bounds checking
							dir += 360;
						}
					} else if (input.StartsWith('F')) {
						// Calculate direction
						int yDir = (dir == 90 ? 1 : (dir == 270 ? -1 : 0));
						int xDir = (dir == 0 ? 1 : (dir == 180 ? -1 : 0));

						x += xDir * int.Parse(input.Substring(1));
						y += yDir * int.Parse(input.Substring(1));
					} else {
						// Extreme data
						Console.WriteLine("Invalid Char: {0}", input.Substring(1));
					}

					// Output data
					Console.WriteLine("Instruction {3}, East {0}, North {1}, Manhattan {2}", x, y, Math.Abs(x) + Math.Abs(y), input);
				} while (!reader.EndOfStream);
			}
		}

		void Day12_2() {
			// Initialize data
			int shX = 0;
			int shY = 0;
			int wpX = 10;
			int wpY = 1;

			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input12.txt"))) {
				do {
					string input = reader.ReadLine();
					// Read input
					if (input.StartsWith('N')) {
						wpY += int.Parse(input.Substring(1));
					} else if (input.StartsWith('W')) {
						wpX -= int.Parse(input.Substring(1));
					} else if (input.StartsWith('S')) {
						wpY -= int.Parse(input.Substring(1));
					} else if (input.StartsWith('E')) {
						wpX += int.Parse(input.Substring(1));
					} else if (input.StartsWith('L')) {
						for (int i = 0, c = int.Parse(input.Substring(1)); i < c; i += 90) {
							// Loop data enough times
							int temp = -wpY;
							wpY = wpX;
							wpX = temp;
						}
					} else if (input.StartsWith('R')) {
						for (int i = 0, c = int.Parse(input.Substring(1)); i < c; i += 90) {
							// Loop data enoguh times
							int temp = -wpX;
							wpX = wpY;
							wpY = temp;
						}
					} else if (input.StartsWith('F')) {
						// Move to waypoint enough times
						int count = int.Parse(input.Substring(1));
						shX += wpX * count;
						shY += wpY * count;
					}

					// Print data
					Console.WriteLine("Instruction {3}, East {0}, North {1}, Manhattan {2}", shX, shY, Math.Abs(shX) + Math.Abs(shY), input);
				} while (!reader.EndOfStream);
			}
		}

	}
}
