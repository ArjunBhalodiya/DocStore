using DocStore.Contract.Entities;

namespace DocStore.Contract.Repositories
{
    public interface IUserRepository
    {
        bool Validate(string userEmailId, string password);
        User FindByUserId(string userId);
        User FindByUserEmailId(string userEmailId);
        User Add(User user);
        bool ValidateEmailVerificationToken(string userId, string token);
        string GetEmailVerificationToken(string userId);
    }
}
