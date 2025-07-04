namespace SnowApi.Core.Entities;

public class MessageTemplate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}