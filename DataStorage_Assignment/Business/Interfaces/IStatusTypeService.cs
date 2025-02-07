using Business.Dtos;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<IResult> CreateStatusTypeAsync(StatusTypeRegistrationForm registrationForm);
    Task<IResult> GetAllStatusTypesAsync();
    Task<IResult> GetStatusTypeByIdAsync(int id);
    Task<IResult> GetStatusByStatusNameAsync(string statusName);
    Task<IResult> UpdateStatusTypeAsync(int id, StatusTypeUpdateForm updateForm);
    Task<IResult> DeleteStatusTypeAsync(int id);
    Task<IResult> CheckIfStatusExists(string statusName);
}