using Dapper;
using Microsoft.Extensions.Options;
using SnowApi.Core.AppSettings;
using SnowApi.Core.Entities;
using SnowApi.Infrastructure.Interfaces;
using System.Data.SqlClient;

namespace SnowApi.Infrastructure;

public class RepositorySql : IRepositorySql
{
    private readonly ConnectionStrings _connectionStrings;

    public RepositorySql(
        IOptions<ConnectionStrings> connectionStrings)
    {
        _connectionStrings = connectionStrings.Value;
    }

    public Customer? PullCustomerDetails(string uniqueId)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           SELECT Id, UniqueId, Name, EmailAddress
                           FROM [dbo].[Customers]
                           WHERE [UniqueId] = @UniqueId
                           AND SetAsDeleted = 0
                           """;
        return connection.QuerySingleOrDefault<Customer?>(sql, new { UniqueId = uniqueId });
    }

    public void AddCustomer(Customer customer)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           INSERT INTO [dbo].[Customers]
                           ([UniqueId]
                           ,[Name]
                           ,[EmailAddress]
                           ,[SetAsDeleted])
                           VALUES
                           (@UniqueId
                           ,@Name
                           ,@EmailAddress
                           ,0);
                           """;
        connection.Execute(sql, new
        {
            customer.UniqueId,
            customer.Name,
            customer.EmailAddress
        });
    }

    /// <summary>
    /// Set customer as deleted in database
    /// </summary>
    /// <param name="uniqueId"></param>
    /// <returns></returns>
    public void DeleteCustomer(string uniqueId)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           IF EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [UniqueId] = @UniqueId)
                           BEGIN
                               UPDATE [dbo].[Customers]
                               SET [SetAsDeleted] = 1
                               WHERE [UniqueId] = @UniqueId
                           END
                           """;
        connection.Execute(sql, new { UniqueId = uniqueId });
    }

    public void UpdateCustomerEmail(string uniqueId, string newEmailAddress)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           IF EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [UniqueId] = @UniqueId)
                           BEGIN
                               UPDATE [dbo].[Customers]
                               SET [EmailAddress] = @NewEmailAddress
                               WHERE [UniqueId] = @UniqueId
                           END
                           """;
        connection.Execute(sql, new { NewEmailAddress = newEmailAddress, UniqueId = uniqueId });
    }

    public MessageTemplate? PullMessageTemplateDetails(int id)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           SELECT Id, Name, Subject, Body
                           FROM [dbo].[MessageTemplates]
                           WHERE [Id] = @Id
                           AND SetAsDeleted = 0
                           """;
        return connection.QuerySingleOrDefault<MessageTemplate?>(sql, new { Id = id });
    }

    public void AddMessageTemplate(MessageTemplate messageTemplate)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           INSERT INTO [dbo].[MessageTemplates]
                           ([Name]
                           ,[Subject]
                           ,[Body]
                           ,[SetAsDeleted])
                           VALUES
                           (@Name
                           ,@Subject
                           ,@Body
                           ,0);
                           """;
        connection.Execute(sql, new
        {
            messageTemplate.Name,
            messageTemplate.Subject,
            messageTemplate.Body
        });
    }

    public void UpdateMessageTemplateSubject(int id, string newSubject)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           IF EXISTS (SELECT 1 FROM [dbo].[MessageTemplates] WHERE [Id] = @Id)
                           BEGIN
                               UPDATE [dbo].[MessageTemplates]
                               SET [Subject] = @NewSubject
                               WHERE [Id] = @Id
                           END
                           """;
        connection.Execute(sql, new { NewSubject = newSubject, Id = id });
    }

    public void UpdateMessageTemplateBody(int id, string newBody)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           IF EXISTS (SELECT 1 FROM [dbo].[MessageTemplates] WHERE [Id] = @Id)
                           BEGIN
                               UPDATE [dbo].[MessageTemplates]
                               SET [Body] = @NewBody
                               WHERE [Id] = @Id
                           END
                           """;
        connection.Execute(sql, new { NewBody = newBody, Id = id });
    }

    /// <summary>
    /// Set message template as deleted in database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void DeleteMessageTemplate(int id)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);

        const string sql = """
                           IF EXISTS (SELECT 1 FROM [dbo].[MessageTemplates] WHERE [Id] = @Id)
                           BEGIN
                               UPDATE [dbo].[MessageTemplates]
                               SET [SetAsDeleted] = 1
                               WHERE [Id] = @Id
                           END
                           """;
        connection.Execute(sql, new { Id = id });
    }

    public void SaveMessageThatWasSent(int messageTemplateId, string customerUniqueId, string message)
    {
        using var connection = new SqlConnection(_connectionStrings.SnowDatabase);
        const string sql = """
                           INSERT INTO [dbo].[MessagesSent]
                           ([MessageTemplateId]
                           ,[CustomerUniqueId]
                           ,[Message]
                           ,[DateTimeSent])
                           VALUES
                           (@MessageTemplateId
                           ,@CustomerUniqueId
                           ,@Message
                           ,GETDATE());
                           """;
        connection.Execute(sql, new
        {
            MessageTemplateId = messageTemplateId,
            CustomerUniqueId = customerUniqueId,
            Message = message
        });
    }
}

