﻿using UnityEngine;
using System.Collections;
using System;

public class LancerMolotovAction : UnitAction {

	public LancerMolotovAction ()
	{
		this.nom = "Lancer Molotov";
	}

	public override void Action ()
	{
		GameObject cellGridObj = GameObject.Find ("CellGrid");
		CellGrid grid = cellGridObj.GetComponent<CellGrid> ();
		if (grid != null && grid.CellGridState is CellGridStateUnitSelected) {
			Unit unit = (grid.CellGridState as CellGridStateUnitSelected).selection;
			Predicate<Unit> predicate = (u) => {
				var hasActionPoints = unit.ActionPoints > 0;
				var isDifferent = unit != u;
				var isEnemy = unit.PlayerNumber != u.PlayerNumber;
				var isCloseEnough = unit.Cell.GetDistance (u.Cell) <= 2;
				var isWayClearOfObstacles = !unit.IsObstacleInTheWay (u, unit.Cell);
				return hasActionPoints && isDifferent && isEnemy && isCloseEnough && isWayClearOfObstacles;
			};
			Action<Unit> unitAction = (Unit target) => {
				unit.DealDamage(target);
				this.active = false;
			};
			grid.CellGridState = new CellGridStateTarget (grid, unit, predicate, unitAction);
		}
	}
}