using GamesManager.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager.BL
{
    class ViewGames
    {
        public DB.Games Games { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Discription { get; set; }

        public string Store { get; set; }

        public string StoreUbisoft { get; set; }

       
        public int SteamID { get; set; }
        public int EpicID { get; set; }
        public int UbisoftID { get; set; }


        public ViewGames(DB.Games games)
        {
            Games = games;

            Image = string.IsNullOrWhiteSpace(games.ImagePath) ? @"/Image\NoImage.jpg" : games.ImagePath;

            Name = $"{games.Name}";

            Price = $"Цена: {games.Price}";

            Discription = $"{games.Discription}";

            Store = $"В Steam:{games.Steam.YesOrNo} | В EpicGames:{games.Epic.YesOrNo}";

            StoreUbisoft = $"В UbisoftStore:{games.Ubisoft.YesOrNo}";

            SteamID = games.Id_Steam;
            EpicID = games.Id_Epic;
            UbisoftID = games.Id_Ubisoft;
        }
    }
}
