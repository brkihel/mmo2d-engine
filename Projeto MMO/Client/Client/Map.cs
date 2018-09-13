using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Client
{
    class Map
    {
        public const int MAX_MAPS = 100;
        public const string MAP_PATH = "Data/Maps/";
        public const string MAP_EXT = ".map";

        public static MapStruct[] Maps = new MapStruct[MAX_MAPS];

        [Serializable]
        public struct TileDataStruct {
            public byte X;
            public byte Y;
            public byte Tileset;
            public byte Autotile;
        }

        [Serializable]
        public struct TileStruct {
            public TileDataStruct[] Layer;
            public byte Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public byte DirBlock;
        }
        
        [Serializable]
        public struct MapStruct {
            public string name;
            public byte maxX;
            public byte maxY;

            public TileStruct[,] Tile;
        }

        [Serializable]
        public enum LayerType {
            Ground = 1,
            Mask,
            Mask2,
            Fringe,
            Fringe2,
            Count
        }

        public static void checkMaps() {
            Console.WriteLine("Checando os mapas...");
            Array.Resize(ref Maps, MAX_MAPS);

            for (int i = 0; i < MAX_MAPS; i++) {
                if (!File.Exists(MAP_PATH + "map" + i + MAP_EXT)) {
                    ClearMap(i);
                    SaveMap(i);
                }
            }
        }

        public static void ClearMap(int mapNum) {
            int xx = 0;
            int yy = 0;

            Maps[mapNum].name = "NoName";
            Maps[mapNum].maxX = 10;
            Maps[mapNum].maxY = 10;
            Maps[mapNum].Tile = new TileStruct[(Maps[mapNum].maxX), (Maps[mapNum].maxY)];

            var mapX = Maps[mapNum].Tile.GetLength(0);
            var mapY = Maps[mapNum].Tile.GetLength(1);

            for(int X = 0; X < Maps[mapNum].maxX; X++) {
                mapX = X;
            }

            for (int Y = 0; Y < Maps[mapNum].maxY; Y++)
            {
                mapY = Y;
            }

            for(xx = 0; xx < Maps[mapNum].Tile.GetLength(0); xx++) {
                for(yy = 0; yy < Maps[mapNum].Tile.GetLength(0); yy++){
                    //Preencher a Array com o tipo do Layer para cada tipo de Layer disponível
                    Maps[mapNum].Tile[xx, yy].Layer = new TileDataStruct[(int)LayerType.Count -1];
                    Array.Resize(ref Maps[mapNum].Tile[xx, yy].Layer, (int)LayerType.Count -1);
                    Maps[mapNum].Tile[xx, yy].Layer[1].Tileset = 2;
                }
            }
        }

        public static void SaveMap(int mapNum) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(MAP_PATH + "map" + mapNum + MAP_EXT, FileMode.OpenOrCreate);
            bf.Serialize(fs, Maps[mapNum]);
            fs.Close();
        }

        public static void LoadMap(int mapNum) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(MAP_PATH + "map" + mapNum + MAP_EXT, FileMode.OpenOrCreate);
            Maps[mapNum] = (MapStruct)bf.Deserialize(fs);
            fs.Close();
        }

        public static void LoadMaps() {
            Console.WriteLine("Carregando os mapas...");
            for (int i = 0; i < MAX_MAPS; i++)
            {
                LoadMap(i);
            }

            Console.WriteLine("Mapas carregados com sucesso!");
        }
    }


}
