using UnityEngine;
using System.Collections;

public abstract class UnitAction {
	public string nom;
	public bool active = true;

	public abstract void Action ();
}
