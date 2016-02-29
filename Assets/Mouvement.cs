using UnityEngine;
using System.Collections;

public class Mouvement : MonoBehaviour {
	public GameObject chemin;
	public float chevauxVapeur;
	public float vitesseMaximale;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		this.transform.Translate(new Vector3(.01f, 0f, 0f) * Time.deltaTime);
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Mathf.Clamp(chevauxVapeur + GetComponent<Rigidbody2D>().velocity.x, 0f, vitesseMaximale), 0f), ForceMode2D.Force);
	}
}
