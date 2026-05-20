namespace PhoneLookupConsoleEf.Models;

public class PhoneRequest
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
    public string? Note { get; set; }
    public ICollection<PhoneValidation> Validations { get; set; } = new List<PhoneValidation>();
}
