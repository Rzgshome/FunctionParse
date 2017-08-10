/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 20:19
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseData.
	/// </summary>
    [Serializable]
    public class FunctionParseData<T> : FunctionParseResultBase<T>
	{
		private Object value;
		public Object Value{get{return value;}}
		private ParseDataTypeEnum dataType;
        public FunctionParseData(FunctionParseDataInfo<T> dataInfo, String strValue, ParseDataTypeEnum parseDataType)
            : base(dataInfo)
		{
			dataType = parseDataType;
			switch (parseDataType)
			{
				case ParseDataTypeEnum.Field:
				case ParseDataTypeEnum.String:
					value = strValue;
					break;
                case ParseDataTypeEnum.Decimal:
                    value = Decimal.Parse(strValue);
					break;
				default:
					try
					{
                        value = Decimal.Parse(strValue);
                        dataType = ParseDataTypeEnum.Decimal;
					} 
					catch (Exception)
					{
						value = strValue;
					}
					break;
			}
		}
		
		public Boolean IsOther()
		{
			return dataType == ParseDataTypeEnum.Other;
		}
		
		protected internal sealed override TypeEnum GetResultType() 
		{
			switch (dataType)
			{
				case ParseDataTypeEnum.Field:
					return dataInfo.GetFieldResultType(value.ToString());
				case ParseDataTypeEnum.String:
					return TypeEnum.String;
                case ParseDataTypeEnum.Decimal:
                    return TypeEnum.Decimal;
				default:
					return TypeEnum.Invalid;
			}
		}
		
		protected internal sealed override void SetResultFunction()
		{
			switch(dataType)
			{
				case ParseDataTypeEnum.Field:
					resultFunction = dataInfo.GetFieldResultFunction(value.ToString());
					break;
				default:
					resultFunction = delegate (T t)
					{
						return value;
					};
					break;
			}
		}
		
		protected internal sealed override Object GetResultFunctionOrData() 
		{
			switch(dataType)
			{
				case ParseDataTypeEnum.Field:
                    return dataInfo.IsFixedField(value.ToString()) ? resultFunction(default(T)) : resultFunction;
				default:					
					return value;
			}
		}

        public override sealed string ToString()
        {
            return dataType.ToString() + "|" + value;
        }
	}
}
