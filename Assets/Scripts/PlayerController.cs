using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour {
	public float maxSpeed;

	public float thrustValue = 1f;

	public Vector3 moveTarget;

	private Ship ship;

	private Rigidbody2D rBody;
	// Use this for initialization
	void Start () {
		rBody = this.GetComponent<Rigidbody2D>();
		ship = this.GetComponent<Ship>();
	}
	
	void OnMouseUpAsButton() {
		//moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//moveTarget.z = this.transform.position.z;
	}

	void Update(){
		GameObject target = null;
		Collider2D[] touches;
		if (Input.GetMouseButtonUp(0)){
			touches = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			foreach (Collider2D c in touches){
				Debug.Log(c.tag);
				if (c.gameObject.tag == "Block"){// && Vector3.Distance(this.transform.position, c.transform.position) < 1){
					target = c.gameObject;
					break;
				} else if (c.gameObject.tag == "Station"){
					Application.LoadLevel("Starmap");
				}
			}
		}

		if (target != null){
			touches = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.Distance(this.transform.position, target.transform.position));
			foreach (Collider2D c in touches){
				if (c.gameObject.tag != "Block" ) continue;

				if (Vector3.Distance(this.transform.position, c.transform.position) < Vector3.Distance(this.transform.position, target.transform.position) ){
					target = c.gameObject;
				}
			}
			if (ship.AddCargo(target.transform.parent.GetComponent<Block>().type)){
				Destroy(target.transform.parent.gameObject);
			}
		} else if(Input.GetMouseButton(0)){
			moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			moveTarget.z = this.transform.position.z;
			Thrust();
		}
	}

	void Thrust(){
		LookAtYConstZ(this.transform, moveTarget);

		//Vector3 lookDirection = this.transform.TransformDirection(moveTarget).normalized;

		this.rBody.AddForce(this.transform.TransformDirection(Vector2.up.x, Vector2.up.y, 0));
	}

	public void LookAtYConstZ(Transform t, Vector3 target)
	{
		transform.rotation = CalculateLook(t, target);
	}
	
	protected Quaternion CalculateLook(Transform t, Vector3 target){
		// Fix X pointing at the target, and maintain the current Z direction
		Vector3 newX = target - transform.position;
		Vector3 newZ = t.forward;
		
		// Calculate new Y direction
		//Vector3 newY = Vector3.Cross(newZ, newX);
		
		// Let the library method do the heavy lifting
		return Quaternion.LookRotation(newZ, newX);
	}
}
