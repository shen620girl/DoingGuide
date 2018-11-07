using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UnitTestGuide.Common
{
    /// <summary>
    /// 输出帮助类
    /// </summary>
public   class CwHelper
    {
        public void Print(object c)
        {
           var jsonstr= JsonConvert.SerializeObject(c);
            Console.WriteLine((jsonstr));
        }
    }
}
