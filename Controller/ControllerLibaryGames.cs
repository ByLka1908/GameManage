using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager.Controller
{
    public class ControllerLibaryGames
    {
        #region Получение Id для combobox

        /// <summary>
        /// Получение Id Steam для ComboBox
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetSteamComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                types = entities.Steam.Select(x => x.YesOrNo).ToList();
                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }


        /// <summary>
        /// Получение Id Epic Games для ComboBox
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetEpicComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                types = entities.Epic.Select(x => x.YesOrNo).ToList();
                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }

        /// <summary>
        /// Получение Id Ubisoft Store
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetUbisoftComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                types = entities.Ubisoft.Select(x => x.YesOrNo).ToList();
                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }
        #endregion
    }
}
