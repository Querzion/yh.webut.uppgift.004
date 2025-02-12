using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static CustomerRegistrationForm CreateRegistrationForm() => new();

    public static StatusTypeEntity Create(StatusTypeRegistrationForm registrationForm) => new()
    {
        StatusName = registrationForm.StatusName
    };

    public static StatusType Create(StatusTypeEntity entity) => new()
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