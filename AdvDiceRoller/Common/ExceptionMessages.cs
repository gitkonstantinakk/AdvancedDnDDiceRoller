using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Common
{
	class ExceptionMessages
	{
		public const string CommandNull = "No command detected. Please, provide DiceRoller command.";
		public const string DiceDoesNotExist = "{0} dice does not exist! Only dice you can roll is one of following: d2, d3, d4, d6, d8, d10, d12, d20, d100 or d120";
		public const string IncorrectBrackets = "Wrongly used brackets!";
		public const string IncorrectOperators = "Wrongly used operators!";
		public const string InvalidNumberOfRolls = "Maximum possible number of rolls with single dice is 150.";
		public const string InvalidSubcommand = "Cant read your subcommand: {0}.";
		public const string TooManyAdvs = "You can perform only one \"adv\" command in one subcommand, but more than one in entire expression. Try use comma (\",\").";
		public const string TooManyDisAdvs = "You can perform only one \"disadv\" command in one subcommand, but more than one in entire expression. Try use comma (\",\").";
		public const string TooManyRolls = "You can perform only one \"roll\" command in entire expression.";
	}
}
