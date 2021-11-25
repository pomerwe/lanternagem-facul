using lanternagem_api.DataTransferObjects;
using lanternagem_api.Domain;
using lanternagem_api.Models;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
    public interface ISystemManagementService
    {
        Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForCostumer(Customer costumer);
        Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForManager(Manager manager);
        Task<(bool IsSuccess, AuthenticatedDto AuthenticatedDto, string ErrorMessage)> Login(LoginDto loginDto);
    }
}
