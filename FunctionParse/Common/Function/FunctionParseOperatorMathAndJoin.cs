/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/03
 * Time: 20:49
 */
using System;
using System.Diagnostics;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseOperatorMathAndJoin.
	/// </summary>
    [Serializable]
    public class FunctionParseOperatorMathAndJoin<T> : FunctionParseOperator<T>
	{
		private const String PLUS = "+";
		private const String SUBTRACT = "-";
		private const String MULTIPLY = "*";
		private const String DIVIDE = "/";
        public FunctionParseOperatorMathAndJoin(FunctionParseDataInfo<T> dataInfo, String operatorName) : base(dataInfo, operatorName) {
            switch (operatorName)
            {
                case MULTIPLY:
                case DIVIDE:
                    operatorPriority = PRIORITY_MULTIPLY_DIVIDE;
                    break;
                default:
                    operatorPriority = PRIORITY_PLUS_SUBTRACT;
                    break;
            }
        }

        protected internal sealed override TypeEnum GetResultType()
        {
            if (!PLUS.Equals(operatorName)) return TypeEnum.Decimal;
            else
            {
                TypeEnum argType0 = argList[0].GetResultType();
                if (argType0 == TypeEnum.String) return TypeEnum.String;
                else return TypeEnum.Decimal;
            }
        }

        protected internal sealed override Boolean CheckArgmentError()
        {
            if (base.CheckArgmentError()) return true;
            TypeEnum argType0 = argList[0].GetResultType();
            TypeEnum argType1 = argList[1].GetResultType();
            if (PLUS.Equals(operatorName))
            {
                if (argType0 == TypeEnum.Decimal && argType1 == TypeEnum.Decimal) return false;
                if (argType0 == TypeEnum.String && argType1 == TypeEnum.String) return false;
            }
            else
            {
                if (argType0 == TypeEnum.Decimal && argType1 == TypeEnum.Decimal) return false;
            }
            return SetError("argType0 != argType1");
        }
		
		protected internal sealed override void SetResultFunction()
		{
			base.SetArgmentResultFunction();			
			object arg0 = argList[0].resultFunction;	
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
						case PLUS:					
							return delegate (T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = argFn1(t);
								if (arg1 == null) return null;
								String argStr0 = arg0 as String;
                                if (argStr0 != null) return argStr0 + arg1.ToString();
								else return (Decimal) arg0 + (Decimal) arg1;
							};
						case SUBTRACT:
                            return delegate(T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = argFn1(t);
								if (arg1 == null) return null;
								return (Decimal) arg0 - (Decimal) arg1;
							};
						case MULTIPLY:
                            return delegate(T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = argFn1(t);
								if (arg1 == null) return null;
								return (Decimal) arg0 * (Decimal) arg1;
							};
						case DIVIDE:
                            return delegate(T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = argFn1(t);
								if (arg1 == null ) return null;
                                Decimal d1 = (Decimal)arg1;
								if (d1 == 0m) return null;
								return (Decimal) arg0 / d1;
							};
						default:
							return null;
					}
					#endregion two function
				} 
				else
				{
					#region argFn0 is function, param1 is not
					switch (operatorName)
					{
						case PLUS:		
							if (param1 is String)
							{
								return delegate (T t)
								{
									Object arg0 = argFn0(t);
									if (arg0 == null) return null;
									return arg0.ToString() + param1.ToString();
								};
							} 
							else 
							{
								return delegate (T t)
								{
									Object arg0 = argFn0(t);
									if (arg0 == null) return null;
									Object arg1 = param1;
									return (Decimal) arg0 + (Decimal) arg1;
								};
							}
						case SUBTRACT:
							return delegate (T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = param1;
								return (Decimal) arg0 - (Decimal) arg1;
							};
						case MULTIPLY:
							return delegate (T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = param1;
								return (Decimal) arg0 * (Decimal) arg1;
							};
						case DIVIDE:
							return delegate (T t)
							{
								Object arg0 = argFn0(t);
								if (arg0 == null) return null;
								Object arg1 = param1;
                                Decimal d1 = (Decimal)arg1;
								if (d1 == 0m) return null;
								return (Decimal) arg0 / d1;
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
					switch (operatorName)
					{
						case PLUS:			
							if (param0 is String)
							{
								return delegate (T t)
								{
									Object arg0 = param0;
									Object arg1 = argFn1(t);
									if (arg1 == null) return null;
									return arg0.ToString() + arg1.ToString();
								};
							} 
							else
							{
								return delegate (T t)
								{
									Object arg0 = param0;
									Object arg1 = argFn1(t);
									if (arg1 == null) return null;								
									else return (Decimal) arg0 + (Decimal) arg1;
								};
							}							
						case SUBTRACT:
							return delegate (T t)
							{
								Object arg0 = param0;
								Object arg1 = argFn1(t);
								if (arg1 == null) return null;
								return (Decimal) arg0 - (Decimal) arg1;
							};
						case MULTIPLY:
							return delegate (T t)
							{
								Object arg0 = param0;
								Object arg1 = argFn1(t);
								if (arg1 == null) return null;
								return (Decimal) arg0 * (Decimal) arg1;
							};
						case DIVIDE:
							return delegate (T t)
							{
								Object arg0 = param0;
								Object arg1 = argFn1(t);
								if (arg1 == null ) return null;
                                Decimal d1 = (Decimal)arg1;
								if (d1 == 0m) return null;
								return (Decimal) arg0 / d1;
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
						case PLUS:			
							if (param0 is String) objResult = param0.ToString() + param1.ToString();
							else objResult = (Decimal) param0 + (Decimal) param1;
							break;							
						case SUBTRACT:
							objResult =  (Decimal) param0 - (Decimal) param1;
							break;							
						case MULTIPLY:
							objResult =  (Decimal) param0 * (Decimal) param1;
							break;		
						case DIVIDE:
							objResult =  (Decimal) param0 / (Decimal) param1;
							break;		
						default:
							objResult = null;
							break;
					}
					return delegate (T t) {return objResult;};
					#endregion param0 is not function, param1 too
				}
			}
		}	
	}
}
