using System.Collections.Generic;
using Transfer.Infrastructure.CrossCutting.ResourcesCatalog.Models;

namespace Transfer.Infrastructure.CrossCutting.ResourcesCatalog;

public interface IResourceCatalog
{
    IEnumerable<Notification> Get(string key);
    IEnumerable<Notification> UnexpectedError();
}