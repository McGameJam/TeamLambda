using UnityEngine;
using System.Collections;

public abstract class UnitAction {
	public string nom;
	public bool active = true;
	public Sprite icon;

	public abstract void Action ();
}
