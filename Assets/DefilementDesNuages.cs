using UnityEngine;
using System.Collections;

public class DefilementDesNuages : MonoBehaviour {

	public GameObject voiture;
	public float vitesseDefilementNuages = 10f;
	private Vector3 origine;

	// Use this for initialization
	void Start () {
		transform.position = voiture.transform.position;
		origine = voiture.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = voiture.transform.position;

		Vector3 offset = (voiture.transform.position - origine);
		Material material = GetComponent<MeshRenderer> ().material;
		material.mainTextureOffset = new Vector2 (offset.x / vitesseDefilementNuages, offset.y / vitesseDefilementNuages);
	}
}
