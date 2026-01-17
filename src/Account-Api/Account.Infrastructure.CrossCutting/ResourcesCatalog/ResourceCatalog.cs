using Account.Infrastructure.CrossCutting.ResourcesCatalog.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Account.Infrastructure.CrossCutting.ResourcesCatalog;

public class ResourceCatalog : IResourceCatalog
{
    public IEnumerable<Notification> Get(string key)
    {
        var notification = new List<Notification>();

        JObject json = JObject.Parse(File.ReadAllText("resources.json"));

        if (json == null || !json.HasValues)
        {
            return notification;
        }
        JArray messages = (JArray)json["resources"];

        if (messages == null || !messages.HasValues)
        {
            return notification;
        }

        foreach (var message in messages)
        {
            if (message["Key"].ToString() == key)
            {
                JArray values = (JArray)message["values"];

                if (values == null || !values.HasValues)
                {
                    continue;
                }

                foreach (var valueElement in values)
                {
                    var value = valueElement["value"];

                    if (value == null || value != null && (value["code"] == null || value["message"] == null))
                    {
                        continue;
                    }

                    notification.Add(new Notification()
                    {
                        Code = value["code"].ToString(),
                        Message = value["message"].ToString()
                    });
                }
            }
        }

        return notification;
    }

    public IEnumerable<Notification> UnexpectedError()
        => Get("UNEXPECTED_ERROR");
}