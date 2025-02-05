using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    // public static UserRegistrationForm Create() => new();
    
    public static UserEntity Create(UserRegistrationForm registrationForm) => new()
    {
        FirstName = registrationForm.FirstName,
        LastName = registrationForm.LastName,
        Email = registrationForm.Email.ToLower()
    };
    
    public static User Create(UserEntity userEntity) => new()
    {
        Id = userEntity.Id,
        FirstName = userEntity.FirstName,
        LastName = userEntity.LastName,
        Email = userEntity.Email
    };
    
    public static UserUpdateForm Create(User user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
    };
    
    public static UserEntity Create(UserEntity userEntity, UserUpdateForm updateForm) => new()
    {
        Id = userEntity.Id,
        FirstName = updateForm.FirstName,
        LastName = updateForm.LastName,
        Email = updateForm.Email
    };
}