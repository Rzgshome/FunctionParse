/*
 * Created by SharpDevelop.
 * User: rzgshome
 * Date: 2013/06/02
 * Time: 17:49
 */
using System;

namespace Com.Rzgshome.Common.Function
{
	/// <summary>
	/// Description of TypeEnum.
    /// </summary>	
    [Serializable]
	public enum TypeEnum
	{
		Int,
        Decimal,
		String,
		Boolean,
        Enum,
        Compute,
        DateTime,
		Invalid,
	}

    [Serializable]
	public enum ParseDataTypeEnum
	{
		Field,
        Decimal,
		String,
		Other,		
	}
}
