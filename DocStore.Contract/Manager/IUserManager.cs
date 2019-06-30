using DocStore.Contract.Models;

namespace DocStore.Contract.Manager
{
    public interface IUserManager
    {
        bool ValidateUser(string userEmailId, string password);
        UserDm FindByUserEmailId(string userEmailId);
        UserDm FindByUserId(string userId);
        UserDm AddUser(UserDm user);
    }
}