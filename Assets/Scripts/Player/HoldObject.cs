using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

	[Header("Holdable Layer")]
	public int m_nHoldableLayer = 8;

	[Header("Range of Grab")]
	public float m_fGrabRange = 200.0f;

	private Outline m_LastOutline;

	// Update is called once per frame
	void Update()
	{
		Camera cam = Camera.main;
		Vector3 mousePos = Input.mousePosition;

		Ray ray = cam.ScreenPointToRay(mousePos);
		Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 1000), Color.black);

		RaycastHit hit;

		// This is a bitmask that makes it so we only check the Holdable layer 
		int layerMask = 1 << m_nHoldableLayer;

		// Check to see if we hit a Holdable object
		if (Physics.Raycast(ray, out hit, m_fGrabRange, layerMask))
		{
			var hitObject = hit.collider.gameObject;

			if (hitObject.GetComponent<Outline>() == null)
			{
				var outline = hitObject.AddComponent<Outline>();
				outline.OutlineMode = Outline.Mode.OutlineAll;

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
