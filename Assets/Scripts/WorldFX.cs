using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFX : MonoBehaviour {

	[Header("Mouse")]
	public bool m_bShowMouse = false;

	[Header("Physics")]
	public float m_fGravity = 10;

	// Use this for initialization
	void Start ()
	{
		Physics.gravity = Vector3.down * m_fGravity;		
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		Cursor.visible = m_bShowMouse;
	}
}
