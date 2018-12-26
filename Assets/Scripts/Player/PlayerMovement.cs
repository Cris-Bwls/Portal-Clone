using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody m_Rigid;
	private Camera m_Cam;
	private float m_fMouseMulti = 10f;
	private float m_fTransMulti = 50f;
	private float m_fJumpForce = 20f;
	private float m_fJumpDelay = 2.0f;
	private float m_fJumpEndDelay = 0.0f;

	private void Awake()
	{
		m_Rigid = GetComponent<Rigidbody>();
		m_Cam = Camera.main;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		float fDeltaTime = Time.fixedDeltaTime;

		// Zero X and Z rotation
		var rot = m_Rigid.rotation;
		Vector3 eulerRot = Vector3.zero;
		eulerRot.y = rot.eulerAngles.y;
		rot.eulerAngles = eulerRot;
		m_Rigid.rotation = rot;

		// Rotate Player
		float fMouseX = Input.GetAxis("Mouse X");
		Debug.Log("fMouseX = " + fMouseX);

		Vector3 v3Torque = Vector3.up * fMouseX * m_fMouseMulti * fDeltaTime;
		m_Rigid.angularVelocity += v3Torque;

		// Look Up/Down
		float fMouseY = Input.GetAxis("Mouse Y");
		Debug.Log("fMouseY = " + fMouseY);

		// Create new camera angle
		var camRot = m_Cam.transform.localEulerAngles;
		camRot.x += fMouseY * m_fMouseMulti * 5 * fDeltaTime;

		// Clamp camera angle
		if (camRot.x < 360.0f - 50.0f && camRot.x > 50.0f)
		{
			if (camRot.x < 180.0f)
			{
				camRot.x = 50.0f;
			}
			else
			{
				camRot.x = 360.0f - 50.0f;
			}
		}

		// Set new camera angle
		m_Cam.transform.localEulerAngles = camRot;

		// Translation Movement
		float fHor = Input.GetAxis("Horizontal");
		Vector3 v3Hor = transform.right * fHor * m_fTransMulti * fDeltaTime;

		float fVer = Input.GetAxis("Vertical");
		Vector3 v3Ver = transform.forward * fVer * m_fTransMulti * fDeltaTime;

		m_Rigid.velocity += (v3Hor + v3Ver);

		// Jump
		if (Input.GetAxis("Jump") > 0.5f && Time.fixedTime > m_fJumpEndDelay)
		{
			m_fJumpEndDelay = Time.fixedTime + m_fJumpDelay;

			m_Rigid.velocity += transform.up * m_fJumpForce;
		}
		
	}
}
