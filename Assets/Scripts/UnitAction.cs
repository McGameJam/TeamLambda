﻿using UnityEngine;
using System.Collections;

public abstract class UnitAction : MonoBehaviour {
	public string nom;
	public bool active = true;
<<<<<<< 4e1f7cdbee8291c2b506917af46cf5d73673fc34
	public Sprite icon;
=======
	public bool oncePerUnit = false;
>>>>>>> Élimination de classes non nécessaires et création de Prefabs pour configurer le jeu.

	public virtual void Action ()
	{
		if (this.oncePerUnit) {
			this.active = false;
		}
	}
}
