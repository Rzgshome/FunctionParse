/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 21:26
 */
using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseDelegate.
    /// </summary>
    [Serializable]
	public class FunctionParseDelegate<T>
	{
        public delegate GetResult GetMethodFunction(List<FunctionParseResultBase<T>> argList);
        public delegate Object GetResult(T t);
        //public delegate void GetVoidResult(T t);
        //public delegate int GetIntResult(T t);
        //public delegate decimal GetDecimalResult(T t);
        //public delegate String GetStringResult(T t);
        //public delegate Boolean GetBooleanResult(T t);
		private FunctionParseDelegate()
		{
		}
	}
}
