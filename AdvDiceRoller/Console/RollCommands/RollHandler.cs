using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Console.RollCommands
{
	public static class RollHandler
	{
		public static void HandleRoll(this CommandManager cmdMng, string[] commands, string comment)
		{
			
			// catch advantage
			var adv =
				from command in commands
				where command.Contains("adv") && !command.Contains("disadv")
				select command;

			List<string> advExpr = new List<string>(adv);
			int advCount = 0;
			bool isAdv = false;
			if (advExpr.Count > 0)
			{
				isAdv = Adv.ProcessAdv(advExpr, out advCount);
			}

			// catch disadvantage
			var disadv =
				from command in commands
				where command.Contains("disadv")
				select command;

			List<string> disAdvExpr = new List<string>(disadv);
			int disAdvCount = 0;
			bool isDisAdv = false;
			if (disAdvExpr.Count > 0)
			{
				isDisAdv = DisAdv.ProcessDisAdv(disAdvExpr, out disAdvCount);
			}

			// catch advantage mixed with disadvantage, if both occur
			List<string> advDisAdvExpr = new List<string>();
			int advDisAdvCount = 0;
			bool isAdvDisAdv = false;
			if (advExpr.Count > 0 && disAdvCount > 0)
			{
				var advDisAdv =
					from command in commands
					where (command.Contains("adv") && !command.Contains("disadv")) || command.Contains("disadv")
					select command;
				advDisAdvExpr = new List<string>(advDisAdv);
				if (advDisAdvExpr.Count > 0)
				{
					isAdvDisAdv = AdvDisAdv.ProcessAdvDisAdv(advDisAdvExpr, out advDisAdvCount);
				}
			}

			var dc =
				(from command in commands
				where command.Contains("dc")
				select command).ToList();

			var noCrit =
				from command in commands
				where command.Contains("nocritical")
				select command;

			var onlyCritS =
				from command in commands
				where command.Contains("onlycriticalsuccess")
				select command;

			var onlyCritF =
				from command in commands
				where command.Contains("onlycriticalfailure")
				select command;

			// general roll handler
			var roll =
				from command in commands
				where command.Contains("roll")
				select command;

			List<string> rollExpr = new List<string>(roll);
			Roll.ProcessRoll(rollExpr, isAdv, advCount, isDisAdv, disAdvCount, isAdvDisAdv, advDisAdvCount, advDisAdvExpr, dc);
		}
	}
}
