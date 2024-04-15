using System.ComponentModel.DataAnnotations;

namespace Domain.Entity;

public abstract class BaseEntity
{
    [Key]
    
    public Guid id { get; set; }

    private DateTime? _createAt; public DateTime?
        CreatAt { get { return _createAt; } set { _createAt = (value == null ? DateTime.UtcNow : value) ; } } 
    public DateTime? UpdateAt { get; set; } 
}