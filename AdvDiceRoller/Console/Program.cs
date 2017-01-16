using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Console
{
	class Program
	{
		static void Main()
		{
            //string input = @"roll d20 + d20";
            //CommandManager cmdMng = new CommandManager(input);
            //cmdMng.Handle();
            UI.StartReadingCommands();
		}
	}
}
