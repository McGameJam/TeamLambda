using UnityEngine;
using System.Collections;

public class Policier : UnitAvecActions {

	public void Start()
	{
		this.actions.Add (new MovementAction ());
		this.actions.Add (new GrenadeLacrymoAction ());
	}
}
