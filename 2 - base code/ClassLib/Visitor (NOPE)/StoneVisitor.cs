using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib.Decorator;
using ClassLib.Factory_Method;

namespace ClassLib.Visitor
{
    public class StoneVisitor : IStoneVisitor
    {
        public string CyborgLiftsTheLoad(Cyborg cyborg, Stone stone)
        {
            throw new NotImplementedException();
        }

        public string ScientistLiftsTheLoad(Scientist scientist, Stone stone)
        {
            throw new NotImplementedException();
        }

        public string WorkerLiftsTheLoad(Worker worker, Stone stone)
        {
            //if (worker.CurrentWeight + stone.Weight <= worker.MaxWeight)
            //{
            //    return 
            //}
            //else
            //{
            //    return "You can`t lift this stone. Robot overload";
            //}
            throw new NotImplementedException();
        }
    }
}
