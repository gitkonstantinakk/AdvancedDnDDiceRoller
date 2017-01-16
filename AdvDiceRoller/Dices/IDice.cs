using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Dices
{
	public interface IDice
	{
		int Sides { get; }
		int Rolls { get; }
		string Expression { get; }
	}
}
