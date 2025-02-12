using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static StatusTypeRegistrationForm CreateRegistrationForm() => new();
    public static StatusTypeUpdateForm CreateUpdateForm() => new();

    public static StatusTypeEntity CreateEntityFrom(StatusTypeRegistrationForm registrationForm) => new()
    {
        StatusName = registrationForm.StatusName
    };

    public static StatusType CreateOutputModel(StatusTypeEntity entity) => new()
    {
        Id = entity.Id,
        StatusName = entity.StatusName
    };
    public static StatusType CreateOutputModelFrom(StatusTypeEntity entity) => new()
    {
        Id = entity.Id,
        StatusName = entity.StatusName
    };

    public static StatusTypeUpdateForm CreateUpdateForm(StatusType statusType) => new()
    {
        Id = statusType.Id,
        StatusName = statusType.StatusName
    };

    public static StatusTypeEntity Update(StatusTypeEntity statusTypeEntity, StatusTypeUpdateForm updateForm)
    {
        statusTypeEntity.StatusName = updateForm.StatusName;
        
        return statusTypeEntity;
    }
}