using UnityEngine;
using System.Collections;

public class ActionButton : MonoBehaviour {

	public UnitAvecActions unitAvecAction;
	public UnitAction unitAction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonClick() {
		Debug.Log (unitAction.nom);
	}
}
