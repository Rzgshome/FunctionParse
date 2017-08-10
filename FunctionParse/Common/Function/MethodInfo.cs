/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 17:40
 */
using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of MethodInfo.
    /// </summary>
    [Serializable]
	public class MethodInfo<T>
	{
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly String name;
		public String Name{get{return name;}}
        protected readonly TypeEnum returnType;
        public TypeEnum GetReturnType(List<FunctionParseResultBase<T>> argList) { return returnType != TypeEnum.Compute ? returnType : GetComputeReturnType(argList); }
        protected readonly List<TypeEnum> paramTypeList;
		public List<TypeEnum> ParamTypeList{get{return paramTypeList;}}
        protected readonly List<TypeEnum> optionParamTypeList;
        public List<TypeEnum> OptionParamTypeList { get { return optionParamTypeList; } }
        private readonly FunctionParseDelegate<T>.GetMethodFunction methodFunction;
        public FunctionParseDelegate<T>.GetResult GetResultFunction(List<FunctionParseResultBase<T>> argList) { return methodFunction(argList); }
        public MethodInfo(String name, TypeEnum returnType, List<TypeEnum> paramTypeList,　List<TypeEnum> optionParamTypeList, FunctionParseDelegate<T>.GetMethodFunction methodFunction, Boolean addLog = true)
		{
			this.name = name;
			this.returnType = returnType;
			this.paramTypeList = paramTypeList;
            this.optionParamTypeList = optionParamTypeList;
            if (addLog && log.IsDebugEnabled)
            {
                this.methodFunction = delegate(List<FunctionParseResultBase<T>> argList)
                {
                    FunctionParseDelegate<T>.GetResult getResultFn = methodFunction(argList);
                    return delegate(T t)
                    {
                        object obj = getResultFn != null ? getResultFn(t) : null;
                        log.DebugFormat("{0}:{1}", name, obj);
                        return obj;
                    };
                };
            }
            else
            {
                this.methodFunction = methodFunction;
            }
		}

        protected virtual TypeEnum GetComputeReturnType(List<FunctionParseResultBase<T>> argList)
        {
            return TypeEnum.Invalid;
        }

        public virtual Boolean CheckArgmentError(List<FunctionParseResultBase<T>> argList)
        {
            return false;
        }
	}
}
