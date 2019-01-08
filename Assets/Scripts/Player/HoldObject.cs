using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

	[Header("Holdable Layer")]
	public LayerMask m_HoldableLayer = 8;
	public LayerMask m_InTransitLayer = 8;

	[Header("Range of Grab")]
	public float m_fGrabRange = 200.0f;

	public Transform m_HoldSpot;

	private Outline m_LastOutline;
	private Held m_Held;

	// Update is called once per frame
	void Update()
	{
		Camera cam = Camera.main;
		Vector3 mousePos = Input.mousePosition;

		Ray ray = cam.ScreenPointToRay(mousePos);
		Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 1000), Color.black);

		RaycastHit hit;

		// This is a bitmask that makes it so we only check the Holdable layer
		int holdableMask = 1 << m_HoldableLayer;
		int inTransitMask = 1 << m_InTransitLayer;
		int layerMask = holdableMask | inTransitMask;

		if (m_Held)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Debug.LogWarning("Drop " + m_Held.gameObject.name);
				Destroy(m_Held);
			}
		}

		// Check to see if we hit a Holdable object
		if (Physics.Raycast(ray, out hit, m_fGrabRange, layerMask))
		{
			var hitObject = hit.collider.gameObject;

			if (m_Held)
			{
				if (Input.GetButtonDown("Fire1"))
				{
					Debug.LogWarning("Drop " + m_Held.gameObject.name);
					Destroy(m_Held);
				}

				return;
			}

			if (Input.GetButtonDown("Fire1"))
			{
				Debug.LogWarning("HELD " + hitObject.name);
				m_Held = hitObject.AddComponent<Held>();
				m_Held.m_HoldSpot = m_HoldSpot;
			}

			if (hitObject.GetComponent<Outline>() == null)
			{
				var outline = hitObject.AddComponent<Outline>();
				outline.OutlineMode = Outline.Mode.OutlineAll;
				outline.OutlineColor = Color.green;
				outline.OutlineWidth = 5.0f;

				if (m_LastOutline)
				{
					Destroy(m_LastOutline);
				}

				m_LastOutline = outline;
			}
		}
		else if (m_LastOutline)
		{
			Destroy(m_LastOutline);
		}
		
	}
}
