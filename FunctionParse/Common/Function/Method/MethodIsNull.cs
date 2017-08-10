using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function.Method
{
    [Serializable]
    class MethodIsNull<T>:MethodInfo<T>
    {
        public const String IS_NULL = "isnull";
        private static MethodIsNull<T> instance = null;
        public static MethodIsNull<T> GetInstance()
        {
            if (instance == null) instance = new MethodIsNull<T>();
            return instance;
        }

        private MethodIsNull()
            : base(IS_NULL, TypeEnum.Boolean, new List<TypeEnum>(), new List<TypeEnum>(), ResultFunction, false)
        {
            paramTypeList.Add(TypeEnum.Compute);
        }

        private static FunctionParseDelegate<T>.GetResult ResultFunction(List<FunctionParseResultBase<T>> argList) 
        {
            return delegate(T t)
            {
                Object arg0 = argList[0].resultFunction(t);
                return (arg0 == null);
            };
        }
    }
}
