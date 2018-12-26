using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonedFX : MonoBehaviour
{
	[System.Serializable]
	public struct Clones
	{
		public Rigidbody first;
		public Rigidbody second;
		public float fPercent;
	}

	public Clones[] m_Clones;

	void FixedUpdate()
	{
		foreach (Clones clones in m_Clones)
		{
			var first = clones.first;
			var second = clones.second;

			var firstRot = first.transform.rotation;
			var secondRot = second.transform.rotation;

			Vector3 angVel = QRotate(first.angularVelocity, Quaternion.Inverse(firstRot)) * (1 - clones.fPercent);
			angVel += QRotate(second.angularVelocity, Quaternion.Inverse(secondRot)) * clones.fPercent;

			Vector3 vel = QRotate(first.velocity, Quaternion.Inverse(firstRot)) * (1 - clones.fPercent);
			vel += QRotate(second.velocity, Quaternion.Inverse(secondRot)) * clones.fPercent;

			first.angularVelocity = QRotate(angVel, firstRot);
			second.angularVelocity = QRotate(angVel, secondRot);

			first.velocity = QRotate(vel, firstRot);
			second.velocity = QRotate(vel, secondRot);
		}
	}

	Vector3 QRotate(Vector3 input, Quaternion rotation)
	{
		return rotation * input;
	}
}
