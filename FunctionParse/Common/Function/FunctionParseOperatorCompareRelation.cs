/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/04
 * Time: 22:26
 * 
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseOperatorCompareRelation.
	/// </summary>
    [Serializable]
    public class FunctionParseOperatorCompareRelation<T> : FunctionParseOperator<T>
	{		
		public const String EQUALS = "=";
		public const String NOT_EQUALS = "!=";
		public const String NOT_EQUALS2 = "<>";
		public const String LARGE = ">";	
		public const String LARGE_EQUALS  = ">=";	
		public const String SMALL = "<";	
		public const String SMALL_EQUALS = "<=";
        public FunctionParseOperatorCompareRelation(FunctionParseDataInfo<T> dataInfo, String operatorName) : base(dataInfo, operatorName) 
        {
            operatorPriority = PRIORITY_COMPARE;
        }
		
		protected internal sealed override TypeEnum GetResultType() 
		{
			return TypeEnum.Boolean;
		}
		
		protected internal sealed override Boolean CheckArgmentError() 
		{
			if (base.CheckArgmentError()) return true;
			TypeEnum argType0 = argList[0].GetResultType();
            TypeEnum argType1 = argList[1].GetResultType();
            return argType0 != argType1 && SetError("argType0 != argType1");
		}

        protected internal sealed override void SetResultFunction()
        {
            base.SetArgmentResultFunction();
            object arg0 = argList[0].GetResultFunctionOrData();
            object arg1 = argList[1].GetResultFunctionOrData();
            resultFunction = GetResultFunctionWithParam(arg0, arg1);
        }

        private FunctionParseDelegate<T>.GetResult GetResultFunctionWithParam(object param0, object param1)
        {
            FunctionParseDelegate<T>.GetResult argFn0 = param0 as FunctionParseDelegate<T>.GetResult;
            FunctionParseDelegate<T>.GetResult argFn1 = param1 as FunctionParseDelegate<T>.GetResult;
            if (argFn0 != null)
            {
                if (argFn1 != null)
                {
                    #region two function
                    switch (operatorName)
                    {
                        case EQUALS:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return arg0.Equals(arg1);
                            };
                        case NOT_EQUALS:
                        case NOT_EQUALS2:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return !arg0.Equals(arg1);
                            };
                        case LARGE:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return (decimal) arg0 > (decimal)arg1;
                            };
                        case LARGE_EQUALS:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return (decimal)arg0 >= (decimal)arg1;
                            };
                        case SMALL:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return (decimal)arg0 < (decimal)arg1;
                            };
                        case SMALL_EQUALS:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return (decimal)arg0 <= (decimal)arg1;
                            };
                        default:
                            return null;
                    }
                    #endregion two function
                }
                else
                {
                    #region argFn0 is function, param1 is not
                    decimal? d1 = ParseDecimalOrNull(param1.ToString());                    
                    switch (operatorName)
                    {
                        case EQUALS:                           
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                return arg0.Equals(param1);
                            };
                        case NOT_EQUALS:
                        case NOT_EQUALS2:                           
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                return !arg0.Equals(param1);
                            };
                        case LARGE:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                return (decimal)arg0 > d1.Value;
                            };
                        case LARGE_EQUALS:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                return (decimal)arg0 >= d1.Value;
                            };
                        case SMALL:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                return (decimal)arg0 < d1.Value;
                            };
                        case SMALL_EQUALS:
                            return delegate(T t)
                            {
                                Object arg0 = argFn0(t);
                                if (arg0 == null) return null;
                                return (decimal)arg0 <= d1.Value;
                            };
                        default:
                            return null;
                    }
                    #endregion argFn0 is function, param1 is not
                }
            }
            else
            {
                if (argFn1 != null)
                {
                    #region argFn1 is function, param0 is not
                    decimal? d0 = ParseDecimalOrNull(param0.ToString());  
                    switch (operatorName)
                    {
                        case EQUALS:                          
                            return delegate(T t)
                            {
                                Object arg0 = param0;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return arg0.Equals(arg1);
                            };
                        case NOT_EQUALS:
                        case NOT_EQUALS2:                           
                            return delegate(T t)
                            {
                                Object arg0 = param0;
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return !arg0.Equals(arg1);
                            };
                        case LARGE:
                            return delegate(T t)
                            {
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return d0.Value > (decimal)arg1;
                            };
                        case LARGE_EQUALS:
                            return delegate(T t)
                            {
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return d0.Value >= (decimal)arg1;
                            };
                        case SMALL:
                            return delegate(T t)
                            {
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return d0.Value < (decimal)arg1;
                            };
                        case SMALL_EQUALS:
                            return delegate(T t)
                            {
                                Object arg1 = argFn1(t);
                                if (arg1 == null) return null;
                                return d0.Value <= (decimal)arg1;
                            };
                        default:
                            return null;
                    }
                    #endregion argFn1 is function, param0 is not
                }
                else
                {
                    #region param0 is not function, param1 too
                    object objResult;
                    switch (operatorName)
                    {
                        case EQUALS:
                            objResult = (param0.Equals(param1));
                            break;
                        case NOT_EQUALS:
                        case NOT_EQUALS2:
                            objResult = !(param0.Equals(param1));
                            break;
                        case LARGE:
                            objResult = ((decimal)param0 > (decimal)param1);
                            break;
                        case LARGE_EQUALS:
                            objResult = ((decimal)param0 >= (decimal)param1);
                            break;
                        case SMALL:
                            objResult = ((decimal)param0 < (decimal)param1);
                            break;
                        case SMALL_EQUALS:
                            objResult = ((decimal)param0 <= (decimal)param1);
                            break;
                        default:
                            objResult = null;
                            break;
                    }
                    return delegate(T t) { return objResult; };
                    #endregion param0 is not function, param1 too
                }
            }
        }	
	}
}
