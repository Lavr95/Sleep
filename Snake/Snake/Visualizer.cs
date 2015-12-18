using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    public static class Visualizer
    {
        public enum TerrainInfo
        {
            Backcolor, Snake, Food, Head
        }
        public struct Terrain
        {
            public TerrainInfo Ti { get; set; }
            public Point Coords { get; set; }

            public Terrain(TerrainInfo ti, Point coords) :this()
            {
                Ti = ti;
                Coords = coords;
            }
        }
        public const int CellSize = 10;
        public static Size MapSize { get; set; }
        private static readonly Dictionary<TerrainInfo, Color> Dict = new Dictionary<TerrainInfo, Color>
                                                 {
                                                     {TerrainInfo.Backcolor, Color.Transparent},
                                                     {TerrainInfo.Food, Color.Red},
                                                     {TerrainInfo.Snake, Color.Black},
                                                     {TerrainInfo.Head, Color.Red}
                                                 };
        private static Bitmap _map;
        static Visualizer()
        {
            MapSize = new Size();
            
        }
        public static void Paint()
        {
            if (_map == null)
                _map = new Bitmap(MapSize.Width, MapSize.Height);
            using (var g = Graphics.FromImage(_map))
                g.Clear(Color.Transparent);
        }

        public static Bitmap Paint(List<Point> list)
        {
            Paint();
            var bm = _map;
            foreach (var point in list)
            {
                var p = new Point(point.X*CellSize, point.Y*CellSize);
                for (int i = 0; i < CellSize; i++)
                    for (int j = 0; j < CellSize; j++)
                        bm.SetPixel(p.X+i,p.Y+j,Color.Green);
            }
            return bm;
        }

        public static Bitmap PaintOnlySomething(List<Terrain> list)
        {
            foreach (var terrain in list)
            {
                var p = new Point(terrain.Coords.X * CellSize, terrain.Coords.Y * CellSize);
                for (int i = 0; i < CellSize; i++)
                    for (int j = 0; j < CellSize; j++)
                        _map.SetPixel(p.X + i, p.Y + j, Dict[terrain.Ti]);
            }
            return _map;
        }
    }
}
