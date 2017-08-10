/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/03
 * Time: 20:28
 */
using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseOperator.
	/// </summary>
    [Serializable]
    public class FunctionParseOperator<T> : FunctionParseBase<T>
    {
        public const int PRIORITY_MAX = 7;
        protected const int PRIORITY_MINUS = 7;
        protected const int PRIORITY_MULTIPLY_DIVIDE = 6;
        protected const int PRIORITY_PLUS_SUBTRACT = 5;
        protected const int PRIORITY_COMPARE = 4;
        protected const int PRIORITY_AND_OR = 3;
        protected const int PRIORITY_NOT = 2;
        protected int operatorPriority = 0;
        protected Boolean hasLeft = true;
        protected Boolean hasRight = true;
        public int OperatorPriority { get { return operatorPriority; } }
        public Boolean HasLeft { get { return hasLeft; } }
		protected String operatorName;
        public FunctionParseOperator(FunctionParseDataInfo<T> dataInfo, String operatorName)
            : base(dataInfo)
		{
			this.operatorName = operatorName;
		}

		protected internal override Boolean CheckArgmentError() 
		{
            if (base.CheckArgmentError()) return true;
            int paramCount = hasLeft && hasRight ? 2 : 1;
            return argList.Count != paramCount && SetError("argList.Count != paramCount");
		}

        public Boolean isArgumentEmpty()
        {
            return argList.Count == 0;
        }
        public Boolean ResetOperator(List<FunctionParseResultBase<T>> argChildren, int index)
        {
            int leftIndex = index - 1;
            int rightIndex = index + 1;
            if (hasLeft)
            {
                if (index == 0) return false;
                FunctionParseResultBase<T> leftParam = argChildren[leftIndex];
                argList.Add(leftParam);                
            }
            if (hasRight)
            {
                if (rightIndex >= argChildren.Count) return false;
                FunctionParseResultBase<T> rightParam = argChildren[rightIndex];
                argList.Add(rightParam);
                // 右側が先にする
                argChildren.RemoveAt(rightIndex);
            }
            if (hasLeft) argChildren.RemoveAt(leftIndex);
            return true;
        }

        public override sealed string ToString()
        {
            return operatorName + "[" + base.ToString() + "]";
        }
	}
}
