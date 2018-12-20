using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Exam;

namespace MonoGameTest
{
    public class LevelManager
    {
        private readonly int numberOfLevels;

        public int NumberOfLevels => numberOfLevels;

        private string[] levels;

        public LevelManager()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Levels");
            var contents = Directory.EnumerateFiles(path, "*.txt").Select(File.ReadAllText).ToArray();
            levels = contents;
            numberOfLevels = contents.Length;
        }

        public Sokoban GetGame(int level)
        {
            if (level < 0)
            {
                throw new ArgumentException("Level msut positive number", nameof(level));
            }

            if (level > NumberOfLevels)
            {
                throw new ArgumentException("Unknown level", nameof(level));
            }
            var map = MapCreator.CreateMap(levels[level - 1]);
           return new Sokoban(map);
        }
    }
}