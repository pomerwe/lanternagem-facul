using lanternagem_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
    public interface IAccidentProvider
    {
        Task<(bool IsSuccess, Accident Accident, string ErrorMessage)> AddAccident(Accident accident);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteAccident(int accidentId);
        Task<(bool IsSuccess, Accident Accident, string ErrorMessage)> GetAccidentById(int id);
        Task<(bool IsSuccess, List<Accident> Accidents, string ErrorMessage)> GetAccidents();
        Task<(bool IsSuccess, Accident Accident, string ErrorMessage)> UpdateAccident(Accident accident);
    }
}
