using System.ComponentModel.DataAnnotations;

namespace Data.Interfaces;

public interface IEntity
{
    int Id { get; set; }
}