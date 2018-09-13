using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.IO;

namespace Client
{
    class Graphic
    {
        public const string DATA_PATH = "Data/";
        public const string GRAPHIC_PATH = "Data/Graphics";
        public const string FILE_EXT = ".png";

        public struct Rect
        {
            public int Top;
            public int Left;
            public int Bottom;
            public int Right;

        }

        public static Rect TileView; 

        //Gráficos de Tileset Mapa sprite
        static Sprite[] tilesetSprite;
        static int numTilesets;

        public static void loadGameAssets()
        {
            checkTilesets();
        }

        public static void Render_Graphics()
        {
            
        }
                  
        public static void renderSprite(Sprite tmpSprite, RenderWindow target, int destX, int destY, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            tmpSprite.TextureRect = new IntRect(sourceX, sourceY, sourceWidth, sourceHeight);
            tmpSprite.Position = new SFML.System.Vector2f(destX, destY);
            target.Draw(tmpSprite);
        }

        static void DrawMapTile(int mapNum, int x, int y) {
            int i = 0;
            RectangleShape srcrect = new RectangleShape(new SFML.System.Vector2f(0, 0));
            srcrect.Size = new SFML.System.Vector2f(0, 0);

            TileView.Top = 0;
            TileView.Bottom = Map.Maps[1].maxY - 1;
            TileView.Left = 0;
            TileView.Right = Map.Maps[1].maxX - 1;

            for(i = (int)Map.LayerType.Ground; i <= (int)Map.LayerType.Mask2; i++) {
                if(Map.Maps[mapNum].Tile[x,y].Layer[i].Tileset > 0 & Map.Maps[mapNum].Tile[x,y].Layer[i].Tileset <= numTilesets) {
                    renderSprite(tilesetSprite[2], Program.gameWindow, (x * 32), (y * 32), 1, 0, 32, (128 / 4));
                }
            }
        }

        static void DrawMapGrid() {
           
        }

        static void checkTilesets()
        {
            Console.WriteLine("Carregando tilesets, aguarde um momento...");
            numTilesets = 1;
            while(File.Exists(GRAPHIC_PATH + "Tilesets/" + numTilesets + FILE_EXT))
            {
                numTilesets += 1;
            }

            Array.Resize(ref tilesetSprite, numTilesets);

            for (int i = 1; i < numTilesets; i++)
            {
                tilesetSprite[i] = new Sprite(new Texture(GRAPHIC_PATH + "Tilesets/" + numTilesets + FILE_EXT));
            }

            Console.WriteLine("Tilesets carregados com sucesso!");

        }

    }
}
