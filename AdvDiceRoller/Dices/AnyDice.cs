using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Dices
{
	class AnyDice : Dice, IDice
	{
		public AnyDice(int rolls, int sides, bool isRollCount) : base(rolls, sides, isRollCount) { }
	}
}
