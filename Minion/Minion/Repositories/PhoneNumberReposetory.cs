using MongoDB.Driver;

namespace Minion.Repositories
{
    public class PhoneNumberReposetory: IPhoneNumberReposetory
    {
        private IMongoDatabase db;
        public PhoneNumberReposetory()
        {

        }
    }
}
