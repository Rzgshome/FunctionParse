/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 22:17
 */
using System;
using Com.Rzgshome.Common.Function.Method;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseMethod.
	/// </summary>
    [Serializable]
    public class FunctionParseMethod<T> : FunctionParseGroup<T>
	{
        private readonly String name; 
        private readonly MethodInfo<T> methodInfo;
        public FunctionParseMethod(FunctionParseMain<T> functionParse, String name)
            : base(functionParse)
		{
            this.name = name;
            this.methodInfo = GetMethodInfo(name);
		}

        private MethodInfo<T> GetMethodInfo(String name)
        {
            switch (name)
            {
                case MethodIf<T>.IF:
                    return MethodIf<T>.GetInstance();
                case MethodAbs<T>.ABS:
                    return MethodAbs<T>.GetInstance();
                case MethodMax<T>.MAX:
                    return MethodMax<T>.GetInstance();
                case MethodMin<T>.MIN:
                    return MethodMin<T>.GetInstance();
                case MethodIsNull<T>.IS_NULL:
                    return MethodIsNull<T>.GetInstance();
                case MethodIsNotNull<T>.IS_NOT_NULL:
                    return MethodIsNotNull<T>.GetInstance();
                default:
                    return functionParse.DataInfo.GetMethodInfo(name);
            }
        }

        public static Boolean IsMethod(FunctionParseMain<T> functionParse, String name)
        {
            switch (name)
            {
                case MethodIf<T>.IF:
                case MethodAbs<T>.ABS:
                case MethodMax<T>.MAX:
                case MethodMin<T>.MIN:
                case MethodIsNull<T>.IS_NULL:
                case MethodIsNotNull<T>.IS_NOT_NULL:
                    return true;
                default:
                    return functionParse.DataInfo.IsMethod(name);
            }
        }

        protected internal override TypeEnum GetResultType()
        {
            return methodInfo.GetReturnType(argList);
        }

        protected internal override Boolean CheckArgmentError()
        {
            if (base.CheckArgmentListError()) return true;
            List<TypeEnum> lstParamType = methodInfo.ParamTypeList;
            int paramIndex = 0;
            if (lstParamType != null)
            {
                if (argList.Count < lstParamType.Count) return SetError("argList.Count < lstParamType.Count");
                for (; paramIndex < lstParamType.Count; paramIndex++)
                {
                    if (lstParamType[paramIndex] != TypeEnum.Compute)
                    {
                        switch (lstParamType[paramIndex]) {
                            case TypeEnum.Boolean:
                            case TypeEnum.Enum:
                            case TypeEnum.String:
                                if (lstParamType[paramIndex] != argList[paramIndex].GetResultType()) return SetError("lstParamType[paramIndex] != TypeEnum.Compute && lstParamType[paramIndex] != argList[paramIndex].GetResultType()");
                                break;
                            case TypeEnum.Int:
                            case TypeEnum.Decimal:
                                if (argList[paramIndex].GetResultType() != TypeEnum.Decimal && argList[paramIndex].GetResultType() != TypeEnum.Int) {
                                    return SetError("lstParamType[paramIndex] != TypeEnum.Compute && lstParamType[paramIndex] != argList[paramIndex].GetResultType()");
                                }
                                break;
                            default:
                                return SetError("lstParamType[paramIndex] != TypeEnum.Compute && lstParamType[paramIndex] != argList[paramIndex].GetResultType()");
                                
                        }                        
                    }
                }
            }
            List<TypeEnum> lstOptionType = methodInfo.OptionParamTypeList;
            if (lstOptionType == null)
            {
                if (paramIndex < argList.Count) return SetError("paramIndex < argList.Count");
            }
            else
            {
                if (argList.Count - paramIndex > lstOptionType.Count) return SetError("argList.Count - paramIndex > lstOptionType.Count");
                for (int i = 0, end = Math.Min(lstOptionType.Count, argList.Count - paramIndex); i < end; i++)
                {
                    if (lstOptionType[i] != TypeEnum.Compute && lstOptionType[i] != argList[paramIndex + i].GetResultType()) return SetError("lstParamType[paramIndex] != TypeEnum.Compute && lstOptionType[i] != argList[paramIndex + i].GetResultType()");
                }
            }
            return methodInfo.CheckArgmentError(argList) && SetError("methodInfo.CheckArgmentError(argList)");
        }

        protected internal override void SetResultFunction()
        {
            base.SetArgmentResultFunction();
            resultFunction = methodInfo.GetResultFunction(argList);
        }

        public override sealed string ToString()
        {
            return name + "(" + base.ToString() + ")";
        }
	}
}
