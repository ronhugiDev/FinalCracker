
using System.ComponentModel.DataAnnotations;

namespace Master.Dto
{
    public record PhoneNumberDto
    {
        public string FullNumber { get; set; }
        public string HashedNumber { get; set; }
    }
}
