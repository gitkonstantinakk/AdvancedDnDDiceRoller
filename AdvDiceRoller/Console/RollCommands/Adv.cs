using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdvDiceRoller.Common;

namespace AdvDiceRoller.Console.RollCommands
{
	public static class Adv
	{
		public static bool ProcessAdv(List<string> adv, out int advCount)
		{
			adv.ForEach(cmd => cmd.ClearAdv());
			adv.ForEach(cmd =>
			{
				if (new Regex("adv").Matches(cmd).Count > 1)
				{
					throw new InvalidRollOperationException(ExceptionMessages.TooManyAdvs);
				}
			});
			adv.ForEach(cmd =>
			{
				if(!cmd.Equals("adv"))
				{
					throw new InvalidRollOperationException(String.Format(ExceptionMessages.InvalidSubcommand, cmd.ToString()));
				}
			});

			advCount = adv.Count;
			if (adv.Count > 0)
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
