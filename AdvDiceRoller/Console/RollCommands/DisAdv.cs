using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdvDiceRoller.Common;

namespace AdvDiceRoller.Console.RollCommands
{
	public static class DisAdv
	{
		public static bool ProcessDisAdv(List<string> disAdv, out int disAdvCount)
		{
			disAdv.ForEach(cmd => 
			{
				cmd.ClearDisAdv();

				if (new Regex("disadv").Matches(cmd).Count > 1)
				{
					throw new ArgumentException(ExceptionMessages.TooManyDisAdvs);
				}

				if (!cmd.Equals("disadv"))
				{
					throw new ArgumentException(String.Format(ExceptionMessages.InvalidSubcommand, cmd.ToString()));
				}
			});

			disAdvCount = disAdv.Count;

			if (disAdv.Count > 0)
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
