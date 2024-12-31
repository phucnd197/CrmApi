This project is to show a possible solution of an Api to integrate a new CRM into the system.
- What this entire project includes:
  - Crm Api (main project of this solution)
  - Internal services that immitate the internal services: User Api (manages internal users), PIM Api (manages prices and products), Shared library (for common functionality like logging and hooking up wrapper service for the event firing)

- The chacteristics of the Crm Api project:
  - Project created using the vertical slice architecture coupled with MediatR for easier maintenance and develop new features
  - Use Request-Endpoint-Response pattern instead of the usual controller pattern using FastEndpoints
  - Use a EFcore for database interaction
  - Use SQLite as the database
  - Use MediatR for communicating between components for reduced coupling
  - Use Serilog for logging with configured sink to ElasticSearch (local elasticsearch running on docker)
  - Use Masstransit for communication with internal services (with local rabbitmq running on docker)
  - Use Distributed locking for a functionality using redis (with local redis running on docker)
  - Use Refit for connecting to other Api like CRM Api and PIM Api
  - Use FluentValidation for basic model validation
  - Use Middleware to log and return result for un-handled exceptions
  - Use XUnit + NSubstitute for unit testing
  
- List of features:
  - Add internal user through webhook that will called by the CRM
  - Check if a user is a Customer in the CRM system and had regsitered pricing agreement (i.e is officially a customer)
  - Get all product list and prices for a products
  - Register for a pricing agreement

- List of missing features because of time constraints:
  - Authentication and Authorization for accessing some endpoint
  - Create a client-side app
  - Maybe adding a mapping library in the future
