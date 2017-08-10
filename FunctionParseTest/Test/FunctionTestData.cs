using System;
using Com.Rzgshome.Common.Function;
using System.Collections.Generic;
using System.Text;

namespace FunctionParseTest.Test
{
    public class FunctionTestData
    {
        public Decimal StaticNumber = 0m;
        public String PropertyName = "";
        public String PropertyValue = "";
        private static readonly FunctionTestData instance;
        protected static readonly List<FieldInfo<FunctionTestData>> lstStaticField;
        protected static readonly List<MethodInfo<FunctionTestData>> lstStaticMethod;
        static FunctionTestData()
        {
            instance = new FunctionTestData();
            lstStaticField = new List<FieldInfo<FunctionTestData>>();
            lstStaticMethod = new List<MethodInfo<FunctionTestData>>();
            AddStaticField(lstStaticField);
            AddStaticMethod(lstStaticMethod);
        }
        public static FunctionTestData GetInstance()
        {
            return instance;
        }

        private FunctionTestData() { }

        #region Field
        private static void AddStaticField(List<FieldInfo<FunctionTestData>> lstField)
        {
            lstField.Add(new FieldInfo<FunctionTestData>("StaticNumber", TypeEnum.Decimal, delegate(FunctionTestData data)
            {
                return data.StaticNumber;
            })); 
        }
        #endregion Field
        
        #region Method
        private static void AddStaticMethod(List<MethodInfo<FunctionTestData>> lstMethod)
        {
            lstMethod.Add(new MethodInfo<FunctionTestData>("RepeatTimes", TypeEnum.String, new List<TypeEnum>(new TypeEnum[] { TypeEnum.Int }), null, delegate(List<FunctionParseResultBase<FunctionTestData>> argList)
            {
                int arg = Convert.ToInt32(argList[0].GetResultWithData());
                return delegate(FunctionTestData data)
                {
                    StringBuilder sld = new StringBuilder();
                    for (int i = arg - 1; i >= 0; i--) sld.AppendLine(data.StaticNumber.ToString());
                    return sld.ToString();
                };
            }));
        }
        #endregion Method

        #region interface
        public void AddAllField(FunctionParseDataInfo<FunctionTestData> info)
        {
            // static以外
            info.AddField(new FieldInfo<FunctionTestData>(PropertyName, TypeEnum.String, delegate(FunctionTestData data)
            {
                return data.PropertyValue;
            }));
                foreach (FieldInfo<FunctionTestData> field in lstStaticField)
                {
                    try
                    {
                        info.AddField(field);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
        }
        
        public void AddAllMethod(FunctionParseDataInfo<FunctionTestData> info)
        {
            foreach (MethodInfo<FunctionTestData> method in lstStaticMethod) info.AddMethod(method);
        }
        #endregion interface
    }
}
