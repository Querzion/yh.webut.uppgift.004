using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;


    public async Task<IResult> CreateStatusTypeAsync(StatusTypeRegistrationForm registrationForm)
    {
        if (registrationForm == null)
            return Result.BadRequest("Invalid status type registration.");
        
        try
        {
            if (await _statusTypeRepository.AlreadyExistsAsync(x => x.StatusName == registrationForm.StatusName))
                return Result.AlreadyExists("This status type already exists.");
            
            var statusTypeEntity = StatusTypeFactory.CreateEntityFrom(registrationForm);
            
            var result = await _statusTypeRepository.CreateAsync(statusTypeEntity);
            return result ? Result.Ok() : Result.Error("Unable to create status type.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> GetAllStatusTypesAsync()
    {
        var statusTypeEntities = await _statusTypeRepository.GetAllAsync();
        var statusType = statusTypeEntities?.Select(StatusTypeFactory.CreateOutputModel);
        return Result<IEnumerable<StatusType>>.Ok(statusType);
    }

    public async Task<IResult> GetStatusTypeByIdAsync(int id)
    {
        var entity = await _statusTypeRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return Result.NotFound("Status not found.");
        
        var status = StatusTypeFactory.CreateOutputModelFrom(entity);
        return Result<StatusType>.Ok(status);
    }

    public async Task<IResult> GetStatusByStatusNameAsync(string statusName)
    {
        var entity = await _statusTypeRepository.GetAsync(x => x.StatusName == statusName);
        if (entity == null)
            return Result.NotFound("Status not found.");
        
        var status = StatusTypeFactory.CreateOutputModelFrom(entity);
        return Result<StatusType>.Ok(status);
    }

    public async Task<IResult> UpdateStatusTypeAsync(int id, StatusTypeUpdateForm updateForm)
    {
        var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.Id == id);
        if (statusTypeEntity == null)
            return Result.NotFound("Status not found.");
        
        try
        {
            statusTypeEntity = StatusTypeFactory.Update(statusTypeEntity, updateForm);
            
            var result = await _statusTypeRepository.UpdateAsync(statusTypeEntity);
            return result ? Result.Ok() : Result.Error("Status type was not updated.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> DeleteStatusTypeAsync(int id)
    {
        var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.Id == id);
        if (statusTypeEntity == null)
            return Result.NotFound("Status not found.");
        
        try
        {
            var result = await _statusTypeRepository.DeleteAsync(statusTypeEntity);
            return result ? Result.Ok() : Result.Error("Status type was not deleted.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }

    public async Task<IResult> CheckIfStatusExists(string statusName)
    {
        var entity = await _statusTypeRepository.GetAsync(x => x.StatusName == statusName);
        if (entity == null)
            return Result.NotFound("Status type not found.");
        
        try
        {
            var statusType = StatusTypeFactory.CreateOutputModelFrom(entity);
            return Result<StatusType>.Ok(statusType);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
}