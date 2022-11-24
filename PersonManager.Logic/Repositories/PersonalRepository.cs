using PersonManager.Logic.Models;

namespace PersonManager.Logic.Repositories
{
    public class PersonalRepository : Repository<Models.Person>
    {
        public PersonalRepository()
            : base()
        {

        }
        public override Person Create()
        {
            return new Person();
        }
    }
}
