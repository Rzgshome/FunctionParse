using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Rzgshome.Common.Function
{
    public class FunctionTest
    {
        public static String GetResult()
        {
            StringBuilder sld = new StringBuilder();
            addResult(sld, "IF(123 + 2345 > 3000, 3000, 123 + 2345 )", "2468");
            addResult(sld, "IF(123 + 2345 < 3000, 'aaa', 'bbb' )", "aaa");
            addResult(sld, "IF(123 + 2345 <> 3000, if(1 >= 5 - 4, 1000, 2000), 3000)", "1000");
            addResult(sld, "IF(123 + 2345 != 3000, 3.890, 2.90)", "3.890");
            addResult(sld, "IF(123 + 2345 > 3000 OR 456 >= 456, 20 + 908 * 10, 123 + 2345 )", "9100");
            addResult(sld, "IF(not 123 + 2345 <= 2468, 2345 + 123, 50 + 1230 / 2460 + 1000 )", "1050.5");
            addResult(sld, "IF(not 123 + 2345 <= 2468, 2345 + 123, - 50 + 1230 / 2460 + 1000 )", "950.5");
            addResult(sld, "IF(abs(289 - 300) >= 11, 2345 - abs(123- 2345), - 50 + 1230 / 2460 + 1000 )", "123");
            addResult(sld, "IF(min(289 - 300, 1) <= -10, min(289 - 300, -20), - 50 + 1230 / 2460 + 1000 )", "-20");
            addResult(sld, "IF(max(289 - 300, 1) < 1, max(289 - 300, -20), max(- 50 + 1230 / 2460 + 1000, 2001) )", "2001");
            return sld.ToString();
        }

        private static void addResult(StringBuilder sld, String strFunction, String expect)
        {
            FunctionParseMain<Object> main = new FunctionParseMain<Object>(new FunctionParseDataInfo<Object>());
            if (main.Compile(strFunction))
            {
                Object result = main.RunFunction(null);
                if (result != null && result.ToString().Equals(expect))
                {
                    sld.AppendLine("Assert Ok.expect:" + expect + ", result:" + result);
                }
                else
                {
                    sld.AppendLine("Assert Error.expect:" + expect + ", result:" + result);
                }
            }
            else
            {
                sld.AppendLine("Compile Error:" + strFunction);
            }
            
        }
    }
}
