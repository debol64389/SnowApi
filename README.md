## Local Development setup
### Infrastructure
The development environment is set up to use containerized infrastructure (`snowapi-infrastructure`)
running on the local developer machine using Docker Desktop.

Infrastructure container can be managed in Visual Studio by right clicking on the `development-resources\SnowApi.Infrastructure.development` and choosing
one of the options: `Compose Up|Down|Restart`. Alternatively, the container can be started from the command line using the 
`docker compose --file development-resources/compose.infrastructure.development.yml up --build --remove-orphans` command.

### Application usage
//Customers/customers/add_new - can be used to add a new customer
//Customers/customers/get_details - can be used to pull details by providing customer’s unique Id
//Customers/customers/update_email - can be used to update email by providing customer’s unique Id
//Customers/customers/delete - can be used to set customer as deleted by providing unique Id

//MessageTemplates /message_templates/add_new - can be used to add a new communication template. A body of communication template may contain a placeholders such {CustomerName}, {UniqueId} and {EmailAddress}. The Api should replace these placeholders with actual customer’s data when sending the messages.
//MessageTemplates /message_template/get_details - can be used to pull template details by providing template Id
//MessageTemplates /message_template/update_subject - can be used to update template subject by providing template Id
//MessageTemplates /message_template/update_body - can be used to update template body by providing template Id. A body of communication template may contain a placeholders such {CustomerName}, {UniqueId} and {EmailAddress}. The Api should replace these placeholders with actual customer’s data when sending the messages.
//MessageTemplates /message_template/delete - can be used to set template as deleted by providing template Id

//Communication /communication/send_template_message - can be used to send a template message to customer by providing customer’s unique Id and a templates Id. Placeholder supposed to be replaced with actual customer data. Sent message is supposed to be logged to debugging console and saved to [MessageSent] table.


