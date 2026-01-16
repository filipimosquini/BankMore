using System.Collections.Generic;

namespace Authentication.Application.Users.Commands.Dto;

public class AuthenticationDto
{
    /// <summary>
    /// The JWT token.
    /// </summary>
    /// <example></example>
    public string AccessToken { get; set; }

    /// <summary>
    /// Time of expiration token.
    /// </summary>
    /// <example></example>
    public double ExpiresIn { get; set; }

    /// <summary>
    /// user Information form token.
    /// </summary>
    /// <example></example>
    public UserToken UserToken { get; set; }
}

public class UserToken
{
    /// <summary>
    /// The user id.
    /// </summary>
    /// <example></example>
    public string Id { get; set; }

    /// <summary>
    /// The document.
    /// </summary>
    /// <example></example>
    public string Cpf { get; set; }

    /// <summary>
    /// The user claims.
    /// </summary>
    /// <example></example>
    public IEnumerable<UserClaim> Claims { get; set; }
}

public class UserClaim
{
    /// <summary>
    /// The value of claim.
    /// </summary>
    /// <example></example>
    public string Value { get; set; }

    /// <summary>
    /// The type of claim.
    /// </summary>
    /// <example></example>
    public string Type { get; set; }
}