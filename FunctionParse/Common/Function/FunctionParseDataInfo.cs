/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 20:26
 */
using System;
using System.Collections.Generic;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseDataInfo.
    /// </summary>
    [Serializable]
    public class FunctionParseDataInfo<T>
	{
        protected readonly Dictionary<String, MethodInfo<T>> dicMethodList;
		protected readonly Dictionary<String, FieldInfo<T>> dicField;
		public FunctionParseDataInfo()
		{
            dicMethodList = new Dictionary<String, MethodInfo<T>>();
            dicField = new Dictionary<String, FieldInfo<T>>();

		}
        public void AddField(FieldInfo<T> info)
        {
            dicField.Add(info.Name.ToLower(), info);
        }

        public void ReplaceField(FieldInfo<T> info)
        {
            dicField[info.Name.ToLower()] = info;
        }

        public void AddMethod(MethodInfo<T> info)
        {
            dicMethodList.Add(info.Name.ToLower(), info);
        }
		
		public Boolean IsField(String name) 
		{
            return dicField.ContainsKey(name.ToLower());
		}

        public Boolean IsFixedField(String name)
        {
            return dicField.ContainsKey(name.ToLower()) && dicField[name.ToLower()].Fixed;
        }
		
		public Boolean IsMethod(String name) 
		{
			return dicMethodList.ContainsKey(name);
		}
		
		public TypeEnum GetFieldResultType(String name)  
		{
            return dicField[name.ToLower()].Type;
		}

        public MethodInfo<T> GetMethodInfo(String name)
        {
            return dicMethodList[name];
        }

        public FunctionParseDelegate<T>.GetResult GetFieldResultFunction(String name) {
            name = name.ToLower();
            return dicField.ContainsKey(name) ?  dicField[name].ResultFn : null;
        }

        public Dictionary<String, Object> GetPropertyValueDictionary(T t)
        {
            Dictionary<String, Object> dic = new Dictionary<string, object>();
            foreach (String key in dicField.Keys) 
            {
                FieldInfo<T> fieldInfo = dicField[key];
                if (!fieldInfo.Fixed)
                {
                    if (fieldInfo.ResultFn == null) dic.Add(key, null);
                    else dic.Add(key, fieldInfo.ResultFn(t));
                }
            }
            return dic;
        }

        public List<FunctionParseResultInfo> GetPropertyValueList(T t)
        {
            List<FunctionParseResultInfo> lstValue = new List<FunctionParseResultInfo>();
            foreach (String key in dicField.Keys)
            {
                FieldInfo<T> fieldInfo = dicField[key];
                if (!fieldInfo.Fixed)
                {
                    if (fieldInfo.ResultFn == null) lstValue.Add(new FunctionParseResultInfo(key, fieldInfo.Type, null));
                    else lstValue.Add(new FunctionParseResultInfo(key, fieldInfo.Type, fieldInfo.ResultFn(t)));
                }               
            }
            return lstValue;
        }
	}

    [Serializable]
    public class FunctionParseResultInfo
    {
        private readonly String key;
        private readonly TypeEnum type;
        private readonly Object value;

        public String Key { get { return key; } }
        public TypeEnum Type { get { return type; } }
        public Object Value { get { return value; } }
        public FunctionParseResultInfo(String key, TypeEnum type, Object value)
        {
            this.key = key;
            this.type = type;
            this.value = value;
        }
    }
}
