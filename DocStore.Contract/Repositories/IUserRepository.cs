using DocStore.Contract.Entities;

namespace DocStore.Contract.Repositories
{
    public interface IUserRepository
    {
        bool ValidateUser(string userEmailId, string password);
        User FindByUserId(string userId);
        User FindByUserEmailId(string userEmailId);
        User AddUser(User user);
        bool ValidateEmailVerificationToken(string userId, string token);
        string GetEmailVerificationToken(string userId);
    }
}
