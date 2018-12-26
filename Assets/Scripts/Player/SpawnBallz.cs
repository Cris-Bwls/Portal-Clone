using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallz : MonoBehaviour {

	public GameObject m_Ball;
	public float m_fForce;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Camera cam = Camera.main;
			Vector3 mousePos = Input.mousePosition;

			Vector3 pos = cam.ScreenToWorldPoint(mousePos);
			Ray ray = cam.ScreenPointToRay(mousePos);

			var ball = Instantiate(m_Ball, pos, Quaternion.LookRotation(ray.direction));

			AddForce(ball);
		}
	}

	void AddForce(GameObject ball)
	{
		var rb = ball.GetComponent<Rigidbody>();
		rb.AddForce(ball.transform.forward * m_fForce);
	}
}
