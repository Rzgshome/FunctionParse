/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 20:19
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of FunctionParseBase.
	/// </summary>
    [Serializable] 
    public class FunctionParseBase<T> : FunctionParseResultBase<T>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Boolean isNoError = true;
        protected List<FunctionParseResultBase<T>> argList = new List<FunctionParseResultBase<T>>();
        public FunctionParseBase(FunctionParseDataInfo<T> dataInfo) : base(dataInfo)
		{			
		}

        protected internal virtual void ResetChildren() 
        {
            for (int i = argList.Count - 1; i >= 0; i--)
            {
                FunctionParseBase<T> child = argList[i] as FunctionParseBase<T>;
                if (child != null) child.ResetChildren();
            }
        }

        protected internal override Boolean CheckArgmentError()
        {
            return CheckArgmentListError();
        }


        protected Boolean CheckArgmentListError()
        {
            for (int i = argList.Count - 1; i >= 0; i--)
            {
                if (argList[i] == null || argList[i].CheckArgmentError()) return true;
            }
            return false;
        }
		
		protected internal override void SetResultFunction()
		{
			SetArgmentResultFunction();
		}
		
		protected void SetArgmentResultFunction()
		{
			for (int i = argList.Count - 1; i >= 0; i--) argList[i].SetResultFunction();
		}

        protected Boolean SetError(String error)
        {
            isNoError = false;
            log.Error(error);
            return true;
        }

        public override string ToString()
        {
            if (argList.Count == 0) return "";
            else
            {
                StringBuilder sld = new StringBuilder();
                for (int i = 0, lastIndex = argList.Count - 1; i <= lastIndex; i++)
                {
                    sld.Append(argList[i].ToString());
                    if (i != lastIndex) sld.Append(",");
                }
                return sld.ToString();
            }
        }

        protected static Decimal? ParseDecimalOrNull(String str)
        {
            try
            {
                if (str == null) return null;
                str = str.Replace(",", "").Trim();
                return Decimal.Parse(str);
            }
            catch (FormatException)
            {
                return null;
            }
        }
	}
}
