using System.ComponentModel.DataAnnotations;
using DocStore.Api.DocStore;

namespace DocStore.Api.ViewModels
{
    public class AddUserRequestVm
    {
        [Required(ErrorMessage = "Enter email address.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string UserEmailId { get; set; }
        public UserGender UserGender { get; set; }
    }
}
