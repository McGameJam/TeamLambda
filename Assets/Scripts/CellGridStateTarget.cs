using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CellGridStateTarget : CellGridState {

	public enum eTargetType
	{
		Path,
		Point,
		Quatre,
		Sept
	};

	private Unit _unit;
	List<Cell> _pathsInRange;
	List<Unit> _unitsInRange;
	Func<List<Cell>, List<Cell>> _cellFilter;
	Predicate<Unit> _unitPredicate;
	Action<Cell> _cellAction;
	Action<Unit> _unitAction;
	eTargetType _type;

	public CellGridStateTarget(CellGrid cellGrid, Unit unit, eTargetType type, Func<List<Cell>, List<Cell>> cellFilter, Action<Cell> cellAction)
		: base(cellGrid)
	{
		_unit = unit;
		_type = type;
		_pathsInRange = new List<Cell>();
		_unitsInRange = new List<Unit>();
		_cellFilter = cellFilter;
		_cellAction = cellAction;
	}

	public CellGridStateTarget(CellGrid cellGrid, Unit unit, eTargetType type, Predicate<Unit> unitPredicate, Action<Unit> unitAction)
		: base(cellGrid)
	{
		_unit = unit;
		_type = type;
		_pathsInRange = new List<Cell>();
		_unitsInRange = new List<Unit>();
		_unitPredicate = unitPredicate;
		_unitAction = unitAction;
	}

	public CellGridStateTarget(CellGrid cellGrid, Unit unit, eTargetType type, Func<List<Cell>, List<Cell>> cellFilter, Predicate<Unit> unitPredicate, Action<Cell> cellAction, Action<Unit> unitAction)
		: base(cellGrid)
	{
		_unit = unit;
		_type = type;
		_pathsInRange = new List<Cell>();
		_unitsInRange = new List<Unit>();
		_cellFilter = cellFilter;
		_unitPredicate = unitPredicate;
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

	private void AjouterAutresCells(ref float f, ref Vector3 v3, ref List<Cell> markedCells)
	{
		f += 1;
		var nextCell = v3;
		var cellThree = _cellGrid.Cells.Find (c => (c as Hexagon).CubeCoord == nextCell);
		if (cellThree != null)
			markedCells.Add (cellThree);
		f -= 2;
		nextCell = v3;
		var cellFour = _cellGrid.Cells.Find (c => (c as Hexagon).CubeCoord == nextCell);
		if (cellFour != null)
			markedCells.Add (cellFour);
	}

	private void AddIfExists(Vector3 coord, List<Cell> markedCells)
	{
		var cell = _cellGrid.Cells.Find (c => (c as Hexagon).CubeCoord == coord);
		if (cell != null)
			markedCells.Add (cell);
	}

	private void AddIfExists(Vector2 coord, List<Cell> markedCells)
	{
		var cell = _cellGrid.Cells.Find (c => c.OffsetCoord == coord);
		if (cell != null)
			markedCells.Add (cell);
	}

	public override void OnCellSelected(Cell cell)
	{
		base.OnCellSelected(cell);
		if (!_pathsInRange.Contains(cell)) return;
		List<Cell> markedCells;
		if (_type == eTargetType.Path) {
			markedCells = _unit.FindPath (_cellGrid.Cells, cell);
		} else {
			markedCells = new List<Cell> ();
			markedCells.Add (cell);
			if (_type == eTargetType.Quatre) {
				var cellCubeCoord = (cell as Hexagon).CubeCoord;
				var unitCubeCoord = (_unit.Cell as Hexagon).CubeCoord;
				var direction = cellCubeCoord - unitCubeCoord;
				direction.x = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0;
				direction.y = direction.y > 0 ? 1 : direction.y < 0 ? -1 : 0;
				direction.z = direction.z > 0 ? 1 : direction.z < 0 ? -1 : 0;
				var nextCell = cellCubeCoord;
				if (direction.x == 0 || direction.y == 0 || direction.z == 0) {
					nextCell.x += direction.x;
					nextCell.y += direction.y;
					nextCell.z += direction.z;
					AddIfExists (nextCell, markedCells);
					nextCell = cellCubeCoord;
					if (direction.x == 1 && direction.y == -1) {
						nextCell.y -= 1;
						nextCell.z += 1;
						AddIfExists (nextCell, markedCells);
						nextCell = cellCubeCoord;
						nextCell.x += 1;
						nextCell.z -= 1;
						AddIfExists (nextCell, markedCells);
					} else if (direction.x == 1 && direction.z == -1) {
						nextCell.y += 1;
						nextCell.z -= 1;
						AddIfExists (nextCell, markedCells);
						nextCell = cellCubeCoord;
						nextCell.x += 1;
						nextCell.y -= 1;
						AddIfExists (nextCell, markedCells);
					} else if (direction.y == 1 && direction.z == -1) {
						nextCell.x += 1;
						nextCell.z -= 1;
						AddIfExists (nextCell, markedCells);
						nextCell = cellCubeCoord;
						nextCell.x -= 1;
						nextCell.y += 1;
						AddIfExists (nextCell, markedCells);
					} else if (direction.x == -1 && direction.y == 1) {
						nextCell.x -= 1;
						nextCell.z += 1;
						AddIfExists (nextCell, markedCells);
						nextCell = cellCubeCoord;
						nextCell.y += 1;
						nextCell.z -= 1;
						AddIfExists (nextCell, markedCells);
					} else if (direction.x == -1 && direction.z == 1) {
						nextCell.y -= 1;
						nextCell.z += 1;
						AddIfExists (nextCell, markedCells);
						nextCell = cellCubeCoord;
						nextCell.x -= 1;
						nextCell.y += 1;
						AddIfExists (nextCell, markedCells);
					} else if (direction.y == -1 && direction.z == 1) {
						nextCell.x += 1;
						nextCell.y -= 1;
						AddIfExists (nextCell, markedCells);
						nextCell = cellCubeCoord;
						nextCell.x -= 1;
						nextCell.z += 1;
						AddIfExists (nextCell, markedCells);
					}
				} else {
					var diff = cellCubeCoord - unitCubeCoord;
					var absX = Math.Abs (diff.x);
					var absY = Math.Abs (diff.y);
					var absZ = Math.Abs (diff.z);
					if (absX > absY && absX > absZ) {
						if (diff.x > 0) {
							nextCell.x += 1;
							nextCell.y -= 1;
							AddIfExists (nextCell, markedCells);
							nextCell.y += 1;
							nextCell.z -= 1;
							AddIfExists (nextCell, markedCells);
							nextCell.y -= 1;
							nextCell.x += 1;
							AddIfExists (nextCell, markedCells);
						} else {
							nextCell.x -= 1;
							nextCell.y += 1;
							AddIfExists (nextCell, markedCells);
							nextCell.y -= 1;
							nextCell.z += 1;
							AddIfExists (nextCell, markedCells);
							nextCell.y += 1;
							nextCell.x -= 1;
							AddIfExists (nextCell, markedCells);
						}
					} else if (absY > absX && absY > absZ) {
						if (diff.y > 0) {
							nextCell.x -= 1;
							nextCell.y += 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x += 1;
							nextCell.z -= 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x -= 1;
							nextCell.y += 1;
							AddIfExists (nextCell, markedCells);
						} else {
							nextCell.x += 1;
							nextCell.y -= 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x -= 1;
							nextCell.z += 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x += 1;
							nextCell.y -= 1;
							AddIfExists (nextCell, markedCells);
						}
					} else {
						if (diff.z > 0) {
							nextCell.x -= 1;
							nextCell.z += 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x += 1;
							nextCell.y -= 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x -= 1;
							nextCell.z += 1;
							AddIfExists (nextCell, markedCells);
						} else {
							nextCell.x += 1;
							nextCell.z -= 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x -= 1;
							nextCell.y += 1;
							AddIfExists (nextCell, markedCells);
							nextCell.x += 1;
							nextCell.z -= 1;
							AddIfExists (nextCell, markedCells);
						}
					}
				}
			} else if (_type == eTargetType.Sept) {
				foreach (var otherCell in cell.GetNeighbours(_cellGrid.Cells)) {
					markedCells.Add (otherCell);
				}
			}
		}
		foreach (var _cell in markedCells)
		{
			_cell.MarkAsPath();
		}
	}

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		if (_cellFilter != null) {
			_pathsInRange = _cellFilter(_cellGrid.Cells);

			var cellsNotInRange = _cellGrid.Cells.Except(_pathsInRange);

			foreach (var cell in cellsNotInRange)
			{
				cell.UnMark();
			}
			foreach (var cell in _pathsInRange)
			{
				cell.MarkAsReachable();
			}
		}
		if (_unitPredicate != null) {
			_unitsInRange = _cellGrid.Units.FindAll (_unitPredicate);

			foreach (var currentUnit in _unitsInRange)
			{
				currentUnit.SetState(new UnitStateMarkedAsReachableEnemy(currentUnit));
			}
		}
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
