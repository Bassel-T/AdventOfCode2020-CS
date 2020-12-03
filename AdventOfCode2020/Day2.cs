using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace AdventOfCode2020 {
	class Day2 {

		public Day2(bool Part2) {
			if (Part2) {
				Day2_2();
			} else {
				Day2_1();
			}
		}

		void Day2_1() {
			// Count the number of valid passwords
			int valid = 0;
			
			// Open the file
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "input2.txt"))) {
				while (!reader.EndOfStream) {
					// Get important data
					string input = reader.ReadLine();

					// Avoid using "indexOf()" to save efficiency (O(n) --> O(1))
					int dash = input[1] == '-' ? 1 : 2;
					int space = (input[dash + 2] == ' ' ? 2 : 3);

					// Remove all instances of the character
					string removed = input.Replace(input.Substring(dash + space + 1, 1), "");
					
					// Get bounds
					int min = Convert.ToInt32(input.Substring(0, dash));
					int max = Convert.ToInt32(input.Substring(dash + 1, space - 1));

					// Test against range
					// (Don't bother testing upper range if not in lower)
					if (input.Length - removed.Length - 1 >= min) {
						if (input.Length - removed.Length - 1 <= max) {
							valid++;
						}
					}
				}
			}

			Console.WriteLine(valid);
		}

		void Day2_2() {
			// Initialize count
			int valid = 0;

			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "input2.txt"))) {
				while (!reader.EndOfStream) {
					
					// Read line
					string input = reader.ReadLine();

					// Get parameters
					int dash = input[1] == '-' ? 1 : 2;
					int space = (input[dash + 2] == ' ' ? 2 : 3);
					int first = Convert.ToInt32(input.Substring(0, dash));
					int second = Convert.ToInt32(input.Substring(dash + 1, space - 1));
					char check = input.Substring(dash + space + 1, 1)[0];

					// Substring for password (Indexed-one)
					string password = input.Substring(dash + space + 3);

					// Check for validity
					if ((password[first] == check) ^ (password[second] == check)) {
						valid++;
					}
				}
			}

			Console.WriteLine(valid);
		}

	}
}
