using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LancerBriqueAction : UnitAction {

	public LancerBriqueAction()
	{
		this.nom = "Lancer une brique";
	}

	public override void Action()
	{
		GameObject cellGridObj = GameObject.Find ("CellGrid");
		CellGrid grid = cellGridObj.GetComponent<CellGrid> ();
		if (grid != null) {
			Unit unit = (grid.CellGridState as CellGridStateUnitSelected).selection;
			Func<List<Cell>, List<Cell>> predicate = (list) => {
				List<Cell> cellules = new List<Cell>();
				var unitsInRange = grid.Units.FindAll(
					u => {
						var isDifferent = unit != u;
						var isEnemy = unit.PlayerNumber != u.PlayerNumber;
						var isCloseEnough = unit.Cell.GetDistance(u.Cell) <= 2;
						var isInOriginalList = list.Contains(u.Cell);
						var isWayClearOfObstacles = !unit.IsObstacleInTheWay(u, unit.Cell);
						return isDifferent && isEnemy && isCloseEnough && isInOriginalList && isWayClearOfObstacles;
					}
				);

				unitsInRange.ForEach(u => cellules.Add(u.Cell));
				return cellules;
			};
			Action<Cell> cellAction = null;
			Action<Unit> unitAction = (Unit target) => {
				Debug.Log("Action");
			};
			//grid.CellGridState = new CellGridStateUnitMovement(grid, (grid.CellGridState as CellGridStateUnitSelected).selection);
			grid.CellGridState = new CellGridStateTarget (grid, unit, predicate, cellAction, unitAction);
		}
	}
}
