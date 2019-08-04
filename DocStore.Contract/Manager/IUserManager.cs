using DocStore.Contract.Models;

namespace DocStore.Contract.Manager
{
    public interface IUserManager
    {
        bool Validate(string userEmailId, string password);
        UserDm FindByUserEmailId(string userEmailId);
        UserDm FindByUserId(string userId);
        UserDm Add(UserDm user);
        bool SendEmailVerificationLink(UserDm user);
        bool ValidateEmailVerificationToken(string userId, string token);
    }
}