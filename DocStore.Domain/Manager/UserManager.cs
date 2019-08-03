using DocStore.Contract.Entities;
using DocStore.Contract.Manager;
using DocStore.Contract.Models;
using DocStore.Contract.Repositories;
using DocStore.Domain.Helper;

namespace DocStore.Domain.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;
        private readonly EmailHelper emailHelper;

        public UserManager(IUserRepository _userRepository, EmailHelper _emailHelper)
        {
            userRepository = _userRepository;
            emailHelper = _emailHelper;
        }

        public bool ValidateUser(string userEmailId, string password)
        {
            return userRepository.ValidateUser(userEmailId, password);
        }

        public UserDm FindByUserId(string userId)
        {
            var user = userRepository.FindByUserId(userId);
            if (user == null)
            {
                return null;
            }

            return new UserDm
            {
                UserId = user.UserId,
                UserEmailId = user.UserEmailId,
                UserGender = user.UserGender,
                UserEmailIdVerified = user.UserEmailIdVerified,
                UserProfilePicUrl = user.UserProfilePicUrl,
                UserIsActive = user.UserIsActive,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn
            };
        }

        public UserDm FindByUserEmailId(string userEmailId)
        {
            var user = userRepository.FindByUserEmailId(userEmailId);
            if (user == null)
            {
                return null;
            }

            return new UserDm
            {
                UserId = user.UserId,
                UserEmailId = user.UserEmailId,
                UserGender = user.UserGender,
                UserEmailIdVerified = user.UserEmailIdVerified,
                UserProfilePicUrl = user.UserProfilePicUrl,
                UserIsActive = user.UserIsActive,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn
            };
        }

        public UserDm AddUser(UserDm user)
        {
            var addedUser = userRepository.AddUser(new User
            {
                UserId = user.UserId,
                UserEmailId = user.UserEmailId,
                UserGender = user.UserGender,
                UserEmailIdVerified = user.UserEmailIdVerified,
                UserProfilePicUrl = user.UserProfilePicUrl,
                UserIsActive = user.UserIsActive,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn
            });
            if (addedUser == null)
            {
                return null;
            }

            return new UserDm
            {
                UserId = addedUser.UserId,
                UserEmailId = addedUser.UserEmailId,
                UserGender = addedUser.UserGender,
                UserEmailIdVerified = addedUser.UserEmailIdVerified,
                UserProfilePicUrl = addedUser.UserProfilePicUrl,
                UserIsActive = addedUser.UserIsActive,
                CreatedOn = addedUser.CreatedOn,
                ModifiedOn = addedUser.ModifiedOn
            };
        }

        public bool SendEmailVerificationLink(UserDm user)
        {
            var token = userRepository.GetEmailVerificationToken(user.UserId);
            if (user == null)
            {
                return false;
            }

            return emailHelper.SendEmail(user.UserEmailId, string.Empty, string.Empty,
                              "Verify Email | DocStore", $"http://localhost:7000/users/{user.UserId}/email/verify/{token}");
        }

        public bool ValidateEmailVerificationToken(string userId, string token)
        {
            return userRepository.ValidateEmailVerificationToken(userId, token);
        }
    }
}