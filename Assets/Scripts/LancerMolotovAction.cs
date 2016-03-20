using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LancerMolotovAction : UnitAction {

	public int targetRange;
	public CellGridStateTarget.eTargetType targetType;

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
					var isCloseEnough = unit.Cell.GetDistance (c) <= this.targetRange;
					var u = grid.Units.Find(otherUnit => otherUnit.Cell == c);
					var isWayClearOfObstacles = u == null || !unit.IsObstacleInTheWay (u, unit.Cell);
					return hasActionPoints && isDifferent && isCloseEnough && isWayClearOfObstacles;
				});
			};
			Action<Cell> cellAction = (Cell target) => {
				base.Action ();
			};
			grid.CellGridState = new CellGridStateTarget (grid, unit, this.targetType, cellFilter, cellAction);
		}
	}
}
