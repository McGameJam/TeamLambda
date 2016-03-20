using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CellGridStateGameOver : CellGridState
{
    public CellGridStateGameOver(CellGrid cellGrid) : base(cellGrid)
    {
    }

    public override void OnStateEnter()
    {
		GameObject.Find ("CanvasVictoire").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("Main Camera").SetActive (false);
		GameObject.Find ("GUICamera").SetActive (false);
		var gagnant = _cellGrid.Units.Select (u => u.PlayerNumber).Distinct ().ToList ();
		if (gagnant.Count == 1 && gagnant.ElementAt (0) == 0) {
			GameObject.Find ("VictoirePoliciers").SetActive (false);
		} else {
			GameObject.Find ("VictoireManifestants").SetActive (false);
		}
    }
}