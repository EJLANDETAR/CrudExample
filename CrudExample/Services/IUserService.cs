using CrudExample.Models;

namespace CrudExample.Services
{
    public interface IUserService
    {
        List<UserModel> Get();
        UserModel? Get(string id);
        UserModel Add(UserModel model); 
        UserModel? Update(UserModel model);
        bool Delete(string id);
    }
}
