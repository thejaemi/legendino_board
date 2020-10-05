using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldHelper {

	class IntComparer:IEqualityComparer<int>{
		bool IEqualityComparer<int>.Equals(int x, int y){
			return x == y;
		}

		int IEqualityComparer<int>.GetHashCode(int obj){
			return obj.GetHashCode();
		}
	}
	private static Dictionary<int, WaitForSeconds> dicTimeYielder = new Dictionary<int, WaitForSeconds>(new IntComparer());
	private static WaitForEndOfFrame endOfFrameYielder = new WaitForEndOfFrame();
	private static WaitForFixedUpdate fixedUpdateYielder = new WaitForFixedUpdate();

	public static WaitForSeconds waitForSeconds(int miliSec)
	{
		WaitForSeconds pYielder = null;

		if(!dicTimeYielder.TryGetValue(miliSec, out pYielder))
		{
			pYielder = new WaitForSeconds(miliSec * 0.001f);
			dicTimeYielder.Add(miliSec, pYielder);
		}
		return pYielder;
	}

	public static WaitForEndOfFrame waitForEndOfFrame()
	{
		return endOfFrameYielder;
	}

	public static WaitForFixedUpdate waitForFixedUpdate()
	{
		return fixedUpdateYielder;
	}

	
}
