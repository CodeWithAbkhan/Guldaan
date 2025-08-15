using System.ComponentModel.DataAnnotations;

namespace Guldaan.Common.Db;

public interface IConcurrencyCheckEntity
{
    [ConcurrencyCheck]
    public Guid Version { get; set; }
}