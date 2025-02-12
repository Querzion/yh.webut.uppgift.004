using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static CustomerRegistrationForm Create() => new();

    public static StatusTypeEntity Create(StatusTypeRegistrationForm registrationForm) => new()
    {
        StatusName = registrationForm.StatusName
    };

    public static StatusType Create(StatusTypeEntity entity) => new()
    {
        Id = entity.Id,
        StatusName = entity.StatusName
    };

    public static StatusTypeUpdateForm Create(StatusType statusType) => new()
    {
        Id = statusType.Id,
        StatusName = statusType.StatusName
    };

    public static StatusTypeEntity Create(StatusTypeEntity statusTypeEntity, StatusTypeUpdateForm updateForm) => new()
    {
        Id = statusTypeEntity.Id,
        StatusName = updateForm.StatusName
    };
}