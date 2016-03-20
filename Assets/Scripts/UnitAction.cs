using UnityEngine;
using System.Collections;

public abstract class UnitAction : MonoBehaviour {
	public string nom;
	[SerializeField]
	public bool active = true;
	public Sprite icon;
	public bool oncePerUnit = false;

	public virtual void Action ()
	{
		if (this.oncePerUnit) {
			this.active = false;
		}
	}
}
