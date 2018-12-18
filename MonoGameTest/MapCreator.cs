using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public static class MapCreator
    {
        public static Cell[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            // Вероятно нужно проверить верность карты. Или нет :hm:
            var result = new Cell[rows.Length, rows[0].Length];

            for (var x = 0; x < rows.Length; x++)
            {
                for (var y = 0; y < rows[x].Length; y++)
                {
                    result[x, y] = CreateCell(rows[x][y]);
                }
            }

            return result;
        }

        private static Cell CreateCell(char symbol)
        {
            if (symbol == 'T')
            {
                return new Cell(true, new Empty());
            }

            var creature = CreateCreatureBySymbol(symbol);
            return new Cell(false, creature);
        }

        private static ICreature CreateCreatureBySymbol(char symbol)
        {
            switch (symbol)
            {
                case 'P':
                    return CreateCreatureByTypeName("Player");
                case 'W':
                    return CreateCreatureByTypeName("Wall");
                case 'E':
                    return CreateCreatureByTypeName("Empty");
                case 'B':
                    return CreateCreatureByTypeName("Box");
                default:
                    throw new Exception($" Wrong character for ICreatur {symbol}");
            }
        }

        private static ICreature CreateCreatureByTypeName(string name)
        {
            switch (name)
            {
                case "Player":
                    return new Player();
                case "Box":
                    return new Box();
                case "Empty":
                    return new Empty();
                case "Wall":
                    return new Wall();
                default:
                    throw new Exception("Dunno");
            }
        }

    }
}
