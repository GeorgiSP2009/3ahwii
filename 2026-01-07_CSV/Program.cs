using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

var records = new List<Person>();

using (var reader = new StreamReader("persons.csv", System.Text.Encoding.UTF8))
using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", MissingFieldFound = null }))
{
    csv.Context.RegisterClassMap<PersonMap>();
    records = csv.GetRecords<Person>().ToList();
}

Console.WriteLine($"{"Fullname",-30} | {"Email",-35} | {"Telefon",-15} | {"Adresse",-60} | {"Unicode",-5}");
Console.WriteLine(new string('-', 30 + 35 + 15 + 60 + 5 + 4 * 3)); // separators

foreach (var person in records)
{
    Console.WriteLine($"{person.Fullname,-30} | {person.Email,-35} | {person.Telefon,-15} | {person.Adresse,-60} | {person.Unicode,-5}");
}

public class Person
{
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? Adresse { get; set; }
    public string? Unicode { get; set; }
}

public class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        Map(m => m.Fullname).Name("Fullname");
        Map(m => m.Email).Name("Email");
        Map(m => m.Telefon).Name("Telefon");
        Map(m => m.Adresse).Name("Adresse");
        Map(m => m.Unicode).Name("unicode");
    }
}