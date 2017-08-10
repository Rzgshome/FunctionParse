/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 21:44
 */
using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseGroup.
	/// </summary>
    [Serializable]
    public class FunctionParseGroup<T> : FunctionParseBase<T>
	{
        protected readonly List<List<FunctionParseResultBase<T>>> argGroupList = new List<List<FunctionParseResultBase<T>>>();
        protected readonly FunctionParseMain<T> functionParse;
        private FunctionParseGroup<T> preGroup;
        private List<FunctionParseResultBase<T>> preArgList;
        public FunctionParseGroup(FunctionParseMain<T> functionParse) : base(functionParse.DataInfo) 
        {
            this.functionParse = functionParse;
        }
		
		public void StartGroup()
		{
			preGroup = functionParse.CurGroup;
			preArgList = functionParse.ArgList;
			functionParse.CurGroup = this;
            functionParse.ArgList = new List<FunctionParseResultBase<T>>();
			argGroupList.Add(functionParse.ArgList);
		}
		
		public void MoveToNextParameter()
		{
            functionParse.ArgList = new List<FunctionParseResultBase<T>>();
			argGroupList.Add(functionParse.ArgList);
		}
		
		public void EndGroup()
		{
			functionParse.ArgList = preArgList;
			functionParse.CurGroup = preGroup;
		}

        protected internal override void ResetChildren()
        {
            for (int i = 0, end = argGroupList.Count; i < end; i++)
            {
                foreach (FunctionParseResultBase<T> child in argGroupList[i]) 
                {
                    FunctionParseBase<T> childFunction = child as FunctionParseBase<T>;
                    if (childFunction != null) childFunction.ResetChildren();
                }
                FunctionParseResultBase<T> result = functionParse.ResetChildren(argGroupList[i]);
                if (result != null) argList.Add(result);
            }
        }

        protected internal override TypeEnum GetResultType()
        {
            return argList[0].GetResultType();
        }

        protected internal override Boolean CheckArgmentError()
        {
            if (base.CheckArgmentError()) return true;
            return (argList.Count != 1) && SetError("argList.Count != 1");
        }

        protected internal override void SetResultFunction()
        {
            base.SetArgmentResultFunction();
            Object child = argList[0].GetResultFunctionOrData();
            if (child is FunctionParseDelegate<T>.GetResult) resultFunction = child as FunctionParseDelegate<T>.GetResult;
            else resultFunction = delegate(T t)
            {
                return child;
            };
        }

        public override string ToString()
        {
            return "(" + base.ToString() + ")";
        }
	}
}
