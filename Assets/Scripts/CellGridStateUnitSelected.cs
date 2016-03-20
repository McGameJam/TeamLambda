using UnityEngine;
using System.Collections;
using System;

public class CellGridStateUnitSelected : CellGridState {

	public Unit selection;

	public CellGridStateUnitSelected(CellGrid cell, Unit unit) : base(cell)
	{
		selection = unit;
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter ();

		selection.OnUnitSelected ();
	}
	public override void OnUnitClicked(Unit unit)
	{
		if (unit.PlayerNumber.Equals (selection.PlayerNumber)) {
			_cellGrid.CellGridState = new CellGridStateUnitSelected (_cellGrid, unit);
		} else {
			_cellGrid.CellGridState = new CellGridStateWaitingForInput (_cellGrid);
		}

	}
	public override void OnStateExit()
	{
		selection.OnUnitDeselected();
	}
	public override void OnCellClicked(Cell sender)
	{
		base.OnCellSelected(sender);
		_cellGrid.CellGridState = new CellGridStateWaitingForInput (_cellGrid);
	}
}
