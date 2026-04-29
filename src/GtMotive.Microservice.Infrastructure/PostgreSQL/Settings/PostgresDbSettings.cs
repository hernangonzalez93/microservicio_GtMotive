using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Infrastructure.PostgreSQL.Settings;

public class PostgresDbSettings
{
    /// <summary>
    /// Gets or sets the connection string used to establish a connection to the PostgreSQL database.
    /// </summary>
    public required string ConnectionString { get; set; }
}
