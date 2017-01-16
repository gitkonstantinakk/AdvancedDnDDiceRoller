using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdvDiceRoller.Common;

namespace AdvDiceRoller.Console.RollCommands
{
	class AdvDisAdv
	{
		public static bool ProcessAdvDisAdv(List<string> advDisAdv, out int advDisAdvCount)
		{
			advDisAdv.ForEach(cmd => 
			{
				cmd.ClearDisAdv();

				if (new Regex("disadv").Matches(cmd).Count > 1)
				{
					throw new ArgumentException(ExceptionMessages.TooManyDisAdvs);
				}

				if (new Regex("adv").Matches(cmd).Count > 1)
				{
					throw new ArgumentException(ExceptionMessages.TooManyDisAdvs);
				}
				if (!cmd.Equals("adv") && !cmd.Equals("disadv"))
				{
					throw new ArgumentException(String.Format(ExceptionMessages.InvalidSubcommand, cmd.ToString()));
				}
			});

			advDisAdvCount = advDisAdv.Count;

			if (advDisAdv.Count > 0)
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
