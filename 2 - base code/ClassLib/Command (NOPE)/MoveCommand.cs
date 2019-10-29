using ClassLib.Decorator;
using ClassLib.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Command
{
    public class MoveCommand : ICommand
    {
        Game game;

        public MoveCommand(Game gm)
        {
            game = gm;
        }

        public string Execute(params Stone[] stone)
        {
            return "";
        }

        public string Undo()
        {
            return "";
        }
    }
}
