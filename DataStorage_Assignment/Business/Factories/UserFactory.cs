using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static UserRegistrationForm Create() => new();
    
    public static UserEntity Create(UserRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email
    };
    
    public static User Create(UserEntity user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
    };
    
    public static UserUpdateForm Create(User user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
    };
    
    public static UserEntity Create(UserUpdateForm form) => new()
    {
        Id = form.Id,
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email
    };
}