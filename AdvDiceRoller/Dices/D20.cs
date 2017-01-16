using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvDiceRoller.Common;

namespace AdvDiceRoller.Dices
{
	public class D20 : DiceExpr, IDice, IAdv
	{
		public D20(int rolls, int sides, bool isRollCount) : base(rolls, sides, isRollCount) { }
	}
}
