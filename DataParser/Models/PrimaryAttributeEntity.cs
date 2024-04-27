using System.ComponentModel.DataAnnotations;
using DataParser.Enums;

namespace DataParser.Models;

public class PrimaryAttributeEntity
{
    [Key]
    public PrimaryAttribute PrimaryAttributeId { get; set; }
    public string Name { get; set; }

    public List<HeroEntity> Heroes { get; set; } = new();
}