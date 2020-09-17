using System;
using System.Text;
using System.Collections.Generic;

public class Utils {

    public static Array GetEnumValues<T>()
    {
        Array arr = Enum.GetValues(typeof(T));
        return arr;
    }

	/// <summary>
	/// json 데이터 yn 값으로 넘어오는 경우
	/// </summary>
	/// <param name="_value"></param>
	/// <returns></returns>
	public static bool WebBoolValueConverter(string _value)
	{
		bool value = false;
		if(_value.Equals("Y"))
			value = true;

		return value;
	}
    
    public static bool WebBoolValueConverter(int _value)
    {
        bool value = false;
        if(_value > 0)
            value = true;

        return value;
    }

    public static string ConvertIntToStringBool(int _value)
    {
        string value = "N";
        if(_value > 0)
            value = "Y";

        return value;
    }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="_value"></param>
	/// <returns></returns>
	public static byte ConvertIntToByte(int _value){

		// byte value = Convert.ToByte(_value);
		// try {
		// 	value = Convert.ToByte(_value);
		// }                     
		// catch (OverflowException) {
		// 	Debug.Log("Value is overflow to byte ");
		// }

		// return value;
		return Convert.ToByte(_value);
	}

	public static T ConvertStringToEnumData<T>( string _value )
    {
        return (T)Enum.Parse( typeof( T ), _value );
    }

	public static T ConvertIntToEnumData<T>(int _value)
	{
		return (T)Enum.ToObject(typeof(T), _value);
	}

	public static int ConvertCharToInt(char _value)
	{
		return Convert.ToInt32(_value);
	}

	public static T ConvertCharToEnumData<T>(char _value)
	{
		return (T)Enum.ToObject(typeof(T), _value);
	}

    public static char ConvertStringToChar(string _value)
    {
        return _value[0];
    }

	public static void InvokeAction(System.Action action)
	{
		if(action != null && action.GetInvocationList().Length > 0)
		{
			action();
		}
	}

    public static void InvokeAction(System.Action<object[]> action, params object[] parameters)
    {
        if (action != null && action.GetInvocationList().Length > 0)
        {
            action(parameters);
        }
    }

    public static void InvokeAction<T>(System.Action<T> action, T parameter)
    {
        if(action != null && action.GetInvocationList().Length > 0)
        {
            action(parameter);
        }
    }

    public static string GetActionName(DinoAction _action)
    {
        string actionName = "";
        switch(_action)
        {
            case DinoAction.NormalAttack :
                actionName = "공격";
                break;

            case DinoAction.Defence :
                actionName = "방어";
                break;

            case DinoAction.Counter :
                actionName = "카운터";
                break;
            case DinoAction.SpecialAttack :
                actionName = "스페셜 공격";
                break;
        }
        return actionName;
    }

    // public static TimeSpan GetRemainTimeSpanToFinishDateTime(DateTime remainTime)
    // {
    //     TimeSpan span = remainTime - ServerSyncTime.now;
    //     return span;
    // }

    // public static string GetRemainTimeStringFormatDD(TimeSpan timeSpan)
    // {
    //     string ddFormat = "{0}일";
    //     return string.Format(ddFormat, timeSpan.Days);
    // }

    // public static string GetRemainTimeStringFormatHHMM(TimeSpan timeSpan)
    // {
    //     string hhmmFormat = UITextLibrary.GetText("TimeFormat_hhmm");
    //     string hhFormat = UITextLibrary.GetText("TimeFormat_hh");
    //     string mmFormat = UITextLibrary.GetText("TimeFormat_mm");

    //     if (timeSpan.Minutes > 0 && timeSpan.Hours == 0)
    //         return string.Format(mmFormat, timeSpan.Minutes);
    //     else if (timeSpan.Hours > 0 && timeSpan.Minutes == 0)
    //         return string.Format(hhFormat, timeSpan.Hours);

    //     return string.Format(hhmmFormat, timeSpan.Hours, timeSpan.Minutes);
    // }

    // public static string GetRemainTimeStringFormatMMSS(TimeSpan timeSpan)
    // {
    //     string mmssFormat = UITextLibrary.GetText("TimeFormat_mmss");
    //     string mmFormat = UITextLibrary.GetText("TimeFormat_mm");
    //     string ssFormat = UITextLibrary.GetText("TimeFormat_ss");

    //     if (timeSpan.Seconds > 0 && timeSpan.Minutes == 0)
    //         return string.Format(ssFormat, timeSpan.Seconds);
    //     else if (timeSpan.Minutes > 0 && timeSpan.Seconds == 0)
    //             return string.Format(mmFormat, timeSpan.Minutes);

    //     return string.Format(mmssFormat, timeSpan.Minutes, timeSpan.Seconds);
    // }

    // public static string GetSpendTimeStringColonMMSS(float spendTime)
    // {
    //     int minute = (int)spendTime / 60;
    //     int seconds = (int)spendTime % 60;
    //     string timeFormat = "{0:00}:{1:00}";

    //     return string.Format(timeFormat, minute, seconds);
    // }

    // //public static string GetAppendedSentence(string[] words)
    // //{
    // //    if (words == null)
    // //        return string.Empty;
    // //    StringBuilder stringBuilder = new StringBuilder();

    // //    for (int i = 0; i < words.Length; i++)
    // //    {
    // //        stringBuilder.Append(words[i]);
    // //    }

    // //    return stringBuilder.ToString();
    // //}


    // public static string GetAppendedSentence(params string[] words)
    // {
    //     if (words == null)
    //         return string.Empty;
    //     StringBuilder stringBuilder = new StringBuilder();

    //     for (int i = 0; i < words.Length; i++)
    //     {
    //         stringBuilder.Append(words[i]);
    //     }

    //     return stringBuilder.ToString();
    // }

    // public static T[] GetShuffledArray<T>(T[] array)
    // {
    //     int length = array.Length;

    //     for (int i = 0; i < length; i++)
    //     {
    //         int randNum = UnityEngine.Random.Range(0, length);
    //         T temp = array[i];
    //         array[i] = array[randNum];
    //         array[randNum] = temp;
    //     }
    //     return array;
    // }

    // public static string GetDotText(int value)
    // {
    //     string dot = "";
    //     switch (value)
	// 	{
	// 	default:
	// 	case 0: dot = "    ";
    //     break;
	// 	case 1: dot = ".   ";
    //     break;
	// 	case 2: dot = "..  ";
    //     break;
	// 	case 3: dot = "... ";
    //     break;
	// 	}
    //     return dot;
    // }

    // public static string GetGoodsCountTextWithComma(int value)
    // {
    //     return string.Format("{0:#,##0}", value);
    // }

    // public static T GetCreatedObjectComponent<T>(UnityEngine.GameObject obj, UnityEngine.Transform parent = null) where T : UnityEngine.MonoBehaviour
    // {
    //     UnityEngine.GameObject go = UnityEngine.MonoBehaviour.Instantiate<UnityEngine.GameObject>(obj.gameObject);
        
    //     if(parent != null)
    //         go.transform.SetParent(parent);

    //     go.transform.localPosition = UnityEngine.Vector3.zero;
    //     go.transform.localScale = UnityEngine.Vector3.one;

    //     T slotComponent = go.GetComponent<T>();
    //     return slotComponent;
    // }

	// //jsonparse data convert
	// public static Dictionary<string , string> ConvertDictionaryValueToString( Dictionary<string,object> _originDic)
	// {
	// 	Dictionary<string, string> newDic = new Dictionary<string, string>();
	// 	foreach (KeyValuePair<string, object> keyValuePair in _originDic)
	// 	{
	// 		newDic.Add(keyValuePair.Key, keyValuePair.Value.ToString());
	// 	}
	// 	return newDic;
	// }

    // public static string GetItemSpriteName(int itemIndex)
    // {
    //     if(itemIndex < 0)
    //         itemIndex = 0;
            
    //     return string.Format("Item_{0}", itemIndex);
    // }

    // public static string GetItemBackgroundSpriteName(int grade)
    // {
    //     return string.Format("IconBG_Grade_{0}", grade);
    // }
}
