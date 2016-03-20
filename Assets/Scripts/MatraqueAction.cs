using UnityEngine;
using System.Collections;
using System;

public class MatraqueAction : UnitAction {

	public int targetMinRange = 0;
	public int targetMaxRange = 1;
	public CellGridStateTarget.eTargetType targetType = CellGridStateTarget.eTargetType.Point;

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
				var distance = unit.Cell.GetDistance (u.Cell);
				var isFarEnough = distance >= this.targetMinRange; 
				var isCloseEnough = distance <= this.targetMaxRange;
				var isWayClearOfObstacles = !unit.IsObstacleInTheWay (u, unit.Cell);
				return hasActionPoints && isDifferent && isEnemy && isCloseEnough && isFarEnough && isWayClearOfObstacles;
			};
			Action<Unit> unitAction = (Unit target) => {
				Animator animator = unit.GetComponent<Animator>();
				animator.SetTrigger("Matraque");
				unit.DealDamage(target);
				base.Action ();
			};
			grid.CellGridState = new CellGridStateTarget (grid, unit, this.targetType, predicate, unitAction);
		}
	}
}
