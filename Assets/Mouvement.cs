using UnityEngine;
using System.Collections;

public class Mouvement : MonoBehaviour {
	public GameObject chemin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		this.transform.Translate(new Vector3(.01f, 0f, 0f) * Time.deltaTime);
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (6f, 0f), ForceMode2D.Force);
	}
}
