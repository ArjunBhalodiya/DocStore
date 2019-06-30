using System;

namespace DocStore.Contract.Models
{
    public class UserDm
    {
        public string UserId { get; set; }
        public string UserEmailId { get; set; }
        public short UserGender { get; set; }
        public string UserProfilePicUrl { get; set; }
        public bool UserEmailIdVerified { get; set; }
        public bool UserIsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
