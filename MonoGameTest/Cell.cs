using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class Cell
    {
        public ICreature OnCell { get; set; }

        public bool IsTarget { get; }

        public Cell(bool isTarget, ICreature onCell)
        {
            this.IsTarget = isTarget;
            this.OnCell = onCell;
        }
    }
}
