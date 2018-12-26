using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
	public List<Rigidbody> m_Entered = new List<Rigidbody>();
	public List<Rigidbody> m_InTransit = new List<Rigidbody>();


	public void OnTriggerEnter(Collider other)
	{
		GameObject otherObject = other.gameObject;
		Rigidbody otherRB = otherObject.GetComponent<Rigidbody>();
		if (otherRB)
		{
			if (!m_InTransit.Contains(otherRB))
			{
				m_Entered.Add(otherRB);
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		GameObject otherObject = other.gameObject;
		Rigidbody otherRB = otherObject.GetComponent<Rigidbody>();
		if (otherRB)
		{
			if (m_InTransit.Contains(otherRB))
			{
				m_InTransit.Remove(otherRB);
			}
		}
	}
}
