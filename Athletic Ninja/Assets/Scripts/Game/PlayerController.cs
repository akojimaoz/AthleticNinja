using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float MoveSpeed = 0.1f;     //移動速度
	public float gravityScale = 20.0f; //重力
	public float jumpForce = 15f;      //ジャンプ力

	Rigidbody rb;
	Vector3 gravity;
	bool isGround = true;              //地面判定

	public enum TYPE{
		TOP,
		BOTTOM,
		LEFT,
		RIGHT,
		NEAR,
		FAR,
		TOP_LEFT,
		TOP_RIGHT,
		BOTTOM_LEFT,
		BOTTOM_RIGHT
	};
		
	public TYPE type; //重力を下に初期化
	Vector3[] gravityType = new Vector3[10];

	void Start () {
		rb = GetComponent<Rigidbody> ();
		type = TYPE.BOTTOM;
		gravityType [(int)TYPE.TOP]          = new Vector3 (0.0f, 1.0f, 0.0f);
		gravityType [(int)TYPE.BOTTOM]       = new Vector3 (0.0f, -1.0f, 0.0f);
		gravityType [(int)TYPE.LEFT]         = new Vector3 (-1.0f, 0.0f, 0.0f);
		gravityType [(int)TYPE.RIGHT]        = new Vector3 (1.0f, 0.0f, 0.0f);
		gravityType [(int)TYPE.NEAR]         = new Vector3 (0.0f, 0.0f, -1.0f);
		gravityType [(int)TYPE.FAR]          = new Vector3 (0.0f, 0.0f, 1.0f);
		gravityType [(int)TYPE.TOP_LEFT]     = new Vector3 (-0.7071f, 0.7071f, 0.0f);
		gravityType [(int)TYPE.TOP_RIGHT]    = new Vector3 (0.7071f, 0.7071f, 0.0f);
		gravityType [(int)TYPE.BOTTOM_LEFT]  = new Vector3 (-0.7071f, -0.7071f, 0.0f);
		gravityType [(int)TYPE.BOTTOM_RIGHT] = new Vector3 (0.7071f, -0.7071f, 0.0f);

	}
	
	void FixedUpdate () {
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");
		if (x != 0) {
			transform.position += transform.right * MoveSpeed * x;
		}
		if (z != 0) {	
			transform.position += transform.forward * MoveSpeed * z;
		}
			
		AddGravity ();

	}

	void Update() {
		if (isGround && Input.GetKeyDown ("space")) {
			Jump ();
		}

	}


	void OnCollisionEnter(Collision collision) {
		Debug.Log("isGroung = " + isGround); 
		if(collision.gameObject.tag == "Ground") {
			isGround = true;
		}
	}

	void OnCollisionExit(Collision collision) {

		if(collision.gameObject.tag == "Ground") {
			isGround = false;
		}
		Debug.Log("isGroung = " + isGround); 
	}
		
	void OnTriggerEnter(Collider other){
		switch (type) {
		case TYPE.TOP:
			switch (other.gameObject.tag) {
			case "TopLeft":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 225.0f);
				type = TYPE.TOP_LEFT;
				break;
			case "TopRight":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 135.0f);
				type = TYPE.TOP_RIGHT;
				break;
			default:
				break;
			}
			break;
		
		case TYPE.BOTTOM:
			switch (other.gameObject.tag) {
			case "BottomLeft":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, -45.0f);
				type = TYPE.BOTTOM_LEFT;
				break;
			case "BottomRight":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 45.0f);
				type = TYPE.BOTTOM_RIGHT;
				break;
			default:
				break;
			}
			break;

		case TYPE.LEFT:
			switch (other.gameObject.tag) {
			case "TopLeft":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 225.0f);
				type = TYPE.TOP_LEFT;
				break;
			case "BottomLeft":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, -45.0f);
				type = TYPE.BOTTOM_LEFT;
				break;
			default:
				break;
			}
			break;

		case TYPE.RIGHT:
			switch (other.gameObject.tag) {
			case "TopRight":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 135.0f);
				type = TYPE.TOP_RIGHT;
				break;
			case "BottomRight":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 45.0f);
				type = TYPE.BOTTOM_RIGHT;
				break;
			default:
				break;
			}
			break;

		case TYPE.TOP_LEFT:
			switch (other.gameObject.tag) {
			case "Top":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 180.0f);
				type = TYPE.TOP;
				break;
			case "Left":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, -90.0f);
				type = TYPE.LEFT;
				break;
			default:
				break;
			}
			break;

		case TYPE.TOP_RIGHT:
			switch (other.gameObject.tag) {
			case "Top":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 180.0f);
				type = TYPE.TOP;
				break;
			case "Right":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 90.0f);
				type = TYPE.RIGHT;
				break;
			default:
				break;
			}
			break;

		case TYPE.BOTTOM_LEFT:
			switch (other.gameObject.tag) {
			case "Bottom":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
				type = TYPE.BOTTOM;
				break;
			case "Left":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, -90.0f);
				type = TYPE.LEFT;
				break;
			default:
				break;
			}
			break;
		case TYPE.BOTTOM_RIGHT:
			switch (other.gameObject.tag) {
			case "Bottom":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
				type = TYPE.BOTTOM;
				break;
			case "Right":
				transform.rotation = Quaternion.Euler (0.0f, 0.0f, 90.0f);
				type = TYPE.RIGHT;
				break;
			default:
				break;
			}
			break;

		}
	}



	void Jump() {
		rb.velocity = new  Vector3 (rb.velocity.x , jumpForce, rb.velocity.z);
	}


	void AddGravity(){
		float x, y, z;
		x = gravityScale * gravityType[(int)type].x;
		y = gravityScale * gravityType[(int)type].y;
		z = gravityScale * gravityType[(int)type].z;
		gravity = new Vector3 (x, y, z);
		rb.AddForce (gravity, ForceMode.Acceleration);
	}
		


}