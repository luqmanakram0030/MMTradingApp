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
        public IAsyncRelayCommand AddEmployeeCommand { get; }
        public IAsyncRelayCommand DeleteEmployeeCommand { get; }

        private readonly ISharedService _sharedService;

        [ObservableProperty]
        private bool isVisible;

        private readonly IEmployeeService _employeeService;

        [ObservableProperty]
        private EmployeeModel selectedEmployee;
        public AddEmployeeViewModel(ISharedService sharedService, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            this._sharedService = sharedService;
            
             SelectedEmployee = _sharedService.GetValue<EmployeeModel>("SelectedEmployee");
            if (SelectedEmployee != null)
                IsVisible = true;
            else
            {
                SelectedEmployee = new EmployeeModel();
                IsVisible = false;
            }
            // Initialize commands
            AddEmployeeCommand = new AsyncRelayCommand(AddEmployeeAsync);
            DeleteEmployeeCommand = new AsyncRelayCommand(DeleteEmployeeAsync);
        }
        private async Task AddEmployeeAsync()
        {
            try
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

                Common.BusyIndicator(false);
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
                    
                }
                MessagingCenter.Unsubscribe<string>(this, "Delete");
            });
        }
    }
}
