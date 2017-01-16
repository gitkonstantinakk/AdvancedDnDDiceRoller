using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Dices
{
	class AnyDice : DiceExpr, IDice
	{
		public AnyDice(int rolls, int sides, bool isRollCount) : base(rolls, sides, isRollCount) { }
	}
}
