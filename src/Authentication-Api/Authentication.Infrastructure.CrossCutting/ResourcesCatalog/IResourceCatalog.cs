using System.Collections.Generic;
using Authentication.Infrastructure.CrossCutting.ResourcesCatalog.Models;

namespace Authentication.Infrastructure.CrossCutting.ResourcesCatalog;

public interface IResourceCatalog
{
    IEnumerable<Notification> Get(string key);
    IEnumerable<Notification> UnexpectedError();
}