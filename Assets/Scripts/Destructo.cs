using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructo : MonoBehaviour {

	[System.Serializable]
	public struct Bounds
	{
		public Vector3 min;
		public Vector3 max;
	}

	[Header("Postitional Destruction")]
	public bool m_bUseBounds;
	public Bounds m_Bounds;

	[Header("Timed Destruction")]
	public bool m_bUseTimer;
	public float m_fDuration;
	private float m_fTimeOfDeath;

	// Use this for initialization
	void Awake ()
	{
		StartTimer();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (BoundsCheck() || TimeCheck())
		{
			Destroy(gameObject);
		}
	}

	bool BoundsCheck()
	{
		bool output = false;

		if (m_bUseBounds)
		{
			var pos = transform.position;

			if (pos.x < m_Bounds.min.x || pos.x > m_Bounds.max.x)
			{
				output = true;
			}
			else if (pos.y < m_Bounds.min.y || pos.y > m_Bounds.max.y)
			{
				output = true;
			}
			else if (pos.z < m_Bounds.min.z || pos.z > m_Bounds.max.z)
			{
				output = true;
			}
		}

		return output;
	}

	void StartTimer()
	{
		if (m_bUseTimer)
		{
			m_fTimeOfDeath = Time.time + m_fDuration;
		}
	}

	bool TimeCheck()
	{
		bool output = false;

		if (m_bUseTimer)
		{
			if (Time.time > m_fTimeOfDeath)
			{
				output = true;
			}
		}

		return output;
	}
}
