using UnityEngine;
using System.Collections;

public class ActionButton : MonoBehaviour {

	public UnitAvecActions unitAvecAction;
	public UnitAction unitAction;
	public AudioClip audioClip;

	public void Apply()
	{
		unitAction.Action ();
	}
}
