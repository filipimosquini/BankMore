using Newtonsoft.Json;
using System.Collections.Generic;

namespace Transfer.Infrastructure.CrossCutting.ResourcesCatalog.Models;

public class Notification
{
    public string Code { get; set; }
    public string Message { get; set; }
}

public sealed class NotificationEnvelope
{
    [JsonProperty("notifications")]
    public List<Notification>? Notifications { get; set; }
}