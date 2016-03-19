using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CellGridStateTarget : CellGridState {

	private Unit _unit;
	List<Cell> _pathsInRange;
	List<Unit> _unitsInRange;
	Func<List<Cell>, List<Cell>> _predicate;
	Action<Cell> _cellAction;
	Action<Unit> _unitAction;

	public CellGridStateTarget(CellGrid cellGrid, Unit unit, Func<List<Cell>, List<Cell>> predicate, Action<Cell> cellAction, Action<Unit> unitAction) : base(cellGrid)
	{
		_unit = unit;
		_pathsInRange = new List<Cell>();
		_unitsInRange = new List<Unit>();
		_predicate = predicate;
		_cellAction = cellAction;
		_unitAction = unitAction;
	}

	public override void OnCellClicked(Cell cell)
	{
		if (_cellAction != null)
			_cellAction (cell);
		_cellGrid.CellGridState = new CellGridStateWaitingForInput (_cellGrid);
	}
	public override void OnUnitClicked(Unit unit)
	{
		if (_unitAction != null)
			_unitAction (unit);
		_cellGrid.CellGridState = new CellGridStateWaitingForInput (_cellGrid);
	}
	public override void OnCellDeselected(Cell cell)
	{
		base.OnCellDeselected(cell);

		foreach (var _cell in _pathsInRange)
		{
			_cell.MarkAsReachable();
		}
		foreach (var _cell in _cellGrid.Cells.Except(_pathsInRange))
		{
			_cell.UnMark();
		}
	}
	public override void OnCellSelected(Cell cell)
	{
		base.OnCellSelected(cell);
		if (!_pathsInRange.Contains(cell)) return;
		var path = _unit.FindPath(_cellGrid.Cells, cell);
		foreach (var _cell in path)
		{
			_cell.MarkAsPath();
		}
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		_unit.OnUnitSelected();
		var unitCell = _unit.Cell;

		_pathsInRange = _predicate(_cellGrid.Cells);
		var cellsNotInRange = _cellGrid.Cells.Except(_pathsInRange);

		foreach (var cell in cellsNotInRange)
		{
			cell.UnMark();
		}
		foreach (var cell in _pathsInRange)
		{
			cell.MarkAsReachable();
		}

		if (_unit.ActionPoints <= 0) return;

		foreach (var currentUnit in _cellGrid.Units)
		{
			if (currentUnit.PlayerNumber.Equals(_unit.PlayerNumber))
				continue;

			if (_unit.IsUnitAttackable(currentUnit,_unit.Cell))
			{
				currentUnit.SetState(new UnitStateMarkedAsReachableEnemy(currentUnit));
				_unitsInRange.Add(currentUnit);
			}
		}

		if (unitCell.GetNeighbours(_cellGrid.Cells).FindAll(c => c.MovementCost <= _unit.MovementPoints).Count == 0 
			&& _unitsInRange.Count == 0)
			_unit.SetState(new UnitStateMarkedAsFinished(_unit));
	}
	public override void OnStateExit()
	{
		_unit.OnUnitDeselected();
		foreach (Unit unit in _unitsInRange)
		{
			if (unit == null) continue;
			unit.SetState(new UnitStateNormal(unit));
		}
		foreach (var cell in _cellGrid.Cells)
		{
			cell.UnMark();
		}   
	}
}
