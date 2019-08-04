using DocStore.Api.ViewModels;
using DocStore.Contract.Manager;
using DocStore.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocStore.Api.Controllers
{
    [Route("Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UsersController(IUserManager _userManager)
        {
            userManager = _userManager;
        }

        /// <summary>
        /// This endpoint used to check email address is valid or not.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns>Returns true or false.</returns>
        /// <response code="200">If email address is registered in database return record.</response>
        /// <response code="204">If email address is not registered in database return null.</response> 
        /// <response code="500">If something goes wrong while fetching email address return null.</response>
        [HttpGet("email/{emailId}")]
        public IActionResult GetUserByEmailId(string emailId)
        {
            var user = userManager.FindByUserEmailId(emailId);
            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        /// <summary>
        /// This endpoint used to get workspace based on current user.
        /// </summary>
        /// <returns>Returns list of workspace.</returns>
        /// <response code="200">If user id is registered in database return record.</response>
        /// <response code="204">If user id is not registered in database return null.</response> 
        /// <response code="500">If something goes wrong while fetching record based on user id return null.</response>
        [HttpGet("{userId}")]
        public IActionResult GetUserByUserId(string userId)
        {
            var user = userManager.FindByUserId(userId);
            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        /// <summary>
        /// This endpoint used to add user.
        /// </summary>
        /// <param name="addUserRequestVm"></param>
        /// <returns>Returns added user.</returns>
        /// <response code="200">If added successfully in database, sent email verification link and return user record.</response>
        /// <response code="409">If user already exist in database return conflict.</response> 
        /// <response code="500">If something goes wrong while adding user in database return null.</response>
        [HttpPost]
        public IActionResult AddUser(AddUserRequestVm addUserRequestVm)
        {
            var user = userManager.FindByUserEmailId(addUserRequestVm.UserEmailId);
            if (user != null)
            {
                return Conflict();
            }

            var addedUser = userManager.Add(new UserDm
            {
                UserEmailId = addUserRequestVm.UserEmailId,
                UserGender = (short)addUserRequestVm.UserGender,
                UserEmailIdVerified = false,
                UserIsActive = true
            });

            if (addedUser == null)
            {
                return StatusCode(500);
            }

            if (!userManager.SendEmailVerificationLink(addedUser))
            {
                return StatusCode(500);
            }

            return Ok(addedUser);
        }

        /// <summary>
        /// This endpoint used to add user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>Returns added user.</returns>
        /// <response code="200">If added successfully in database, sent email verification link and return user record.</response>
        /// <response code="409">If user already exist in database return conflict.</response> 
        /// <response code="500">If something goes wrong while adding user in database return null.</response>
        [HttpGet("{userId}/email/verify/{token}")]
        public IActionResult VerifyEmail(string userId, string token)
        {
            var user = userManager.FindByUserId(userId);
            if (user == null)
            {
                return BadRequest();
            }

            var verifyToken = userManager.ValidateEmailVerificationToken(userId, token);
            if (!verifyToken)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}