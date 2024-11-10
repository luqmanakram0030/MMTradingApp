using MMAdmin.Abstract;
using MMAdmin.Views.Popup;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.ViewModels.EmployeeManagement
{
    public partial class AddEmployeeViewModel : ObservableObject
    {
        
        public IAsyncRelayCommand DeleteEmployeeCommand { get; }

        private readonly ISharedService _sharedService;

        [ObservableProperty]
        private bool isVisible;
        [ObservableProperty]
        private string confirmPassword;

        private readonly IEmployeeService _employeeService;
        [ObservableProperty]
        private bool isPasswordVisible;
        [ObservableProperty]
        private EmployeeModel selectedEmployee;

        public AddEmployeeViewModel(ISharedService sharedService, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            this._sharedService = sharedService;

            SelectedEmployee = _sharedService.GetValue<EmployeeModel>("SelectedEmployee");
            if (SelectedEmployee != null)
            {
                ConfirmPassword=SelectedEmployee.Password;
            IsVisible = true;
        }
        else

        {
                SelectedEmployee = new EmployeeModel();
                IsVisible = false;
                
            }
            // Initialize commands
          
            DeleteEmployeeCommand = new AsyncRelayCommand(DeleteEmployeeAsync);
        }
        [RelayCommand]
        private async Task TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }
        [RelayCommand]
        private async Task AddEmployeeAsync(Object obj)
        {
            try
            {
                
                var page = obj as AddEmployee;
                var btnAddEmployee = page.FindByName("btnAddEmployee");
                if (!ValidationHelper.IsFormValid(SelectedEmployee, page))
                {
                    return;
                }

                await Common.ControlBounceEffect(btnAddEmployee);
                if (SelectedEmployee.Password == ConfirmPassword)
                {


                    Common.BusyIndicator(true);
                    if (SelectedEmployee == null)
                        return;
                    if (SelectedEmployee.UserId != Guid.Empty)
                    {
                        await _employeeService.UpdateEmployeeAsync(SelectedEmployee);
                    }
                    else
                        await _employeeService.AddEmployeeAsync(SelectedEmployee);

                    await Shell.Current.GoToAsync("..");
                    Common.BusyIndicator(false);
                }
                else
                {
                  Application.Current.MainPage.DisplayAlert("Error",  "Password and Confirm Password do not match", "OK");
                }
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }

        [RelayCommand]
        private async Task GoBackAsync(Object obj)
        {
            try
            {
                var page = obj as AddEmployee;
                var btnAddEmployee = page.FindByName("btngoback");
                

                await Common.ControlBounceEffect(btnAddEmployee);

                await Shell.Current.GoToAsync("..");
                
            }
            catch(Exception ex)
            {
                Common.BusyIndicator(false);
            }
        }

        private async Task DeleteEmployeeAsync()
        {
            await MopupService.Instance.PushAsync(new DeleteOnlyPopup(SelectedEmployee.UserId));
            MessagingCenter.Subscribe<string>(this, SelectedEmployee.UserId.ToString(), async (sender) =>
            {
                if (sender == "Delete")
                {
                    await _employeeService.DeleteEmployeeAsync(SelectedEmployee.UserId);
                    await Shell.Current.GoToAsync("..");
                    
                }
                MessagingCenter.Unsubscribe<string>(this, "Delete");
            });
        }
    }
}
