using System.Collections.Generic;
using Account.Infrastructure.CrossCutting.ResourcesCatalog.Models;

namespace Account.Infrastructure.CrossCutting.ResourcesCatalog;

public interface IResourceCatalog
{
    IEnumerable<Notification> Get(string key);
    IEnumerable<Notification> UnexpectedError();
}