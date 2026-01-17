namespace Authentication.Infrastructure.Sections;

public class Identity
{
    public int ExpiratesIn { get; set; }
    public string Issuer { get; set; }
    public string ValidOn { get; set; }
    public string RsaKeyId { get; set; }
    public string RsaPrivateKeyPem { get; set; }
}