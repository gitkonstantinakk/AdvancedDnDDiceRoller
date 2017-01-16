using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdvDiceRoller.Common;
using AdvDiceRoller.Dices;

namespace AdvDiceRoller.Console.RollCommands
{
	public static class Roll
	{

		public static void ProcessRoll (List<string> rolls, 
			bool adv, int advCount, 
			bool disAdv, int disAdvCount, 
			bool advDisAdv, int advDisAdvCount, List<string> advDisAdvExpr,
			bool isDc, int dcValue
			)

		public static void ProcessRoll (List<string> rolls, bool adv, int advCount, bool disAdv, int disAdvCount, bool advDisAdv, int advDisAdvCount, List<string> advDisAdvE)
		{
			// check if there is more than one "roll" expression (shouldn't be!!)
			if (rolls.Count() > 1)
			{
				throw new InvalidRollOperationException(ExceptionMessages.TooManyRolls);
			}

			// now we can be sure that te only expression in rolls list is the one with "roll" expression
			string roll = rolls[0];

			// check if there is more than one  "roll" in expression (shouldn't be!!)
			if (new Regex("roll").Matches(roll).Count > 1)
			{
					throw new InvalidRollOperationException(ExceptionMessages.TooManyRolls);
			}

			// eliminate characters invalid for roll expression
			roll = roll.ClearRoll();

			// check on brackets
			if (!roll.CheckBrackets())
			{
				throw new InvalidRollOperationException(ExceptionMessages.IncorrectBrackets);
			}

			// find all dice expressions (###d###) and store them in diceExprs list
			for (int i = 0; i < roll.Length; i++)
			{
				int newPos = Find(roll, i);
				if (newPos < 0)
				{
					break;
				}
				i = newPos;
			}

			// POLYMORPHISM BELOW!!!
			// generate roll results for all dice expressions found in expression
			List<int> rollResults = new List<int>();
			int advDisAdvIndex = 0;
			foreach (var dice in DiceExprs)
			{
				if (dice.GetType().Name.ToString().ToLower().Equals("d20") && dice.Sides == 1)
				{
					if (advDisAdv && advDisAdvCount > 0)
					{
						if (advDisAdvIndex < advDisAdvCount)
						{
							if (advDisAdvExpr[advDisAdvIndex].Equals("adv"))
							{
								rollResults.Add(dice.RollAdv());
								System.Console.WriteLine("Rolled with adv!");
							}
							else
							{
								rollResults.Add(dice.RollDisadv());
								System.Console.WriteLine("Rolled with disadv!");
							}
							advDisAdvIndex += 1;
						}
					}
					else if (adv && advCount > 0)
					{
						rollResults.Add(dice.RollAdv());
						System.Console.WriteLine("Rolled with adv!");
						advCount -= 1;
					}
					else if (disAdv && disAdvCount > 0)
					{
						rollResults.Add(dice.RollDisadv());
						System.Console.WriteLine("Rolled with disadv!");
						disAdvCount -= 1;
					}
				}
				else
				{
					rollResults.Add(dice.Roll());
				}
			}

			// replace dice expressions with results
			for (int i = 0, j = 0; i < roll.Length; i++, j++)
			{
				if (j < DiceExprs.Count && j >= 0)
				{
					int cutStart = roll.IndexOf(DiceExprs[j].Expression);
					int cutLength = DiceExprs[j].Expression.Length;
					i = cutStart;
					string diceExpr = roll.Substring(cutStart, cutLength);
					string rollResult = rollResults[j].ToString();
					StringBuilder finalExpr = new StringBuilder();
					finalExpr = finalExpr
						.Append(roll.Substring(0, cutStart))
						.Append(rollResult)
						.Append(roll.Substring(cutStart + cutLength, roll.Length - (cutStart + cutLength)));
					roll = finalExpr.ToString();
				}
				else
				{
					break;
				}
			}


			// evaluate whole expression
			Eval evaluation = new Common.Eval();
			System.Console.WriteLine(Math.Round(evaluation.Execute(roll), 0).ToString());


		}

			System.Console.WriteLine(roll);
        }

		
		private static List<Dice> diceExprs = new List<Dice>();
		public static List<Dice> DiceExprs
		{
			get { return diceExprs; }
			set { diceExprs = value; }
		}	


		// supporting methods
		// Find searches for single dice expression (###d###) in string. If found - stores it in diceExprs list and returns index of expression's last char. If not - returns -1;
		private static int Find (string str, int index)
		{
			int diceIndex = str.IndexOf('d', index);
			if (diceIndex >= 0)
			{
				int rollCount;
				bool isRollCount;
				if (CheckBackwards(str, diceIndex) == diceIndex)
				{
					rollCount = 1;
					isRollCount = false;
				}
				else
				{
					rollCount = int.Parse(str.Substring(CheckBackwards(str, diceIndex), diceIndex - CheckBackwards(str, diceIndex)));
					isRollCount = true;
				}
				int sides = int.Parse(str.Substring(diceIndex + 1, CheckForward(str, diceIndex) - diceIndex));
				if (sides != 20)
				{
					diceExprs.Add(new AnyDice(rollCount, sides, isRollCount));
				}
				else
				{
					diceExprs.Add(new D20(rollCount, sides, isRollCount));
				}
				
				return CheckForward(str, diceIndex);
			}
			else
			{
				return diceIndex;
			}
			
		}

		// CheckBackwards returns index of dice expression's first char
		private static int CheckBackwards(string str, int index)
		{
			int cutStart = index, temp, j = 1;

			while (true)
			{
				if (index - j >= 0)
				{
					if (int.TryParse(str[index - j].ToString(), out temp))
					{
						cutStart--;

						if (index - j > 0)
						{
							j++;
						}
						else if (index - j == 0)
						{
							break;
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
			}
			return cutStart;
		}

		// CheckForward returns index of dice expression's last char
		private static int CheckForward(string str, int index)
		{
			int cutEnd = index, temp, j = 1;

			while (true)
			{
				if (index + j <= str.Length - 1)
				{
					if (int.TryParse(str[index + j].ToString(), out temp))
					{
						cutEnd++;

						if (index + j < str.Length - 1)
						{
							j++;
						}
						else if (index + j == str.Length - 1)
						{
							break;
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
			}
			return cutEnd;
		}
	}
}
