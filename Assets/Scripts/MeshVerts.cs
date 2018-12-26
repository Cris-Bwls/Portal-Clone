using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVerts : MonoBehaviour {

	public Vector3[] vector3s;

	// Use this for initialization
	void Start ()
	{
		vector3s = GetComponent<MeshCollider>().sharedMesh.vertices;
	}
}
