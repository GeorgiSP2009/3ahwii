namespace PhoneLookupConsoleEf.Models;

public class PhoneValidation
{
    public int Id { get; set; }
    public int PhoneRequestId { get; set; }
    public bool IsValid { get; set; }
    public string? CountryCode { get; set; }
    public string? CountryName { get; set; }
    public string? Carrier { get; set; }
    public string? LineType { get; set; }
    public string? InternationalFormat { get; set; }
    public PhoneRequest? PhoneRequest { get; set; }
}
