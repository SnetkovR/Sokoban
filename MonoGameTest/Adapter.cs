using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam;

namespace MonoGameTest
{
    class Adapter
    {
        private string map;

        public string Map => map; 

        public Adapter(Cell[,] map)
        {
            ConvertToString(map);
        }

        private void ConvertToString(Cell[,] map)
        {
            var builder = new StringBuilder();
            var len = map.GetLength(0);
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    var cell = map[i, j];
                    if (!(cell.OnCell is Empty))
                    {
                        builder.Append(CreateSymbolByCreature(cell.OnCell));
                    }
                    else if (cell.IsTarget)
                    {
                        builder.Append(CreateSymbolByCreature());
                    }
                    else
                    {
                        builder.Append(CreateSymbolByCreature(cell.OnCell));
                    }
                }

                builder.Append("\r\n");
            }

            this.map = builder.ToString();
        }

        private string CreateSymbolByCreature(ICreature creature)
        {
            switch (creature)
            {
                case Player player:
                    return "P";
                case Box box:
                    return "B";
                case Empty empty:
                    return "E";
                case Wall wall:
                    return "W";
                default:
                    throw new Exception("wtf");
            }
        }

        private string CreateSymbolByCreature()
        {
            return "T";
        }
    }
}
