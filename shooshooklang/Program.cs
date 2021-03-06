using System;

namespace shooshooklang
{
	public class main {
		public static bool error;
		
		public static List<Var> variable = new List<Var>();
		public static List<bool> iftrue = new List<bool>();
		public static List<WhileCode> whilecode = new List<WhileCode>();
		public static void Main() {
			StreamReader SR = new StreamReader(Console.ReadLine() + ".shoo");

            string? line;
			int isinif = 0;
			string? WCodename = null;
			bool iswhile = false;
			while ((line = SR.ReadLine()) != null && !error)
            {
				string[] c = line.Replace(" ", "").Split(".");
				if (c.Length > 0 && c[c.Length - 1] != "슉슉슉슉") {
					if (line == "시. 시.") {
						WCodename = null;
						break;
					}

					if (WCodename != null) {
						for (int i = 0; i < whilecode.Count; i++)
						{
							if (whilecode[i].name == WCodename) {
								whilecode[i].line.Add(line);
								break;
							}
						}
					}

					if (c[0] == "슈슉시슈슉") {
						WhileCode wcode = new WhileCode();
						wcode.name = c[1];
						whilecode.Add(wcode);
						WCodename = c[1];
					}
				}
                if (error) {
                    break;
                }
            }
            while ((line = SR.ReadLine()) != null && !error)
            {
				string[] c = line.Replace(" ", "").Split(".");
				if (c.Length > 0 && c[c.Length - 1] != "슉슉슉슉" && c[0] != "슈슉시슈슉") {
					if (c[0] == "슈슉시슈슉시" && line.Substring(line.Length - 1) == ".") {
						int count = 0;
						iswhile = true;
						if (c[1] == "슉") {
							for (int a = 0; a < variable.Count; a++)
							{
								if (variable[a].name == c[2]) {
									count = variable[a].value;
									break;
								}
							}
						}
						else if (c[1] == "슈슉") {
							count = c[2].Count(f => f == '쇼') - c[2].Count(f => f == '쇽');
						}

						int codeNum = 0;

						for (int b = 0; b < whilecode.Count; b++)
						{
							if (whilecode[b].name == c[3]) {
								codeNum = b;
								break;
							}
						}

						for (int i = 0; i < count; i++)
						{
							for (int i2 = 0; i2 < whilecode[codeNum].line.Count; i2++)
							{
								if (iftrue.Count > 0 && isinif > 0) {
									if (iftrue[isinif-1]) {
										CodeReader(whilecode[codeNum].line[i2]);
									}
									else if (whilecode[codeNum].line[i2] == "시.") {
										isinif--;
										iftrue.RemoveAt(isinif);
									}
									else if (whilecode[codeNum].line[i2] == "슈슉. 시.") {
										isinif = 0;
										iftrue = new List<bool>();
									}
								}
								else {
									CodeReader(whilecode[codeNum].line[i2]);
								}
								string[] Wc = whilecode[codeNum].line[i2].Replace(" ", "").Split(".");
								if (Wc[0] == "슈슉슈슉슈슈슉슈슉") {
									isinif++;
								}
							}
						}
					}

					if (!iswhile) {
						if (iftrue.Count > 0 && isinif > 0) {
							if (iftrue[isinif-1]) {
								CodeReader(line);
							}
							else if (line == "시.") {
								isinif--;
								iftrue.RemoveAt(isinif);
							}
							else if (line == "슈슉. 시.") {
								isinif = 0;
								iftrue = new List<bool>();
							}
						}
						else {
							CodeReader(line);
						}
					}
				}
                if (error) {
                    break;
                }
				if (c[0] == "슈슉슈슉슈슈슉슈슉") {
					isinif++;
				}
				// Console.WriteLine(isinif + " " + iftrue.Count + " " + line);
            }
		}

		public static void CodeReader(string Code) {
			if (Code != "") {
				if (Code.Substring(Code.Length - 1) == ".") {
					string[] c = Code.Replace(" ", "").Split(".");
					string code = c[0];
					//출력하기
					if (code == "슈슈슉") {
						if (c[1] == "슉") {
							for (int a = 0; a < variable.Count; a++)
							{
								if (variable[a].name == c[2]) {
									Console.WriteLine(variable[a].value);
									break;
								}
							}
						}
						else {
							Console.WriteLine(c[1]);
						}
					}
					//변수선언
					if (code == "슉") {
						if (c[1].Length > 0) {
							Var v = new Var();
							v.name = c[1];
							switch (c[2])
							{
								case "슈":
									v.value = c[3];
									v.type = "string";
									break;
								case "슈슉":
									v.value = c[3].Count(f => f == '쇼') - c[3].Count(f => f == '쇽');
									v.type = "int";
									break;
							}
							variable.Add(v);
						}
					}
					
					//변수값편집
					if (code == "슉슉") {
						if (c[2].Length > 0) {
							for (int a = 0; a < variable.Count; a++)
							{
								if (variable[a].name == c[1]) {
									if (variable[a].type == "int") {
										variable[a].value = c[2].Count(f => f == '쇼') - c[2].Count(f => f == '쇽');
									}
									else {
										variable[a].value = c[2];
									}
								}
							}
						}
					}
					//더하기빼기곱하기나누기
					if (code == "쇽쇼쇽") {
						if (c[1] == "슈슉슈") {
							calculator(c, ((int)Operator.plus));
						}
						else if (c[1] == "슈슉슈슉") {
							calculator(c, ((int)Operator.substract));
						}
						else if (c[1] == "슈슉슈슉슈슈슉") {
							calculator(c, ((int)Operator.multiply));
						}
						else if (c[1] == "슈슉슈슈슉슈슉") {
							calculator(c, ((int)Operator.division));
						}
					}
				}
				else {
					string[] c = Code.Replace(" ", "").Split(".");
					string code = c[0];

					//if문
					if (code == "슈슉슈슉슈슈슉슈슉") {
						dynamic? A = null;
						dynamic? B = null;
						switch (c[1])
						{
							case "슉":
								for (int a = 0; a < variable.Count; a++)
								{
									if (variable[a].name == c[2]) {
										A = variable[a].value;
									}
								}
								break;
							case "슈슉":
								A = c[2].Count(f => f == '쇼') - c[2].Count(f => f == '쇽');
								break;
							case "슈":
								A = c[2];
								break;
							case "슈슈슉슈슉슈슈슉슉슉슈슉":
								A = null;
								break;
						}
						switch (c[3])
						{
							case "슉":
								for (int a = 0; a < variable.Count; a++)
								{
									if (variable[a].name == c[4]) {
										B = variable[a].value;
									}
								}
								break;
							case "슈슉":
								B = c[4].Count(f => f == '쇼') - c[4].Count(f => f == '쇽');
								break;
							case "슈":
								B = c[4];
								break;
							case "슈슈슉슈슉슈슈슉슉슉슈슉":
								B = null;
								break;
						}
						switch (c[5])
						{
							case "슉슉슉슈슈슉":
								iftrue.Add(ifConstruction(((int)Operator.equals), A, B));
								break;
							case "슉슉슈슈슉슈슉슈슈슉":
								iftrue.Add(ifConstruction(((int)Operator.big), A, B));
								break;
							case "슈슉슉슈슉슈슈슉":
								iftrue.Add(ifConstruction(((int)Operator.less), A, B));
								break;
							case "슈슈슉슈슉슈슈슉슉슉슈슉":
								iftrue.Add(ifConstruction(((int)Operator.bigeq), A, B));
								break;
							case "슉슈슉슈슉슈슈슉":
								iftrue.Add(ifConstruction(((int)Operator.lesseq), A, B));
								break;
							case "슈슈슉슈슉슈슈슉":
								iftrue.Add(ifConstruction(((int)Operator.notsign), A, B));
								break;
						}
					}

					//반복문
					
				}
			}
		}

		enum Operator
		{
			plus = 0,
			substract,
			multiply,
			division,

			equals,
			big,
			less,
			bigeq,
			lesseq,
			notsign,
		}

		public static void calculator(string[] c, int Operator) {
			int A = 0;
			int B = 0;
			if (c[5] != null && c[5] == "슉") {
				for (int a = 0; a < variable.Count; a++)
				{
					if (variable[a].name == c[2]) {
						if (variable[a].type == "int") {
							A = variable[a].value;
						}
					}
				}
			}
			else {
				A += c[2].Count(f => f == '쇼') - c[2].Count(f => f == '쇽');
			}
			if (c[6] != null && c[6] == "슉") {
				for (int a = 0; a < variable.Count; a++)
				{
					if (variable[a].name == c[3]) {
						if (variable[a].type == "int") {
							B = variable[a].value;
						}
					}
				}
				for (int a = 0; a < variable.Count; a++)
				{
					if (variable[a].name == c[4]) {
						if (variable[a].type == "int") {
							switch (Operator)
							{
								case 0:
									variable[a].value = A + B;
									break;

								case 1:
									variable[a].value = A - B;
									break;

								case 2:
									variable[a].value = A * B;
									break;

								case 3:
									variable[a].value = A / B;
									break;
							}
						}
					}
				}
			}
			else {
				B += c[3].Count(f => f == '쇼') - c[3].Count(f => f == '쇽');
				for (int a = 0; a < variable.Count; a++)
				{
					if (variable[a].name == c[4]) {
						if (variable[a].type == "int") {
							switch (Operator)
							{
								case 0:
									variable[a].value = A + B;
									break;

								case 1:
									variable[a].value = A - B;
									break;

								case 2:
									variable[a].value = A * B;
									break;

								case 3:
									variable[a].value = A / B;
									break;
							}
						}
					}
				}
			}
		}

		public static bool ifConstruction(int Operator, dynamic? A, dynamic? B) {
			bool b = false;
			switch (Operator)
			{
				case 4:
					b = A == B;
					break;

				case 5:
					b = A > B;
					break;

				case 6:
					b = A < B;
					break;

				case 7:
					b = A >= B;
					break;
				
				case 8:
					b = A <= B;
					break;

				case 9:
					b = A != B;
					break;
			}
			return b;
		}

		// public static void whileFunc(int count) {
		// 	int num = 0;
		// 	gotowhile:
		// 	for (int i = 0; i < count; i++)
		// 	{
		// 		if (whilecode[num].line[i] != null) {
		// 			string[] c = whilecode[num].line[i].Replace(" ", "").Split(".");
					
		// 			if (c[0] == "슈슉시슈슉시") {
		// 				num++;
		// 				if (c[1] == "슉") {
		// 					for (int a = 0; a < variable.Count; a++)
		// 					{
		// 						if (variable[a].name == c[2]) {
		// 							count = variable[a].value;
		// 							break;
		// 						}
		// 					}
		// 				}
		// 				else if (c[1] == "슈슉") {
		// 					count = c[2].Count(f => f == '쇼') - c[2].Count(f => f == '쇽');
		// 				}
		// 				goto gotowhile;
		// 			}
		// 			if (whilecode[num].line[i] == "시.") {
		// 				whilecode.RemoveAt(num);
		// 				num--;
		// 				if (whilecode[num] != null && whilecode[num].line.Count > 0) {
		// 					for (int b = 0; i < whilecode[num].line.Count; b++)
		// 					{
		// 						CodeReader(whilecode[num].line[b]);
		// 						whilecode[num].line.RemoveAt(b);
		// 					}
		// 				}
		// 			}
		// 			else {
		// 				CodeReader(whilecode[num].line[i]);
		// 				whilecode[num].line.RemoveAt(i);
		// 			}
		// 		}
        //     }
		// }

		public class WhileCode {
			public string? name;
			public List<string> line = new List<string>();
		}

		public class Var {
			public string? name;
			public dynamic? value;
			public string? type;
		}
	}
}