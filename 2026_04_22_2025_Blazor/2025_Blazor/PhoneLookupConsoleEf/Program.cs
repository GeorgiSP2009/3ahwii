using Microsoft.EntityFrameworkCore;
using PhoneLookupConsoleEf.Data;
using PhoneLookupConsoleEf.Models;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite("Data Source=phone_lookup.db")
    .Options;

using var db = new AppDbContext(options);
db.Database.EnsureCreated();

Console.WriteLine("Phone Lookup EF Core Console");
Console.WriteLine("Database: phone_lookup.db");

while (true)
{
    PrintMenu();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await CreateAsync(db);
            break;
        case "2":
            await ReadAllAsync(db);
            break;
        case "3":
            await UpdateAsync(db);
            break;
        case "4":
            await DeleteAsync(db);
            break;
        case "0":
            Console.WriteLine("Bye!");
            return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

static void PrintMenu()
{
    Console.WriteLine();
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1 - Create phone request + validation");
    Console.WriteLine("2 - Read all entries");
    Console.WriteLine("3 - Update a validation");
    Console.WriteLine("4 - Delete a phone request");
    Console.WriteLine("0 - Exit");
    Console.Write("Your choice: ");
}

static async Task CreateAsync(AppDbContext db)
{
    Console.Write("Phone number: ");
    var phoneNumber = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(phoneNumber))
    {
        Console.WriteLine("Phone number is required.");
        return;
    }

    Console.Write("Note (optional): ");
    var note = Console.ReadLine();

    var request = new PhoneRequest
    {
        PhoneNumber = phoneNumber,
        RequestedAt = DateTime.UtcNow,
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim()
    };

    Console.Write("Is valid (true/false): ");
    var isValid = bool.TryParse(Console.ReadLine(), out var valid) && valid;

    Console.Write("Country code (e.g. AT): ");
    var countryCode = Console.ReadLine();
    Console.Write("Country name: ");
    var countryName = Console.ReadLine();
    Console.Write("Carrier: ");
    var carrier = Console.ReadLine();
    Console.Write("Line type: ");
    var lineType = Console.ReadLine();
    Console.Write("International format: ");
    var internationalFormat = Console.ReadLine();

    request.Validations.Add(new PhoneValidation
    {
        IsValid = isValid,
        CountryCode = string.IsNullOrWhiteSpace(countryCode) ? null : countryCode.Trim(),
        CountryName = string.IsNullOrWhiteSpace(countryName) ? null : countryName.Trim(),
        Carrier = string.IsNullOrWhiteSpace(carrier) ? null : carrier.Trim(),
        LineType = string.IsNullOrWhiteSpace(lineType) ? null : lineType.Trim(),
        InternationalFormat = string.IsNullOrWhiteSpace(internationalFormat) ? null : internationalFormat.Trim()
    });

    db.PhoneRequests.Add(request);
    await db.SaveChangesAsync();

    Console.WriteLine($"Saved request with Id {request.Id}.");
}

static async Task ReadAllAsync(AppDbContext db)
{
    var requests = await db.PhoneRequests
        .Include(r => r.Validations)
        .OrderByDescending(r => r.RequestedAt)
        .ToListAsync();

    if (requests.Count == 0)
    {
        Console.WriteLine("No entries found.");
        return;
    }

    foreach (var request in requests)
    {
        Console.WriteLine($"Request Id: {request.Id}");
        Console.WriteLine($"  Number: {request.PhoneNumber}");
        Console.WriteLine($"  RequestedAt (UTC): {request.RequestedAt:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"  Note: {request.Note ?? "-"}");

        foreach (var validation in request.Validations)
        {
            Console.WriteLine($"    Validation Id: {validation.Id}, Valid: {validation.IsValid}, Country: {validation.CountryName ?? "-"} ({validation.CountryCode ?? "-"}), Carrier: {validation.Carrier ?? "-"}, LineType: {validation.LineType ?? "-"}");
        }
    }
}

static async Task UpdateAsync(AppDbContext db)
{
    Console.Write("Validation Id to update: ");
    if (!int.TryParse(Console.ReadLine(), out var validationId))
    {
        Console.WriteLine("Invalid Id.");
        return;
    }

    var validation = await db.PhoneValidations.FirstOrDefaultAsync(v => v.Id == validationId);
    if (validation is null)
    {
        Console.WriteLine("Validation not found.");
        return;
    }

    Console.Write($"New carrier (current: {validation.Carrier ?? "-"}): ");
    var newCarrier = Console.ReadLine();
    Console.Write($"New line type (current: {validation.LineType ?? "-"}): ");
    var newLineType = Console.ReadLine();

    validation.Carrier = string.IsNullOrWhiteSpace(newCarrier) ? validation.Carrier : newCarrier.Trim();
    validation.LineType = string.IsNullOrWhiteSpace(newLineType) ? validation.LineType : newLineType.Trim();

    await db.SaveChangesAsync();
    Console.WriteLine("Validation updated.");
}

static async Task DeleteAsync(AppDbContext db)
{
    Console.Write("PhoneRequest Id to delete: ");
    if (!int.TryParse(Console.ReadLine(), out var requestId))
    {
        Console.WriteLine("Invalid Id.");
        return;
    }

    var request = await db.PhoneRequests.FirstOrDefaultAsync(r => r.Id == requestId);
    if (request is null)
    {
        Console.WriteLine("PhoneRequest not found.");
        return;
    }

    db.PhoneRequests.Remove(request);
    await db.SaveChangesAsync();
    Console.WriteLine("PhoneRequest and related validations deleted.");
}
