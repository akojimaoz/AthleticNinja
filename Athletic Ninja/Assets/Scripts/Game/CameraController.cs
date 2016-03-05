using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		transform.position = offset;
	}
	
	// Update is called once per frame
	void Update () {

		//	transform.position = target.position + offset;
		//transform.rotation = target.rotation;
	}
}
