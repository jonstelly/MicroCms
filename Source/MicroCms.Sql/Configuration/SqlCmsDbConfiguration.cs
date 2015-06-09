using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Sql.Configuration
{
	/// <summary>
	/// This class allows us to configure the database connection and other attributes via code, rather than
	/// through the config file (which was the standard approach prior to EF6). This allows us to to keep the
	/// config file(s) clean and free of any Entity Framework cruft for applications that aren't using a SQL
	/// database. For application that do intend to use SQL server, any options that are present in the
	/// config file will override these code-based options. In other words, using the config file to set options,
	/// including the connection string, will still work. See https://msdn.microsoft.com/en-us/data/jj680699
	/// for more details.
	/// </summary>
	public class SqlCmsDbConfiguration : DbConfiguration
	{
		public SqlCmsDbConfiguration()
		{
			SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
		}
	} 
}
