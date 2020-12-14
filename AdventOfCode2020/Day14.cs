using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020 {
	class Day14 {

		public Day14(bool Part2) {
			if (Part2) {
				Day14_2();
			} else {
				Day14_1();
			}
		}

		void Day14_1() {
			// Initialize base data
			Dictionary<int, ulong> mem = new Dictionary<int, ulong>();
			string mask = "";
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input14.txt"))) {
				do {
					// Get current input
					string input = reader.ReadLine();
					if (input.StartsWith("mask")) {
						// Input is the mask
						mask = input.Split(" = ")[1];
					} else {
						// Input is memory

						// Get appropriate data
						string[] fragments = input.Split(" = ");
						int bracketLeft = fragments[0].IndexOf('[');
						int bracketRight = fragments[0].IndexOf(']');
						int addr = int.Parse(fragments[0].Substring(4, bracketRight - bracketLeft - 1));
						string bValue = Convert.ToString(int.Parse(fragments[1]), 2);
						
						while (bValue.Length < 36) {
							// Extend binary to 36 bits long
							bValue = bValue.Insert(0, "0");
						}

						// Find value with mask layer
						string maskedValue = "";
						for (int i = 0; i < mask.Length; i++) {
							if (mask[i] == '1' && bValue[i] == '0') {
								maskedValue += '1';
							} else if (mask[i] == '0' && bValue[i] == '1') {
								maskedValue += '0';
							} else {
								maskedValue += bValue[i];
							}
						}

						// Add/Update value and memory to dictionary
						ulong value = Convert.ToUInt64(maskedValue, 2);
						if (mem.ContainsKey(addr)) {
							mem[addr] = value;
						} else {
							mem.Add(addr, value);
						}
					}
				} while (!reader.EndOfStream);
			}

			// Get the sum of everything in the dictionary
			ulong sum = 0;
			foreach (KeyValuePair<int, ulong> pair in mem) {
				Console.WriteLine($"{sum} += {pair.Value}");
				sum += pair.Value;
			}

			Console.WriteLine(sum);
			
		}

		void Day14_2() {
			// Initialize data
			Dictionary<ulong, int> mem = new Dictionary<ulong, int>();
			string mask = "";
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input14.txt"))) {
				do {
					// Read file input
					string input = reader.ReadLine();
					if (input.StartsWith("mask")) {
						// Input is a mask
						mask = input.Split(" = ")[1];
					} else {
						// Input is memory and address
						string[] fragments = input.Split(" = ");
						int bracketLeft = fragments[0].IndexOf('[');
						int bracketRight = fragments[0].IndexOf(']');
						int value = int.Parse(fragments[1]);
						string bAddr = Convert.ToString(int.Parse(fragments[0].Substring(4, bracketRight - bracketLeft - 1)), 2);

						while (bAddr.Length < 36) {
							// Extend the address to 36 bits
							bAddr = bAddr.Insert(0, "0");
						}

						// Mask the address with X values
						string maskedAddr = "";
						for (int i = 0; i < mask.Length; i++) {
							if (mask[i] == '1' && bAddr[i] == '0') {
								maskedAddr += '1';
							} else if (mask[i] == 'X') {
								maskedAddr += 'X';
							} else {
								maskedAddr += bAddr[i];
							}
						}

						// Initialize data for current masked address
						int countX = (maskedAddr.Count(x => x == 'X'));
						Regex reg = new Regex(Regex.Escape("X"));

						for (int i = 0; i < Math.Pow(2, countX); i++) {
							string layerMask = Convert.ToString(i, 2);
							string newAddr = maskedAddr;

							while (layerMask.Length < countX) {
								// Add padding to layerMask
								layerMask = layerMask.Insert(0, "0");
							}

							for (int j = 0; j < layerMask.Length; j++) {
								// Match X values to binary of layerMask
								newAddr = reg.Replace(newAddr, $"{layerMask[j]}", 1);
							}

							// Find the address in base 10
							ulong addr = (Convert.ToUInt64(newAddr, 2));
							
							// Add or update memory location
							if (mem.ContainsKey(addr)) {
								mem[addr] = value;
							} else {
								mem.Add(addr, value);
							}
						}
					}
				} while (!reader.EndOfStream);
			}

			// Sum all data
			ulong sum = 0;
			foreach (KeyValuePair<ulong, int> pair in mem) {
				Console.WriteLine($"{sum} += {pair.Value}");
				sum += (ulong)pair.Value;
			}

			Console.WriteLine(sum);
		}

	}
}
