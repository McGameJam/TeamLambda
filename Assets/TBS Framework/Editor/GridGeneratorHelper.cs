﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ICellGridGenerator), true)]
public class GridGeneratorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ICellGridGenerator gridGenerator = (ICellGridGenerator)target;
  
        if (GUILayout.Button("Generate Grid"))
        {
            gridGenerator.GenerateGrid();
        }
        if (GUILayout.Button("Clear Grid"))
        {
            var children = new List<GameObject>();
            foreach(Transform cell in gridGenerator.CellsParent)
            {
                children.Add(cell.gameObject);
            }

            children.ForEach(c => DestroyImmediate(c));
        }
    }
}
