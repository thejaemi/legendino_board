using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 소팅 오더 (레이어 뎁스 설정)에 이용
/// 일단 AddComponent로 작업하고 이펙트 관리 관련하여 모든 이펙트에 기본 컴포넌트로 가져갈지 결정
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
[DisallowMultipleComponent]
public class EffectController : MonoBehaviour {
	private ParticleSystem m_particleSystem;
	private ParticleSystemRenderer[] m_allEffectRenderer;

	void OnEnable()
	{
		if(m_particleSystem == null)
			m_particleSystem = GetComponent<ParticleSystem>();
#if UNITY_EDITOR_WIN || UNITY_EDITOR_OSX
		if(!runInEditMode)
			ActiveDeactivateChecker();
#elif UNITY_ANDROID
		ActiveDeactivateChecker();
#endif 
	}

	private void ActiveDeactivateChecker()
	{
		StopCoroutine("CheckIfAlive");
		StartCoroutine("CheckIfAlive");
	}

	private IEnumerator CheckIfAlive ()
	{
		if(m_particleSystem == null)
			m_particleSystem = GetComponent<ParticleSystem>();
			
		while(m_particleSystem.IsAlive(true))
			yield return YieldHelper.waitForSeconds(500);

		gameObject.SetActive(false);
		OnDead();
	}

	protected virtual void OnDead(){}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="belowDepth"></param>
	public void SetEffectAbove(int belowDepth)
	{
		if(m_allEffectRenderer == null)
		{
			m_allEffectRenderer = GetComponentsInChildren<ParticleSystemRenderer>(true);
		}

		int effDepth = belowDepth + 1;
		for(int i = 0; i < m_allEffectRenderer.Length; i++)
		{
			m_allEffectRenderer[i].sortingOrder = effDepth;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="aboveDepth"></param>
	public void SetEffectBelow(int aboveDepth)
	{
		if(m_allEffectRenderer == null)
		{
			m_allEffectRenderer = GetComponentsInChildren<ParticleSystemRenderer>(true);
		}

		int effDepth = aboveDepth - 1;
		for(int i = 0; i < m_allEffectRenderer.Length; i++)
		{
			m_allEffectRenderer[i].sortingOrder = effDepth;
		}
	}

	public void SetEffectDepth(int depth)
	{
		if(m_allEffectRenderer == null)
		{
			m_allEffectRenderer = GetComponentsInChildren<ParticleSystemRenderer>(true);
		}

		for(int i = 0; i < m_allEffectRenderer.Length; i++)
		{
			m_allEffectRenderer[i].sortingOrder = depth;
		}
	}
}
