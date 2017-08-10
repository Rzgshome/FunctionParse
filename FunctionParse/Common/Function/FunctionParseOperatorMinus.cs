/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/03
 * Time: 20:50
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseOperatorMinus.
	/// </summary>
    [Serializable]
    public class FunctionParseOperatorMinus<T> : FunctionParseOperator<T>
	{
        public FunctionParseOperatorMinus(FunctionParseDataInfo<T> dataInfo, String operatorName)
            : base(dataInfo, operatorName)
        {
            hasLeft = false;
            operatorPriority = PRIORITY_MINUS;
		}
		
		protected internal sealed override TypeEnum GetResultType() 
		{
            return TypeEnum.Decimal;
		}
		
		protected internal sealed override Boolean CheckArgmentError() 
		{
            if (base.CheckArgmentError()) return true;
            if (argList[0].GetResultType() != TypeEnum.Decimal) return SetError("argList[0].GetResultType() != TypeEnum.Decimal");
			return false;
		}

        protected internal sealed override void SetResultFunction()
        {
            FunctionParseDelegate<T>.GetResult argFn0 = argList[0].GetResultFunctionOrData() as FunctionParseDelegate<T>.GetResult;
            if (argFn0 != null)
            {
                resultFunction = delegate(T t)
                {
                    Object arg0 = argFn0(t);
                    if (arg0 == null) return null;
                    else return -(Decimal)arg0;
                };
            }
            else
            {
                resultFunction = delegate(T t)
                {
                    return -(Decimal) argList[0].GetResultFunctionOrData();
                };
            }
        }


        protected internal sealed override Object GetResultFunctionOrData() {
            FunctionParseDelegate<T>.GetResult argFn0 = argList[0].GetResultFunctionOrData() as FunctionParseDelegate<T>.GetResult;
            if (argFn0 != null) return resultFunction;
            else return -(Decimal)argList[0].GetResultFunctionOrData();
        }
	}
}
