/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 17:44
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FieldInfo.
    /// </summary>
    [Serializable]
    public class FieldInfo<T>
	{
		private readonly String name;
		public String Name{get{return name;}}
        private readonly Boolean isFixed;
        public Boolean Fixed { get { return isFixed; } }
        private readonly TypeEnum type;
        public TypeEnum Type { get { return type; } }
        private readonly FunctionParseDelegate<T>.GetResult resultFn;
        public FunctionParseDelegate<T>.GetResult ResultFn { get { return resultFn; } }
        public FieldInfo(String name, TypeEnum type, FunctionParseDelegate<T>.GetResult resultFn, Boolean isFixed = false)
		{
			this.name = name;
			this.type = type;
            this.resultFn = resultFn;
            this.isFixed = isFixed;
		}
	}
}
