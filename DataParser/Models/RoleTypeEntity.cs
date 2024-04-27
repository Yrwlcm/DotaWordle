using System.ComponentModel.DataAnnotations;
using DataParser.Enums;

namespace DataParser.Models;

public class RoleTypeEntity
{
    [Key]
    public RoleType RoleTypeId { get; set; }
    public string Name { get; set; }
    
    public List<RoleEntity> Roles { get; set; } = new();
}