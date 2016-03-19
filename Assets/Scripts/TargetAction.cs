using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TargetAction : UnitAction {

	private Func<List<Cell>, List<Cell>> _predicate;
	private Action<Cell> _cellAction;
	private Action<Unit> _unitAction;

	public TargetAction(Func<List<Cell>, List<Cell>> predicate, Action<Cell> cellAction, Action<Unit> unitAction)
	{
		this.nom = "Target";
		_predicate = predicate;
		_cellAction = cellAction;
		_unitAction = unitAction;
	}
	
	public override void Action()
	{
		GameObject cellGridObj = GameObject.Find ("CellGrid");
		CellGrid grid = cellGridObj.GetComponent<CellGrid> ();
		if (grid != null) {
			Unit unit = (grid.CellGridState as CellGridStateUnitSelected).selection;
			grid.CellGridState = new CellGridStateTarget(grid, unit, _predicate, _cellAction, _unitAction);
		}
	}
}
