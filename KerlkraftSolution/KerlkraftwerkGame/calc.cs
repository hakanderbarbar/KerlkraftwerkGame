using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerlkraftwerkGame
{
    public class Calc
    {
        public static int Diff(int x, int y)
        {
            if (x >= y)
            {
                return x - y;
            }
            else
            {
                return y - x;
            }
        }
    }
}