using ClassLib.Decorator;
using ClassLib.Factory_Method;
using ClassLib.State;
using ClassLib.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Command
{
    public class StoneCommand : ICommand
    {
        Game game;

        public StoneCommand(Game gm)
        {
            game = gm;
        }

        public string Execute(params Stone[] stone)
        {
            return game.Robot.AddStone(stone);
        }

        public string Undo()
        {
            return game.Robot.DropStone();
        }
    }
}
