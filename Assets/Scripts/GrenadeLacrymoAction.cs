using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GrenadeLacrymoAction : UnitAction {

	public GrenadeLacrymoAction()
	{
		this.nom = "Grenade Lacrymogène";
	}

	public override void Action()
	{
		GameObject cellGridObj = GameObject.Find ("CellGrid");
		CellGrid grid = cellGridObj.GetComponent<CellGrid> ();
		if (grid != null && grid.CellGridState is CellGridStateUnitSelected) {
			Unit unit = (grid.CellGridState as CellGridStateUnitSelected).selection;
			Func<List<Cell>, List<Cell>> filter = (list) => {
				return unit.GetAvailableDestinations(list);
			};
			Action<Cell> cellAction = (Cell cell) => {
				var path = unit.FindPath(grid.Cells, cell);
				unit.Move(cell,path);
			};
			//grid.CellGridState = new CellGridStateUnitMovement(grid, (grid.CellGridState as CellGridStateUnitSelected).selection);
			grid.CellGridState = new CellGridStateTarget (grid, unit, filter, cellAction);
		}
	}
}
