/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 17:36
 */
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParse.
	/// </summary>
    [Serializable]
    public class FunctionParseMain<T> : FunctionParseBase<T>
	{
        public FunctionParseMain(FunctionParseDataInfo<T> dataInfo) : base(dataInfo) { }

        private FunctionParseGroup<T> curGroup;
        public FunctionParseGroup<T> CurGroup{get{return curGroup;} set{curGroup = value;}}
        public List<FunctionParseResultBase<T>> ArgList { get { return argList; } set { argList = value; } }
		private String strFunction;
		private String strLowerFunction;
		public virtual Boolean Compile(String function)
		{
			strFunction = function;
			strLowerFunction = strFunction.ToLower();
			int startIndex = 0;
			char[] arrChar = strLowerFunction.ToCharArray();
			for (int i = 0; i < arrChar.Length; i++)
			{
				switch(arrChar[i])
				{
					case ' ':
						ParseFixedValue(startIndex, i, false);
						startIndex = i + 1;
						break;
					case '(':
                        ParseGroup(startIndex, i);
                        startIndex = i + 1;
						break;
					case ')':
						ParseFixedValue(startIndex, i, false);
                        if (curGroup == null) return ! SetError("no ( is pair to the ) at " + i);
                        curGroup.EndGroup();
						startIndex = i + 1;
						break;
					case ',':
						ParseFixedValue(startIndex, i, false);
                        if (curGroup == null) return ! SetError("no ( is pair to the , at " + i);
                        curGroup.MoveToNextParameter();
						startIndex = i + 1;
						break;
					case '"':
					case '\'':
						// there is no " in String 
                        if (startIndex != i) return ! SetError("no char can before " + arrChar[i] + "。");
						while (true) {
							int index = strLowerFunction.IndexOf(arrChar[i],  i + 1);
                            if (index < 0) return ! SetError("There is no " + arrChar[i] + " which pair to " + arrChar[i] + " at:" + i);
							i = index;
							if (arrChar[index - 1] != '\\') break;
						}
						ParseFixedValue(startIndex + 1, i, true);
						startIndex = i + 2;
						break;
					case '+':
						ParseFixedValue(startIndex, i, false);
                        argList.Add(new FunctionParseOperatorMathAndJoin<T>(dataInfo, "+"));
						startIndex = i + 1;
						break;
					case '-':
						ParseFixedValue(startIndex, i, false);
                        if (argList.Count == 0) argList.Add(new FunctionParseOperatorMinus<T>(dataInfo, "-"));
                        else
                        {
                            FunctionParseResultBase<T> preResult = argList[argList.Count - 1];
                            if (preResult is FunctionParseOperatorCompareRelation<T> ||preResult is FunctionParseOperatorLogicRelation<T>) argList.Add(new FunctionParseOperatorMinus<T>(dataInfo, "-"));
                            else argList.Add(new FunctionParseOperatorMathAndJoin<T>(dataInfo, "-"));
                        }
						startIndex = i + 1;		
						break;
					case '*':
						ParseFixedValue(startIndex, i, false);
                        argList.Add(new FunctionParseOperatorMathAndJoin<T>(dataInfo, "*"));
						startIndex = i + 1;
						break;
					case '/':
						ParseFixedValue(startIndex, i, false);
                        argList.Add(new FunctionParseOperatorMathAndJoin<T>(dataInfo, "/"));
						startIndex = i + 1;
						break;
                    case '=':
                        ParseFixedValue(startIndex, i, false);
                        argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, "="));
						startIndex = i + 1;
						break;
                    case '!':
                        ParseFixedValue(startIndex, i, false);
                        i++;
                        if (i < arrChar.Length && arrChar[i] == '=')
                        {
                            argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, "!="));
                            startIndex = i + 1;
                        }
                        else
                        {
                            return ! SetError("There is no = after !");
                        }
						break;
					case '>':
                        ParseFixedValue(startIndex, i, false);
                        i++;
                        if (i < arrChar.Length)
                        {
                            if (arrChar[i] == '=') argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, ">="));
                            else
                            {
                                i--;
                                argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, ">"));
                            }                            
                            startIndex = i + 1;
                        }
                        else
                        {
                            return ! SetError("There is no char after >");
                        }
						break;
					case '<':
						ParseFixedValue(startIndex, i, false);
                        i++;
                        if (i < arrChar.Length)
                        {
                            if (arrChar[i] == '=') argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, "<="));
                            else if (arrChar[i] == '>') argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, "<>"));
                            else
                            {
                                i--;
                                argList.Add(new FunctionParseOperatorCompareRelation<T>(dataInfo, "<"));
                            }                            
                            startIndex = i + 1;
                        }
                        else
                        {
                            return ! SetError("There is no char after <");
                        }
						break;					
					default:
						break;						
				}
            }
            if (startIndex < arrChar.Length) ParseFixedValue(startIndex, arrChar.Length, false);
            if (isNoError)
            {
                ResetChildren();
                if (isNoError)
                {
                    if (CheckArgmentError()) return ! SetError("CheckArgmentError:");
                    else
                    {
                        SetResultFunction();
                        return true;
                    }
                }
            }
			return false;
		}

        protected internal sealed override void ResetChildren()
        {
            base.ResetChildren();
            FunctionParseResultBase<T> afterRest = ResetChildren(argList);
            argList.Clear();
            argList.Add(afterRest);
        }

        protected internal FunctionParseResultBase<T> ResetChildren(List<FunctionParseResultBase<T>> argChildren)
        {
            if (argChildren == null || argChildren.Count == 0) return null;// SetError("ResetChildren none.");
            else if (argChildren.Count == 1) return argChildren[0];
            else 
            {
                // 計算順番
                for (int priority = FunctionParseOperator<T>.PRIORITY_MAX; priority > 0; priority--)
                {
                    for (int i = 0, end = argChildren.Count; i < end; i++)
                    {
                        FunctionParseOperator<T> argOperator = argChildren[i] as FunctionParseOperator<T>;
                        if (argOperator != null && argOperator.OperatorPriority == priority && argOperator.isArgumentEmpty())
                        {
                            if (!argOperator.ResetOperator(argChildren, i))
                            {
                                SetError("ResetOperator is Error.");
                                return null;
                            }
                            end = argChildren.Count;
                            // 左側の引数を削除するため、i - 1が処理済み
                            if (argOperator.HasLeft && i != 0) i--;
                        }
                    }
                }
            }
            if (argChildren.Count == 1) return argChildren[0];
            SetError("ResetOperator argChildren.Count != 1.");
            return null;
        }

		
		private void ParseFixedValue(int startIndex, int index, Boolean isString)
		{
            if (index > startIndex)
            {
                String strValue = strFunction.Substring(startIndex, index - startIndex).Trim();
                if (strValue.Length > 0)
                {
                    if (isString) argList.Add(new FunctionParseData<T>(dataInfo, strValue, ParseDataTypeEnum.String));
                    else
                    {
                        String strLowerValue = strValue.ToLower();
                        if (FunctionParseOperatorLogicRelation<T>.IsRelation(strLowerValue)) argList.Add(new FunctionParseOperatorLogicRelation<T>(dataInfo, strLowerValue));
                        else
                        {
                            if (dataInfo.IsField(strValue)) argList.Add(new FunctionParseData<T>(dataInfo, strValue, ParseDataTypeEnum.Field));
                            else argList.Add(new FunctionParseData<T>(dataInfo, strValue, ParseDataTypeEnum.Other));
                        }
                    }
                }
            }
		}

        private void ParseGroup(int startIndex, int index)
		{
            String strValue = strLowerFunction.Substring(startIndex, index - startIndex).Trim();
            FunctionParseGroup<T> group = null;
            if (strValue.Length > 0 && FunctionParseOperatorLogicRelation<T>.IsRelation(strValue))
            {
                argList.Add(new FunctionParseOperatorLogicRelation<T>(dataInfo, strValue));
                strValue = String.Empty;
            }
			if (strValue.Length > 0)
			{
                if (FunctionParseMethod<T>.IsMethod(this, strValue)) group = new FunctionParseMethod<T>(this, strValue);
				else SetError("There is no method named:" + strValue);
			} else {
				int lastIndex = argList.Count - 1;
                if (lastIndex < 0) group = new FunctionParseGroup<T>(this);
				else 
				{
                    FunctionParseData<T> data = argList[lastIndex] as FunctionParseData<T>;
                    if (data == null || !data.IsOther()) group = new FunctionParseGroup<T>(this);
					else 
					{
						strValue = data.Value.ToString();
						argList.RemoveAt(lastIndex);
                        if (FunctionParseMethod<T>.IsMethod(this, strValue)) group = new FunctionParseMethod<T>(this, strValue);
						else SetError("There is no method named:" + strValue);
					}					
				}
			}
			if (group != null) 
			{
				argList.Add(group);
				group.StartGroup();
			}
		}

        protected internal override sealed Boolean CheckArgmentError()
        {
            if (base.CheckArgmentError()) return true;
            return (argList.Count != 1);
        }

        protected internal override sealed void SetResultFunction()
        {
            base.SetArgmentResultFunction();
            Object child = argList[0].GetResultFunctionOrData();
            if (child is FunctionParseDelegate<T>.GetResult) resultFunction = child as FunctionParseDelegate<T>.GetResult;
            else resultFunction = delegate(T t)
            {
                return child;
            };
        }

        public Object RunFunction(T t)
        {
            return resultFunction != null ? resultFunction(t) : null;
        }

        public Dictionary<String, Object> GetPropertyValueDictionary(T t)
        {
            return dataInfo.GetPropertyValueDictionary(t);
        }

        public List<FunctionParseResultInfo> GetPropertyValueList(T t)
        {
            return dataInfo.GetPropertyValueList(t);
        }
	}
}
