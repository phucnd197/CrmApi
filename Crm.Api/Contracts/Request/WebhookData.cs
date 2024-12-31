namespace Crm_Api.Contracts.Request;

public record WebhookData<T>(string Event, T Body);
