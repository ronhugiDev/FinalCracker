using Minion.Dtos;
using Minion.Entities;

namespace Minion
{
    public static class Extensions
    {
        public static PhoneNumberDto AsDto(this PhoneNumber number)
        {
            return new PhoneNumberDto
            {
                FullNumber = number.FullNumber,
                HashedNumber = number.HashedNumber
            };
        }
    }
}
