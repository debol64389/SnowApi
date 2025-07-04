namespace SnowApi.Core.DTOs;

public class MessageTemplateDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}