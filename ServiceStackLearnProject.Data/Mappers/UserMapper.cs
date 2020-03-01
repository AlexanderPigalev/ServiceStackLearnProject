using System.Collections.Generic;
using System.Data;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStackLearnProject.Data.Mappers.Interfaces;
using ServiceStackLearnProject.Enteties;

namespace ServiceStackLearnProject.Data.Mappers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserMapper : IUserMapper
    {
        /// <summary>
        /// Соединение с БД (IoC)
        /// </summary>
        private IDbConnectionFactory _dbCon { get; set; }

        public UserMapper(IDbConnectionFactory dbCon)
        {
            _dbCon = dbCon;

            using (IDbConnection db = _dbCon.OpenDbConnection())
            {
                db.CreateTableIfNotExists(typeof(User));
            }
        }

        /// <summary>
        /// Сохранение пользователя в базу
        /// </summary>
        /// <param name="newUser">Данные пользователя</param>
        public void CreateUser(User newUser)
        {
            using (IDbConnection db = _dbCon.OpenDbConnection())
            {
                db.Insert(newUser);
            }
        }

        /// <summary>
        /// Проверка на существование пользователя в базе
        /// </summary>
        /// <param name="firstName">Имя пользователя</param>
        /// <param name="secondName">Фамилия пользователя</param>
        /// <returns>True, если пользователь найден, false, если не найден</returns>
        public bool Exist(string firstName, string secondName)
        {
            using (IDbConnection db = _dbCon.OpenDbConnection())
            {
                User findedUser = db.Single<User>(x => x.FirstName == firstName && x.SecondName == secondName);
                if (findedUser is null)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="user">Данные пользователя, которого необходимо удалить</param>
        public void DeleteUser(User user)
        {
            using (IDbConnection db = _dbCon.OpenDbConnection())
            {
                User currentUser = db.Single<User>(x => x.FirstName == user.FirstName && x.SecondName == user.SecondName);
                db.Delete<User>(currentUser);
            }
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public IEnumerable<User> Get()
        {
            using (IDbConnection db = _dbCon.OpenDbConnection())
            {
                return db.Select<User>();
            }
        }
    }
}
