namespace SnowApi.Core.Entities;

public class Customer
{
    public int Id { get; set; }
    public required string UniqueId { get; set; }
    public required string Name { get; set; }
    public required string EmailAddress { get; set; }
}