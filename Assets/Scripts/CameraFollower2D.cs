using UnityEngine;
using System.Collections;

public class CameraFollower2D : MonoBehaviour {
	public Transform target;
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
	}
}
