using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function.Method
{
    [Serializable]
    class MethodMax<T>:MethodInfo<T>
    {
        public const String MAX = "max";
        private static MethodMax<T> instance = null;
        public static MethodMax<T> GetInstance()
        {
            if (instance == null) instance = new MethodMax<T>();
            return instance;
        }

        private MethodMax()
            : base(MAX, TypeEnum.Decimal, new List<TypeEnum>(), new List<TypeEnum>(), ResultFunction, false)
        {
            paramTypeList.Add(TypeEnum.Decimal);
            paramTypeList.Add(TypeEnum.Decimal);
        }

        private static FunctionParseDelegate<T>.GetResult ResultFunction(List<FunctionParseResultBase<T>> argList) 
        {
            return delegate(T t)
            {
                Object arg0 = argList[0].resultFunction(t);
                if (arg0 == null) return null;
                Object arg1 = argList[1].resultFunction(t);
                if (arg1 == null) return null;
                return Math.Max((Decimal)arg0, (Decimal)arg1);
            };
        }
    }
}
