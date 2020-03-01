using System.Collections.Generic;
using ServiceStackLearnProject.Enteties;

namespace ServiceStackLearnProject.Data.Mappers.Interfaces
{
    public interface IUserMapper
    {
        void CreateUser(User newUser);
        bool Exist(string firstName, string secondName);
        void DeleteUser(User user);
        IEnumerable<User> Get();
    }
}