using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Command
{
    public class GameCommand
    {
        ICommand[] commands;

        Stack<ICommand> commandsHistory;

        public GameCommand()
        {
            commands = new ICommand[2];
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i] = new NoCommand();
            }
            commandsHistory = new Stack<ICommand>();
        }

        public void SetCommand(int number, ICommand com)
        {
            commands[number] = com;
        }

        public string Execute(int number, params Stone[] stone)
        {
            commandsHistory.Push(commands[number]);

            return commands[number].Execute(stone);
        }

        public string Undo(int number)
        {
            if(commandsHistory.Count > 0)
            {
                ICommand undoCommand = commandsHistory.Pop();
                return undoCommand.Undo();
            }
            return "No any past state";
        }
    }
}
