using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function.Method
{
    [Serializable]
    class MethodIf<T>:MethodInfo<T>
    {
        public const String IF = "if";
        private static MethodIf<T> instance = null;
        public static MethodIf<T> GetInstance()
        {
            if (instance == null) instance = new MethodIf<T>();
            return instance;
        }

        private MethodIf()
            : base(IF, TypeEnum.Compute, new List<TypeEnum>(), new List<TypeEnum>(), ResultFunction, false)
        {
            paramTypeList.Add(TypeEnum.Boolean);
            paramTypeList.Add(TypeEnum.Compute);
            paramTypeList.Add(TypeEnum.Compute);
        }

        protected sealed override TypeEnum GetComputeReturnType(List<FunctionParseResultBase<T>> argList)
        {
            return argList.Count >= 3 ? argList[1].GetResultType() : TypeEnum.Invalid;
        }

        public sealed override Boolean CheckArgmentError(List<FunctionParseResultBase<T>> argList)
        {
            if (argList.Count >= 3 && argList[0].GetResultType() == TypeEnum.Boolean && argList[1].GetResultType() == argList[2].GetResultType()) return false;
            return true;
        }

        private static FunctionParseDelegate<T>.GetResult ResultFunction(List<FunctionParseResultBase<T>> argList) 
        {
            return delegate(T t)
            {
                Object arg0 = argList[0].resultFunction(t);
                if (arg0 == null) return null;
                if ((Boolean) arg0 == true) return argList[1].resultFunction(t);
                else return argList[2].resultFunction(t);
            };
        }
    }
}
