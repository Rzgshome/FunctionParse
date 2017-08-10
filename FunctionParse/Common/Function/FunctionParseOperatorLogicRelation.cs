/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/03
 * Time: 20:43
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseOperatorLogicRelation.
    /// NULLの処理
    /// AND： NULL⇒ FALSE
    /// OR：NULL ⇒ FALSE
    /// NOT：NULL ⇒ NULL
	/// </summary>
    [Serializable]
    public class FunctionParseOperatorLogicRelation<T> : FunctionParseOperator<T>
	{
		private const String AND = "and";
        private const String OR = "or";
        private const String NOT = "not";
        public FunctionParseOperatorLogicRelation(FunctionParseDataInfo<T> dataInfo, String operatorName)
            : base(dataInfo, operatorName)
        {
            switch (operatorName)
            {
                case AND:
                case OR:
                    operatorPriority = PRIORITY_AND_OR;
                    break;
                default:
                    hasLeft = false;
                    operatorPriority = PRIORITY_NOT;
                    break;
            }
		}
		
		protected internal sealed override TypeEnum GetResultType() 
		{
			return TypeEnum.Boolean;
		}
		
		protected internal sealed override Boolean CheckArgmentError() 
		{
			if (base.CheckArgmentError()) return true;
			TypeEnum argType0 = argList[0].GetResultType();
            if (argList.Count == 2) 
            {
                TypeEnum argType1 = argList[1].GetResultType();
                if (argType0 == TypeEnum.Boolean && argType1 == TypeEnum.Boolean) return false;
            }
            else if (argType0 == TypeEnum.Boolean) return false;			
            return SetError("argType0 != TypeEnum.Boolean || argType1 != TypeEnum.Boolean");
		}

        protected internal sealed override void SetResultFunction()
        {
            base.SetArgmentResultFunction();
            resultFunction = GetResultFunction();
        }

        private FunctionParseDelegate<T>.GetResult GetResultFunction()
        {
            FunctionParseDelegate<T>.GetResult argFn0 = argList[0].resultFunction;
            switch (operatorName)
            {
                case AND:
                    return delegate(T t)
                    {
                        Object arg0 = argFn0(t);
                        if (arg0 != null && (Boolean)arg0)
                        {
                            Object arg1 = argList[1].resultFunction(t);
                            return arg1 != null && (Boolean)arg1;
                        }
                        return false;
                    };
                case OR:
                    return delegate(T t)
                    {
                        Object arg0 = argFn0(t);
                        if (arg0 != null && (Boolean) arg0) return true;
                        Object arg1 = argList[1].resultFunction(t);
                        return arg1 != null && (Boolean) arg1;
                    };
                case NOT:
                    return delegate(T t)
                    {
                        Object arg0 = argFn0(t);
                        if (arg0 == null) return null;
                        return !((Boolean) arg0);
                    };
                default:
                    return null;
            }
        }

        public static Boolean IsRelation(String strLowerValue)
        {
            switch (strLowerValue)
            {
                case FunctionParseOperatorLogicRelation<T>.AND:
                case FunctionParseOperatorLogicRelation<T>.OR:
                case FunctionParseOperatorLogicRelation<T>.NOT:
                    return true;
                default:
                    return false;
            }
        }
	}
}
