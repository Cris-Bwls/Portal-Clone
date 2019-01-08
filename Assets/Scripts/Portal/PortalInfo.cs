using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInfo : MonoBehaviour
{
	public int m_nIndex;
	public Transform m_Transform;
	public Camera m_Cam;
	public MeshRenderer m_View;
	public PortalTrigger m_Trigger;
	public BoxCollider[] m_PortalColliders;
	public Transform m_Output;
	public GameObject m_ObjectAgainst;

	private void Awake()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var playerCollider = player.GetComponent<Collider>();

		foreach (BoxCollider collider in m_PortalColliders)
		{
			Physics.IgnoreCollision(playerCollider, collider, false);
		}
	}
}
