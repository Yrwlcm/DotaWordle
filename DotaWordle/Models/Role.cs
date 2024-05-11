namespace DotaWordle.Models;

public record Role : IComparable<Role>
{
    public string Name { get; set; }
    public short Level { get; set; }

    public int CompareTo(Role? other)
    {
        if (ReferenceEquals(this, other)) 
            return 0;
        
        if (ReferenceEquals(null, other)) 
            return 1;
        
        if (!string.Equals(Name, other.Name))
            throw new ArgumentException("Roles must have the same name.");
        
        return Level.CompareTo(other.Level);
    }
}