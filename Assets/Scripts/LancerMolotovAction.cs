using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
			Func<List<Cell>, List<Cell>> cellFilter = (list) => {
				return list.FindAll(c => {
					var hasActionPoints = unit.ActionPoints > 0;
					var isDifferent = unit.Cell != c;
					var isCloseEnough = unit.Cell.GetDistance (c) <= 4;
					var u = grid.Units.Find(otherUnit => otherUnit.Cell == c);
					var isWayClearOfObstacles = u == null || !unit.IsObstacleInTheWay (u, unit.Cell);
					return hasActionPoints && isDifferent && isCloseEnough && isWayClearOfObstacles;
				});
			};
			Action<Cell> cellAction = (Cell target) => {
				this.active = false;
			};
			grid.CellGridState = new CellGridStateTarget (grid, unit, CellGridStateTarget.eTargetType.Quatre, cellFilter, cellAction);
		}
	}
}
