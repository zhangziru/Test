using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HomeTest
{
	//单例模式
    public class SingleFactory<T> where T : new()
    {
        private static T _instance;
        public static T CreateInstance()
        {
            
            if (_instance == null)
            {
                _instance =  new T();
            }
            return _instance;
        }		
    }
}
