using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdvDiceRoller.Common;

namespace AdvDiceRoller.Console.RollCommands
{
	class ResultProcessor
	{
		public static bool ProcessDc(List<string> dcExpr, out int dc)
		{
			dcExpr.ForEach(cmd =>
			{
				cmd = cmd.ClearAdv();

				if (dcExpr.Count() > 1)
				{
					throw new ArgumentException(ExceptionMessages.TooManyDCs);
				}

				if (new Regex("dc").Matches(cmd).Count > 1)
				{
					throw new ArgumentException(ExceptionMessages.TooManyDCs);
				}

				if (!cmd.Equals("dc"))
				{
					throw new ArgumentException(String.Format(ExceptionMessages.InvalidSubcommand, cmd.ToString()));
				}
			});

			dc = int.Parse(dcExpr[0].ExtractDCValue());

			if (dc > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
