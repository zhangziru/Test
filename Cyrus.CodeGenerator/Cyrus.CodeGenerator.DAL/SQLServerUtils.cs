using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.DAL
{
    public class SQLServerUtils
    {        
        public static string GetTypeByDBType(string dbtype)
        {
            switch (dbtype.ToLower())
            {
                case "int":
                    return "int";
                case "bigint":
                    return "long";                
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    return "string";
                case "bit":
                    return "bool";
                case "datetime":
                    return "DateTime";
                case "uniqueidentifier":
                    return "Guid";
                default:
                    return "object";
            }
        }
    }
}
