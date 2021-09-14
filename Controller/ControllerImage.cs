using GamesManager.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager.Controller
{
    class ControllerImage
    {
        /// <summary>
        /// Получение изображения
        /// </summary>
        /// <returns></returns>
        public static List<Image> GetImages()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            wanted_path += @"\\Image";

            string[] allfolders = Directory.GetFiles(wanted_path);

            List<string> allfolderJPG = allfolders.Where(c => (c.EndsWith(".jpg")) || (c.EndsWith(".jpeg")) || (c.EndsWith(".png"))).ToList();

            List<BL.Image> images = new List<Image>();

            foreach (var item in allfolderJPG)
            {
                images.Add(new Image { ImagePath = item, NameImage = Path.GetFileName(item) }); 
            }
            return images;
        }
    }
}
