using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvDiceRoller.Common;

namespace AdvDiceRoller.Dices
{
	public abstract class DiceExpr : IDice, IAdv
	{
		private static readonly int[] diceTypes = { 2, 3, 4, 6, 8, 10, 12, 20, 100, 120 };

		private int sides;

		public int Sides
		{
			get { return this.sides; }
			private set
			{
				if ((value.ToString().Length > 3) || !DiceExist(value))
				{
					throw new ArgumentException(String.Format(ExceptionMessages.DiceDoesNotExist, "d" + value));
				}
				else
				{
					this.sides = value;
				}
			}
		}

		private int rolls;

		public int Rolls
		{
			get { return this.rolls; }
			set
			{
				if (value > 150)
				{
					throw new ArgumentException(ExceptionMessages.InvalidNumberOfRolls);
				}
				this.rolls = value;
			}
		}

		private string expression;
		public string Expression
		{
			get { return this.expression; }
		}

		public DiceExpr (int rolls, int sides, bool isRollCount)
		{
			this.Rolls = rolls;
			this.Sides = sides;

			if (isRollCount)
			{
				StringBuilder expr = new StringBuilder();
				expr.Append(this.Rolls.ToString() + "d" + this.Sides.ToString());
				this.expression = expr.ToString();
			}
			else
			{
				StringBuilder expr = new StringBuilder();
				expr.Append("d" + this.Sides.ToString());
				this.expression = expr.ToString();
			}
		}

		public virtual int Roll()
		{
			int result = 0;
			for (int i = 0; i < this.Rolls; i++)
			{
				result += rnd.Next(1, this.Sides + 1);
			}
			return result;
		}

		public int RollAdv()
		{
			int result1 = DiceExpr.rnd.Next(1, 21);
			int result2 = DiceExpr.rnd.Next(1, 21);
			return result1 > result2 ? result1 : result2;
		}

		public int RollDisadv()
		{
			int result1 = DiceExpr.rnd.Next(1, 21);
			int result2 = DiceExpr.rnd.Next(1, 21);
			return result1 < result2 ? result1 : result2;
		}

		// supporting properties
		internal static Random rnd = new Random();

		// supporting methods
		private bool DiceExist(int dice)
		{
			for (int i = 0; i < diceTypes.Length; i++)
			{
				if (dice.Equals(diceTypes[i]) == true)
				{
					return true;
				}
			}
			return false;
		}
	}
}
