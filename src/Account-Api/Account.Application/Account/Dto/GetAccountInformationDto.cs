using System;

namespace Account.Application.Account.Dto;

public record GetAccountInformationDto(Guid AccountId, int AccountNumber);