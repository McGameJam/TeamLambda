using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GrenadeLacrymoAction : UnitAction {

	public int targetMinRange;
	public int targetMaxRange;
	public CellGridStateTarget.eTargetType targetType;

	public override void Action()
	{
		GameObject cellGridObj = GameObject.Find ("CellGrid");
		CellGrid grid = cellGridObj.GetComponent<CellGrid> ();
		if (grid != null && grid.CellGridState is CellGridStateUnitSelected) {
			Unit unit = (grid.CellGridState as CellGridStateUnitSelected).selection;
			Func<List<Cell>, List<Cell>> cellFilter = (list) => {
				return list.FindAll(c => {
					var hasActionPoints = unit.ActionPoints > 0;
					var isDifferent = unit.Cell != c;
					var distance = unit.Cell.GetDistance (c);
					var isCloseEnough = distance <= this.targetMaxRange;
					var isFarEnough = distance >= this.targetMinRange;
					var u = grid.Units.Find(otherUnit => otherUnit.Cell == c);
					var isWayClearOfObstacles = u == null || !unit.IsObstacleInTheWay (u, unit.Cell);
					return hasActionPoints && isDifferent && isCloseEnough && isFarEnough && isWayClearOfObstacles;
				});
			};
			Action<Cell> cellAction = (Cell cell) => {
				Animator animator = unit.GetComponent<Animator>();
				animator.SetTrigger("Lacrymo");
				base.Action ();
				gameObject.GetComponent<AudioSource>().Play();
			};
			//grid.CellGridState = new CellGridStateUnitMovement(grid, (grid.CellGridState as CellGridStateUnitSelected).selection);
			grid.CellGridState = new CellGridStateTarget (grid, unit, this.targetType, cellFilter, cellAction);
		}
	}
}
