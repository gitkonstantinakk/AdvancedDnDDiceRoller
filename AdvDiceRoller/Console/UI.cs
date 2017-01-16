using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvDiceRoller.Console
{
    public static class UI
    {
        enum GAMESTATE
        {
            gameOn,
            gameOff
        };


        public static string state = Convert.ToString(GAMESTATE.gameOn);

        public static void StartReadingCommands()
        {
            while (state == "gameOn")
            {
                System.Console.Write("Enter command for dice roll, or quit to exit:");
                string input = System.Console.ReadLine();

                if(input == "quit")
                {
                    QuitDiceRoller();
                }
                CommandManager cmdMng = new CommandManager("roll " + input);
                cmdMng.Handle();
            }
        }

        public static void QuitDiceRoller()
        {

            System.Console.WriteLine("Goodbye! Better luck next time");
            System.Console.Beep();
        }
    }
}
