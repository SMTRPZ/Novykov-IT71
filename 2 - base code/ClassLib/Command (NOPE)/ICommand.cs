using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Command
{
    public interface ICommand
    {
        string Execute(params Stone[] stone);
        string Undo();
    }
}
