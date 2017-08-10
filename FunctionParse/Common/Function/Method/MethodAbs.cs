using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function.Method
{
    [Serializable]
    class MethodAbs<T>:MethodInfo<T>
    {
        public const String ABS = "abs";
        private static MethodAbs<T> instance = null;
        public static MethodAbs<T> GetInstance()
        {
            if (instance == null) instance = new MethodAbs<T>();
            return instance;
        }

        private MethodAbs()
            : base(ABS, TypeEnum.Decimal, new List<TypeEnum>(), new List<TypeEnum>(), ResultFunction, false)
        {
            paramTypeList.Add(TypeEnum.Decimal);
        }

        private static FunctionParseDelegate<T>.GetResult ResultFunction(List<FunctionParseResultBase<T>> argList) 
        {
            return delegate(T t)
            {
                Object arg0 = argList[0].resultFunction(t);
                if (arg0 == null) return null;
                return Math.Abs((Decimal)arg0);
            };
        }
    }
}
