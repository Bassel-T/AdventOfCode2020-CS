using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020 {
	class Day4 {

		public Day4(bool Part2) {
			if (Part2) {
				Day4_2();
			} else {
				Day4_1();
			}
		}

		void Day4_1() {
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input4.txt"))) {

				int valid = 0;
				int attributes = 0;
				bool cid = false;

				do {

					string line = reader.ReadLine();

					if (line == "") {

						if ((attributes == 7 && !cid) || attributes == 8) {
							valid++;
						}

						attributes = 0;
						cid = false;
					} else {
						string[] fragments = line.Split(' ');
						foreach (string att in fragments) {
							if (att.StartsWith("c")) {
								cid = true;
							}
							attributes++;
						}
					}

				} while (!reader.EndOfStream);

				if ((attributes == 7 && !cid) || attributes == 8) {
					valid++;
				}

				Console.WriteLine(valid);
			}
		}

		void Day4_2() {
			string[] eyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Input4.txt"))) {

				int valid = 0;
				int attributes = 0;
				bool cid = false;
				bool entry = true;

				do {

					string line = reader.ReadLine();

					if (line == "") {

						if (entry && ((attributes == 7 && !cid) || attributes == 8)) {
							valid++;
						}

						attributes = 0;
						cid = false;
						entry = true;
					} else {
						string[] frag = line.Split(new char[] { ':', ' ' });
						for (int i = 0, c = frag.Length; i < c && entry; i += 2) {
							if (frag[i] == "byr") {
								int year = Convert.ToInt32(frag[i + 1]);
								if (year < 1920 || year > 2002) {
									entry = false;
									
								}
							} else if (frag[i] == "iyr") {
								int year = Convert.ToInt32(frag[i + 1]);
								if (year < 2010 || year > 2020) {
									entry = false;

								}
							} else if (frag[i] == "eyr") {
								int year = Convert.ToInt32(frag[i + 1]);
								if (year < 2020 || year > 2030) {
									entry = false;

								}
							} else if (frag[i] == "hgt") {
								if (!(new Regex(@"^(1(([5-8]\d)|(9[0-3]))cm)|(((59)|(6\d)|(7[0-6]))in)$")).IsMatch(frag[i + 1])) {
									entry = false;

								}
							} else if (frag[i] == "hcl") {
								if (!(new Regex(@"^#[\d|a-f]{6}$").IsMatch(frag[i + 1]))) {
									entry = false;

								}
							} else if (frag[i] == "ecl") {
								if (Array.IndexOf(eyeColors, frag[i + 1]) == -1) {
									entry = false;

								}
							} else if (frag[i] == "pid") {
								if (!(new Regex(@"^\d{9}$").IsMatch(frag[i + 1]))) {
									entry = false;

								}
							} else if (frag[i] == "cid") {
								cid = true;
							}

							if (!entry) {
								break;
							}

							attributes++;
						}
					}

				} while (!reader.EndOfStream);

				Console.WriteLine(valid);
			}
		}
	}
}
