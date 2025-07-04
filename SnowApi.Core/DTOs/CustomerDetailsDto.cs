namespace SnowApi.Core.DTOs;

public class CustomerDetailsDto
{
    public required string UniqueId { get; set; }
    public required string Name { get; set; }
    public required string EmailAddress { get; set; }
}