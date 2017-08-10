using System;
using System.Collections.Generic;
using System.Text;
using Com.Rzgshome.Common.Function;

namespace FunctionParseTest.Test
{
    class FunctionParseTestMain : FunctionParseMain<FunctionTestData>
    {
        public FunctionParseTestMain(): this(FunctionTestData.GetInstance())
        { 
        }

        public FunctionParseTestMain(FunctionTestData data)
            : base(new FunctionParseTrendDataInfo(data))
        {
        }  
    }

    class FunctionParseTrendDataInfo : FunctionParseDataInfo<FunctionTestData>
    {
        public FunctionParseTrendDataInfo(FunctionTestData data)
        {
            data.AddAllField(this);
            data.AddAllMethod(this);
        }
    }
}
