
namespace MMAdmin.Domain.Services.Implementation;

public class EmployeeService : IEmployeeService
{
    private readonly FirebaseClient _firebaseClient;

    public EmployeeService()
    {
        _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
    }

    public async Task<List<EmployeeModel>> GetAllEmployeesAsync()
    {
        var employees = (await _firebaseClient
            .Child("Employees")
            .OnceAsync<EmployeeModel>())
            .Select(item => new EmployeeModel
            {
                UserId = item.Object.UserId,
                FullName = item.Object.FullName,
                Email = item.Object.Email,
                PhoneNumber = item.Object.PhoneNumber,
                Password = item.Object.Password,
                Location = item.Object.Location,
                IsPresent = item.Object.IsPresent
            }).ToList();

        return employees;
    }

    public async Task<EmployeeModel> GetEmployeeByIdAsync(Guid userId)
    {
        var allEmployees = await GetAllEmployeesAsync();
        return allEmployees.FirstOrDefault(e => e.UserId == userId);
    }

    public async Task AddEmployeeAsync(EmployeeModel employee)
    {
        await _firebaseClient
            .Child("Employees")
            .PostAsync(employee);
    }

    public async Task UpdateEmployeeAsync(EmployeeModel employee)
    {
        var toUpdateEmployee = (await _firebaseClient
            .Child("Employees")
            .OnceAsync<EmployeeModel>())
            .FirstOrDefault(a => a.Object.UserId == employee.UserId);

        await _firebaseClient
            .Child("Employees")
            .Child(toUpdateEmployee.Key)
            .PutAsync(employee);
    }

    public async Task DeleteEmployeeAsync(Guid userId)
    {
        var toDeleteEmployee = (await _firebaseClient
            .Child("Employees")
            .OnceAsync<EmployeeModel>())
            .FirstOrDefault(a => a.Object.UserId == userId);

        await _firebaseClient
            .Child("Employees")
            .Child(toDeleteEmployee.Key)
            .DeleteAsync();
    }
}

