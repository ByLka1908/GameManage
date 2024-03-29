﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager.Controller
{
    class ControllerGame
    {
        /// <summary>
        /// Вывод данных из бд
        /// </summary>
        /// <returns></returns>
        public static List<BL.ViewGames> GetViewGames()
        {
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                var games = entities.Games.ToList();

                List<BL.ViewGames> view = new List<BL.ViewGames>();

                foreach(var item in games)
                {
                    view.Add(new BL.ViewGames(item));
                }
                return view;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }

        /// <summary>
        /// Добавление игры в бд
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="steam"></param>
        /// <param name="epic"></param>
        /// <param name="ubisoft"></param>
        /// <returns></returns>
        internal static bool AddGame(string name, string description, string price, 
            object steam, object epic, object ubisoft)
        {
            DB.Games games = new DB.Games();
            try
            {
                games = new DB.Games();
                games.Name = name;
                games.Price = Convert.ToInt32(price);
                games.Discription = description;
                games.Id_Steam = GetIdSteam(steam as string);
                games.Id_Epic = GetIdEpic(epic as string);
                games.Id_Ubisoft = GetIdUbisoft(ubisoft as string);
            }
            catch
            {
                throw new Exception("Ошибка входных данных");
            }
            if(games == null)
            {
                return false;
            }
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                entities.Games.Add(games);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("sdasd");
            }
            
        }

        #region Получение Id
        /// <summary>
        /// Получение Id Steam
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static int GetIdSteam(string name)
        {
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                return entities.Steam.Where(x => x.YesOrNo == name).First().Id;
            }
            catch
            {
                throw new Exception("Магазин не найден");
            }
        }

        /// <summary>
        /// Получение Id Epic Games
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static int GetIdEpic(string name)
        {
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                return entities.Epic.Where(x => x.YesOrNo == name).First().Id;
            }
            catch
            {
                throw new Exception("Магазин не найден");
            }
        }

        /// <summary>
        /// Получение Id Ubisoft Store
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static int GetIdUbisoft(string name)
        {
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                return entities.Ubisoft.Where(x => x.YesOrNo == name).First().Id;
            }
            catch
            {
                throw new Exception("Магазин не найден");
            }
        }
        #endregion

        /// <summary>
        /// Удаление игры
        /// </summary>
        /// <param name="games"></param>
        internal static void Remove(DB.Games games)
        {
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                entities.Games.Remove(entities.Games.Find(games.Id));
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Удаление не получилось");
            }
        }

        /// <summary>
        /// Изменение игры
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="steam"></param>
        /// <param name="epic"></param>
        /// <param name="ubisoft"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        internal static bool ChaneGame(string name, string description, string price, object steam, object epic, object ubisoft, DB.Games game)
        {
            DB.EZEntities entities = new DB.EZEntities();
            DB.Games games = entities.Games.Find(game.Id);
            try
            {
                games = new DB.Games();
                games.Name = name;
                games.Price = Convert.ToInt32(price);
                games.Discription = description;
                games.Id_Steam = GetIdSteam(steam as string);
                games.Id_Epic = GetIdEpic(epic as string);
                games.Id_Ubisoft = GetIdUbisoft(ubisoft as string);
            }
            catch
            {
                throw new Exception("Ошибка входных параметров");
            }
            if (games == null)
            {
                return false;
            }
            try
            {
                entities.Games.AddOrUpdate(games);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("eewq");
            }
        }
    }
}
