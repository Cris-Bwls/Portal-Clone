using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Held : MonoBehaviour {

	public Transform m_HoldSpot;
	public float m_fHoldForce = 1000.0f;

	private Rigidbody m_Rigidbody;

	private void Awake()
	{
		Setup();
	}

	public void Setup()
	{
		m_Rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_Rigidbody && m_HoldSpot)
		{
			var forceDir = m_HoldSpot.position - transform.position;
			forceDir.Normalize();

			m_Rigidbody.AddForce(forceDir * m_fHoldForce);
		}
	}
}
