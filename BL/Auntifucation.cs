using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager.BL
{
    class Auntifucation
    {
        /// <summary>
        /// Аунтификатор
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static DB.User AuntifucationUser(string login, string password)
        {
            try
            {
                DB.EZEntities entities = new DB.EZEntities();
                var user = entities.User.Single(x => x.Login == login && x.Password == password);
                if(user != null)
                {
                    DB.Logs logs = new DB.Logs() { UserLogin = user.Login, Data = DateTime.Now };
                    entities.Logs.Add(logs);
                    entities.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception($"User ne naiden");
                }
            }
            catch
            {
                throw new Exception($"Ohibka v bd");
            }
        }
    }
}
