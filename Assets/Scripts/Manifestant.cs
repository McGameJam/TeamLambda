using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manifestant : UnitAvecActions {

	public void Start()
	{
		this.actions.Add (new MovementAction ());
		this.actions.Add (new GrenadeLacrymoAction ());
		this.actions.Add (new LancerBriqueAction ());
	}
}
