using System.ComponentModel.DataAnnotations;

namespace WmsApi.Database.Models;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
}