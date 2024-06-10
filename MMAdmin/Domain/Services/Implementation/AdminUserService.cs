
using Firebase.Database.Query;

namespace MMAdmin.Domain.Services.Implementation;

public class AdminUserService : IAdminUser
{
    private readonly  FirebaseClient _firebaseClient;
    public AdminUserService()
    {
        _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
    }



    public async Task<bool> RegisterAsync(AdminUserModel user)
    {
        var newUser = new AdminUserModel()
        {
            UserId = Guid.NewGuid(),
            Email = user.Email,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password
        };
        var result = await _firebaseClient
            .Child(nameof(AdminUserModel))
            .PostAsync(newUser);

        return result.Object != null;
    }
    public async Task<AdminUserModel> LoginAsync(string email)
    {
        try
        {
            var GetPerson = (await _firebaseClient.Child(nameof(AdminUserModel)).OnceAsync<AdminUserModel>())
            .Where(a => a.Object.Email == email).FirstOrDefault();

            if (GetPerson != null)
            {

                var content = GetPerson.Object;
                return content;

            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            return null;
        }
    }
}

