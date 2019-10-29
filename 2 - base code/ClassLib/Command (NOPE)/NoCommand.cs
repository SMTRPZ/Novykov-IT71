using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Command
{
    public class NoCommand : ICommand
    {
        string ICommand.Execute(params Stone[] stone)
        {
            throw new NotImplementedException();
        }

        string ICommand.Undo()
        {
            throw new NotImplementedException();
        }
    }
}
