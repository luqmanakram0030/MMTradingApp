
namespace MMAdmin.Domain.Services.Interface;

public interface IEmployeeService
{
    Task<List<EmployeeModel>> GetAllEmployeesAsync();
    Task<EmployeeModel> GetEmployeeByIdAsync(Guid userId);
    Task AddEmployeeAsync(EmployeeModel employee);
    Task UpdateEmployeeAsync(EmployeeModel employee);
    Task DeleteEmployeeAsync(Guid userId);
}

