using CrudExample.Models;

namespace CrudExample.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
            GenerateRandom();
        }
        public UserModel Add(UserModel model)
        {
            GenerateRandom();

            model.Id = Guid.NewGuid().ToString();
            model.UpdatedDate = DateTime.Now;
            Values.Users.Add(model);

            return model;
        }

        public bool Delete(string id)
        {
            GenerateRandom();
            var model = Values.Users.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());   

            if(model == null) return false;

            Values.Users.Remove(model);
            return true;
        }

        public List<UserModel> Get()
        {
            GenerateRandom();
            return Values.Users.OrderByDescending(x=> x.UpdatedDate).ToList();
        }

        public UserModel? Get(string id)
        {
            GenerateRandom();
            return Values.Users.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());
        }

        public UserModel? Update(UserModel model)
        {
            GenerateRandom();
            var position = Values.Users
                .Select((x, index) => x.Id.ToLower() == model.Id.ToLower() ? index : -1)
                .Max();
            if(position < 0) return null;

            model.UpdatedDate = DateTime.Now;
            Values.Users[position] = model;

            return model;
        }

        void GenerateRandom()
        {
            if ( (Values.Users?.Count ?? 0) > 0) return;

            string[] maleNames = new string[] { "Aaron", "Abdul", "Abe", "Abel", "Abraham", "Adam", "Adan", "Adolfo", "Adolph", "Adrian", "Peter", "Joshep", "Jhon", "Kevin", "Bod" };
            string[] femaleNames = new string[] { "Abby", "Abigail", "Adele", "Adrian", "Cristina", "Mary", "Chatherine" };
            string[] lastNames = new string[] { "Abbott", "Acosta", "Adams", "Adkins", "Aguilar", "Mendez", "Perez" };
            Random rand = new Random(DateTime.Now.Second);

            for (int i = 0; i < 10; i++)
            {
                var firstName = "";
                
                if (rand.Next(1, 2) == 1)
                {
                    firstName = maleNames[rand.Next(0, maleNames.Length - 1)];
                }
                else
                {
                    firstName = femaleNames[rand.Next(0, femaleNames.Length - 1)];
                }
                
                var lastName = lastNames[rand.Next(0, lastNames.Length - 1)];

                Values.Users.Add(new UserModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"{firstName} {lastName}",
                    UserName = $"{firstName}.{lastName}",
                    Email = $"{firstName}.{lastName}@test.com",
                    UpdatedDate = DateTime.Now,
                    Phone = $"(829) {rand.Next(500, 999)}-{rand.Next(5000, 9999)}",
                    Password = $"Aa123657{rand.Next(1000, 9000)}$"
                }); ;
            }

            
        }
    }
}
