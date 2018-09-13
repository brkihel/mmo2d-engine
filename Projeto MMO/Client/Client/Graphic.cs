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

        //Gráficos de Tileset Mapa sprite
        static Sprite[] tilesetSprite;
        static int numTilesets;

        public static void loadGameAssets()
        {
            checkTilesets();
        }
                  
        public static void renderSprite(Sprite tmpSprite, RenderWindow target, int destX, int destY, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            tmpSprite.TextureRect = new IntRect(sourceX, sourceY, sourceWidth, sourceHeight);
            tmpSprite.Position = new SFML.System.Vector2f(destX, destY);
            target.Draw(tmpSprite);
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
