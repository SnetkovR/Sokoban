using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    interface ICell
    {
        ICreature onCell { get; set; }

        bool isTarget { get; }
    }
}
