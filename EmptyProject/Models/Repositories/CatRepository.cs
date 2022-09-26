using System.Collections.Generic;
using System.Linq;

namespace EmptyProject.Models.Repositories
{
    public class CatRepository : IAnimleReposiroty<Cat>
    {
        List<Cat> cats;

        public CatRepository()
        {

            cats = new List<Cat>() { 
                new Cat() { id = 1,name = "poussy", age = 5 }, 
                new Cat() { id = 2,name = "bagera", age = 3 }, 
                new Cat() { id = 3,name = "cat 3", age = 6 } 
            };
        }
        public Cat GetById(int id)
        {
            return cats.FirstOrDefault(ca => ca.id == id);
        }
    }
}
