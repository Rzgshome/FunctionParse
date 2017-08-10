/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 21:24
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseResultBase.
    /// </summary>
    [Serializable]
    public class FunctionParseResultBase<T>
	{
        protected readonly FunctionParseDataInfo<T> dataInfo;
        public FunctionParseDataInfo<T> DataInfo { get { return dataInfo; } }
        protected internal FunctionParseDelegate<T>.GetResult resultFunction;
        public FunctionParseResultBase(FunctionParseDataInfo<T> dataInfo)
		{
			this.dataInfo = dataInfo;
		}
		protected internal virtual void SetResultFunction(){}

        protected internal virtual Object GetResultFunctionOrData() { return resultFunction; }

		protected internal virtual TypeEnum GetResultType(){return TypeEnum.Invalid;}		
		
		protected internal virtual Boolean CheckArgmentError() {return false;}

        public Object GetResultWithData(T t = default(T))
        {
            Object obj = GetResultFunctionOrData();
            if (obj is FunctionParseDelegate<T>.GetResult) return ((FunctionParseDelegate<T>.GetResult)obj)(t);
            else return obj;
        }
	}
}
