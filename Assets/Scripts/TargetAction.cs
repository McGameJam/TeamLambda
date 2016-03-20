using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TargetAction : UnitAction {

	private Func<List<Cell>, List<Cell>> _cellFilter;
	private Predicate<Unit> _unitPredicate;
	private Action<Cell> _cellAction;
	private Action<Unit> _unitAction;

	public TargetAction(Func<List<Cell>, List<Cell>> cellFilter, Action<Cell> cellAction)
	{
		this.nom = "Target";
		_cellFilter = cellFilter;
		_cellAction = cellAction;
	}

	public TargetAction(Predicate<Unit> unitPredicate, Action<Unit> unitAction)
	{
		this.nom = "Target";
		_unitPredicate = unitPredicate;
		_unitAction = unitAction;
	}
	
	public override void Action()
	{
		GameObject cellGridObj = GameObject.Find ("CellGrid");
		CellGrid grid = cellGridObj.GetComponent<CellGrid> ();
		if (grid != null && grid.CellGridState is CellGridStateUnitSelected) {
			Unit unit = (grid.CellGridState as CellGridStateUnitSelected).selection;
			grid.CellGridState = new CellGridStateTarget(grid, unit, _cellFilter, _unitPredicate, _cellAction, _unitAction);
		}
	}
}
