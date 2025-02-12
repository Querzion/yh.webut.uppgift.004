using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static UserRegistrationForm CreateRegistrationForm() => new();
    public static UserUpdateForm CreateUpdateForm() => new();
    
    public static UserEntity CreateEntityFrom(UserRegistrationForm registrationForm) => new()
    {
        FirstName = registrationForm.FirstName,
        LastName = registrationForm.LastName,
        Email = registrationForm.Email.ToLower()
    };
    
    public static User CreateOutputModel(UserEntity userEntity) => new()
    {
        Id = userEntity.Id,
        FirstName = userEntity.FirstName,
        LastName = userEntity.LastName,
        Email = userEntity.Email
    };
    public static User CreateOutputModelFrom(UserEntity userEntity) => new()
    {
        Id = userEntity.Id,
        FirstName = userEntity.FirstName,
        LastName = userEntity.LastName,
        Email = userEntity.Email
    };
    
    public static UserUpdateForm CreateUpdateForm(User user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
    };
    
    public static UserEntity Update(UserEntity userEntity, UserUpdateForm updateForm)
    {
        userEntity.FirstName = updateForm.FirstName;
        userEntity.LastName = updateForm.LastName;
        userEntity.Email = updateForm.Email;
        
        return userEntity;
    }
}