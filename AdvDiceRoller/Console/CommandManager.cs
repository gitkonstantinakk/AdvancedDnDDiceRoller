using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvDiceRoller.Common;
using AdvDiceRoller.Console;
using AdvDiceRoller.Console.RollCommands;

namespace AdvDiceRoller.Console
{
	public class CommandManager
	{
		private string command;
		public string Command
		{
			get { return this.command; }
			private set { this.command = value; }
		}

		private string[] commands;
		public string[] Commands
		{
			get { return this.commands; }
			set { this.commands = value; }
		}

		private string comment;
		public string Comment
		{
			get { return this.comment; }
			private set { this.comment = value; }
		}

		public CommandManager(string command)
		{
			this.command = command;
		}

		public void Handle()
			// this method provides first character validation in command, recognizes and extracts subcommands in entire expression.
			// Then sends commands to further process in specific classes.
		{
			string cmdToProcess = this.Command;

			// 1. check if command is null or empty
			if (String.IsNullOrEmpty(cmdToProcess))
			{
				throw new ArgumentNullException(ExceptionMessages.CommandNull);
			}

			// 2. detect and extract comment / save comment in "comment" string / separate exact command from comment
			if (cmdToProcess.IndexOf("//") > 0)
			{
				this.Comment = cmdToProcess.Remove(0, cmdToProcess.IndexOf("//"));
				cmdToProcess = cmdToProcess.Substring(0, cmdToProcess.IndexOf("//"));
			}

			// 3. provide general char validation for whole command
			cmdToProcess = cmdToProcess.ClearGeneralInvalidChars();

			// 4. check operators
			if (!cmdToProcess.CheckOperators())
			{
				throw new ArgumentException(ExceptionMessages.IncorrectOperators);
			}

			// 5. save splited command in property array
			this.Commands = cmdToProcess.Split(',');

			
			if (Array.Exists(this.Commands, elem => elem.Contains("roll")))
			{
				this.HandleRoll(this.Commands, this.Comment);
			}
		}
	}
}
