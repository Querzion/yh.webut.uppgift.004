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

        await _statusTypeRepository.BeginTransactionAsync();
        
        try
        {
            var statusTypeEntity = StatusTypeFactory.CreateEntityFrom(registrationForm);

            if (await _statusTypeRepository.AlreadyExistsAsync(x => x.StatusName == registrationForm.StatusName))
            {
                await _statusTypeRepository.RollbackTransactionAsync();
                return Result.AlreadyExists("This status type already exists.");
            }            
            
            var result = await _statusTypeRepository.CreateAsync(statusTypeEntity);

            if (result)
            {
                await _statusTypeRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _statusTypeRepository.RollbackTransactionAsync();
                return Result.Error("Unable to create status type.");
                
            }
        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
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
        await _statusTypeRepository.BeginTransactionAsync();
        
        try
        {
            var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.Id == id);
            if (statusTypeEntity == null)
            {
                await _statusTypeRepository.RollbackTransactionAsync();
                return Result.NotFound("Status not found.");
            }
            
            statusTypeEntity = StatusTypeFactory.Update(statusTypeEntity, updateForm);
            
            var result = await _statusTypeRepository.UpdateAsync(statusTypeEntity);

            if (result)
            {
                await _statusTypeRepository.CommitTransactionAsync();
                return Result.Ok();
            }
            else
            {
                await _statusTypeRepository.RollbackTransactionAsync();
                return Result.Error("Unable to update status type.");
            }
        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
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