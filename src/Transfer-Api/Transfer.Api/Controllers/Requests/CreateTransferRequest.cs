namespace Transfer.Api.Controllers.Requests;

public record CreateTransferRequest(int DestinationAccountNumber, decimal Amount);