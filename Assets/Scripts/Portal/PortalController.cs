using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {

	public RenderTexture m_RenderTexture;
	public PortalInfo[] m_PortalInfo = new PortalInfo[2];

	// Use this for initialization
	void Start ()
	{
		int nLength = m_PortalInfo.Length;
		for (int i = 0; i < nLength; ++i)
		{
			PortalInfo current = m_PortalInfo[i];
			current.m_nIndex = i;

			current.m_Cam.targetTexture = new RenderTexture(m_RenderTexture);

			PortalInfo other = m_PortalInfo[nLength - 1 - i];
			other.m_View.material.mainTexture = current.m_Cam.targetTexture;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		foreach (PortalInfo portal in m_PortalInfo)
		{
			PortalTrigger trigger = portal.m_Trigger;
			if (trigger.m_Entered.Count > 0)
			{
				int nTargetIndex = m_PortalInfo.Length - 1 - portal.m_nIndex;
				PortalInfo target = m_PortalInfo[nTargetIndex];

				foreach(Rigidbody item in trigger.m_Entered)
				{
					TeleportObject(item, portal, target);
				}

				trigger.m_Entered.Clear();
			}
		}
	}

	public bool PlacePortal(int index)
	{
		bool bPlaced = false;

		return bPlaced;
	}

	private void TeleportObject(Rigidbody item, PortalInfo origin, PortalInfo target)
	{
		target.m_Trigger.m_InTransit.Add(item);

		Vector3 itemPos = item.transform.position;
		Vector3 originPos = origin.m_Output.position;
		Vector3 targetPos = target.m_Output.position;
		Vector3 offset = originPos - itemPos;

		Quaternion originRot = origin.m_Transform.rotation;
		Quaternion targetRot = target.m_Transform.rotation;
		Quaternion reverseRot = new Quaternion();
		reverseRot.eulerAngles = new Vector3(0, 0, 180);

		Quaternion newRot = originRot * Quaternion.Inverse(targetRot) * reverseRot;		

		offset = newRot * offset;

		item.transform.position = target.m_Output.position + offset;
		item.transform.rotation = item.transform.rotation * newRot;

		Debug.LogWarning(Time.time + " + " + newRot.eulerAngles);

		item.velocity = newRot * item.velocity;
		item.angularVelocity = newRot * item.angularVelocity;
	}
}
