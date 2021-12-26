namespace VeganMap.Models;

public class BaseModel
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsSaved => Id != default;
}
