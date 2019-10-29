using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Factory_Method
{
    public class CyborgCreator : RobotCreator
    {
        public override Robot Create(string image)
        {
            return new Cyborg(image);
        }
    }
}
