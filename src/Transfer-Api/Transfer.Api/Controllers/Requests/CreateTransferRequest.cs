namespace Transfer.Api.Controllers.Requests;

public record CreateTransferRequest(decimal DestinationAccountNumber, decimal Amount);