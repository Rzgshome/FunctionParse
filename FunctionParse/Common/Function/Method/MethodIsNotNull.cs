using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function.Method
{
    [Serializable]
    class MethodIsNotNull<T>:MethodInfo<T>
    {
        public const String IS_NOT_NULL = "isnotnull";
        private static MethodIsNotNull<T> instance = null;
        public static MethodIsNotNull<T> GetInstance()
        {
            if (instance == null) instance = new MethodIsNotNull<T>();
            return instance;
        }

        private MethodIsNotNull()
            : base(IS_NOT_NULL, TypeEnum.Boolean, new List<TypeEnum>(), new List<TypeEnum>(), ResultFunction, false)
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
