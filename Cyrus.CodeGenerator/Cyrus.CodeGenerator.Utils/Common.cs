using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.Utils
{
    public class Common
    {
        /// <summary>
        /// 将枚举转为字典类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<DictionaryEntry> EnumToDictory(Type type)
        {
            try
            {
                if (!type.IsEnum)
                {
                    throw new Exception("当前的类型不是枚举类型，请使用 枚举 类型来进行操作！");
                }
                List<DictionaryEntry> result = new List<DictionaryEntry>();//使用通用的字典实体类型（键值的类型都是object类型）
                Array arrObj = Enum.GetValues(type);
                foreach (var item in arrObj)
                {
                    result.Add(new DictionaryEntry(Enum.GetName(type, item), item));
                }
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 枚举类型转化为DataTable
        /// </summary>
        public static DataTable EnumToDataTable(Type enumType, string key, string val)
        {
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);

            var table = new DataTable();
            table.Columns.Add(key, Type.GetType("System.String"));
            table.Columns.Add(val, Type.GetType("System.Int32"));
            table.Columns[key].Unique = true;
            for (int i = 0; i < values.Length; i++)
            {
                var dr = table.NewRow();
                dr[key] = names[i];
                dr[val] = (int)values.GetValue(i);
                table.Rows.Add(dr);
            }
            return table;
        }
    }
}
