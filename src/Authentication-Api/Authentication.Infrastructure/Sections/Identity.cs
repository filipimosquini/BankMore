namespace Authentication.Infrastructure.Sections;

public class Identity
{
    public string Secret { get; set; }
    public int ExpiratesIn { get; set; }
    public string Issuer { get; set; }
    public string ValidOn { get; set; }
    public string RsaKeyId { get; set; }
    public string RsaPrivateKeyPem { get; set; }
}