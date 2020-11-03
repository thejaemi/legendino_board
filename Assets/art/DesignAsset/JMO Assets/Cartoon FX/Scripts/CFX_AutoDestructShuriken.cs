using UnityEngine;
using System.Collections;

// [RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;
	// private ParticleSystem m_mainParticle;
	private ParticleSystem[] m_particles;
	
	void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
		m_particles = transform.GetComponentsInChildren<ParticleSystem>(true);
	}
	
	IEnumerator CheckIfAlive ()
	{
		bool finished = false;
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			for(int i = 0; i < m_particles.Length; i++)
			{
				finished = !m_particles[i].IsAlive();
				if(finished == false)
					break;
			}

			if(finished == true)
			{
				if(OnlyDeactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				else
					GameObject.Destroy(this.gameObject);
				break;
			}
			// if(!GetComponent<ParticleSystem>().IsAlive(true))
			// {
			// 	if(OnlyDeactivate)
			// 	{
			// 		#if UNITY_3_5
			// 			this.gameObject.SetActiveRecursively(false);
			// 		#else
			// 			this.gameObject.SetActive(false);
			// 		#endif
			// 	}
			// 	else
			// 		GameObject.Destroy(this.gameObject);
			// 	break;
			// }
		}
	}
}
