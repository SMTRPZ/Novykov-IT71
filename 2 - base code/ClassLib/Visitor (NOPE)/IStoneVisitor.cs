using ClassLib.Decorator;
using ClassLib.Factory_Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Visitor
{
    public interface IStoneVisitor
    {
        string WorkerLiftsTheLoad(Worker worker, Stone stone);

        string CyborgLiftsTheLoad(Cyborg cyborg, Stone stone);

        string ScientistLiftsTheLoad(Scientist scientist, Stone stone);
    }
}
