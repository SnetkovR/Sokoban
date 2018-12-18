using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class Resulter
    {
        public bool IsSuccess { get; }

        public Point NewPosition { get; }

        public Point OldPosition { get; }

        public Resulter InnerRequest { get; set; }


        public Resulter(Point newPosition, bool IsSuccess)
        {
            this.NewPosition = newPosition;
            this.IsSuccess = IsSuccess;
        }

        public Resulter(Point newPosition, Point oldPosition, bool IsSuccess, Resulter innerRequest)
        {
            this.NewPosition = newPosition;
            this.IsSuccess = IsSuccess;
            this.InnerRequest = innerRequest;
            this.OldPosition = oldPosition;
        }

        public Resulter(Point newPosition, Point oldPosition, bool IsSuccess)
        {
            this.NewPosition = newPosition;
            this.IsSuccess = IsSuccess;
            this.InnerRequest = InnerRequest;
            this.OldPosition = oldPosition;
        }

    }
}
