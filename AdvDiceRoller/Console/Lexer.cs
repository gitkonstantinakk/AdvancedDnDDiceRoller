using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdvDiceRoller.Console
{
	public static class Lexer
	{
		public static string ClearGeneralInvalidChars(this string str)
		{
			str = str.ToLower();
			str = Regex.Replace(str, @"[^a-z0-9+\-\/\*,&%\(\)]", "", RegexOptions.Compiled);

			// cases with operators repeated
			str = Regex.Replace(str, @"\++", "+", RegexOptions.Compiled);
			str = Regex.Replace(str, @"-+", "-", RegexOptions.Compiled);
			str = Regex.Replace(str, @"\*+", "*", RegexOptions.Compiled);
			str = Regex.Replace(str, @"\/{2,}", "//", RegexOptions.Compiled);
			str = Regex.Replace(str, @"&+", "&", RegexOptions.Compiled);
			str = Regex.Replace(str, @"%+", "%", RegexOptions.Compiled);
			str = Regex.Replace(str, @",+", ",", RegexOptions.Compiled);

			return str;
		}

		public static string ClearRoll(this string roll)
		{
			roll = Regex.Replace(roll, "roll", "", RegexOptions.Compiled);
			roll = Regex.Replace(roll, @"[^d0-9+\-\/\*%\(\)]", "", RegexOptions.Compiled);
			return roll;
		}

		public static string ClearAdv(this string roll)
		{
			roll = Regex.Replace(roll, "[^adv]", "", RegexOptions.Compiled);
			return roll;
		}

		public static string ClearDisAdv(this string roll)
		{
			roll = Regex.Replace(roll, "[^isadv]", "", RegexOptions.Compiled);
			return roll;
		}

		public static bool CheckBrackets(this string str)
		{
			int currIndexOB = 0;
			int currIndexCB = 0;
			bool result = true;

			if (str.IndexOf('(', currIndexOB) < 0 && str.IndexOf(')', currIndexCB) < 0)
			{
				result = true;
			}
			else
			{
				while (true)
				{
					if (str.IndexOf('(', currIndexOB) >= 0)
					{
						currIndexOB = str.IndexOf('(', currIndexOB);
						currIndexOB++;

						if (str.IndexOf(')', currIndexCB) < 0)
						{
							result = false;
							break;
						}
						else
						{
							currIndexCB = str.IndexOf(')', currIndexCB);
							currIndexCB++;

							if (currIndexCB < currIndexOB)
							{
								result = false;
								break;
							}
							else
							{
								result = true;
								break;
							}
						}
					}
					else
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		public static bool CheckOperators(this string str)
		{
			if ((new Regex(@"(\+|\-|\*|\/){2,}").Matches(str).Count > 0) || (new Regex(@"[\+\-\*\/]").Matches(str).Count == 0 && new Regex(@"d\d{1,3}").Matches(str).Count > 1))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
