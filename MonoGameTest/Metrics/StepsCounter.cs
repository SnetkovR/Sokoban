using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam;

namespace MonoGameTest.Metrics
{
    class StepsCounter 
    {
        private int count;

        public int Count => count;

        public StepsCounter(Sokoban game)
        {
            game.OnTurn += StepForward;
            game.RevertTurnEvent += StepBack;
            count = 0;
        }

        public void StepForward()
        {
            count++;
        }

        public void StepBack()
        {
            count--;
        }
    }
}
