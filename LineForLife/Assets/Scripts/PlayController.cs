using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{

	// Use this for initialization

	public GraphController GraphPlay;

	private GraphData Data;


	public List<PathData> PathCorrect;
	public List<PathData> PathAnswer;
	public Stack ListNodeLatest = new Stack ();

	public void InitPlay (GraphData data)
	{
		Data = data;
		PathCorrect = Data.Clone ().listPath;
		PathAnswer = Data.Clone ().listPath;
		InitGraphPlay (Data);
		gameObject.SetActive (true);
	}

	public void InitGraphPlay (GraphData data)
	{
		GraphPlay.InitGraph (data, 700, 15, GameData.SIZE_VERTEX_PLAY, GameManager.Instance.listColor [Random.Range (0, 8)], HandleWhenSelectNode);
		GraphPlay.SetForPlay ();
	}

	private void HandleWhenSelectNode (Vector2 start, Vector2 end)
	{
		if (IsHaveRoad (start, end)) {
			GraphPlay.DrawLineWhenClick (end);
		}
	}

	private bool IsHaveRoad (Vector2 start, Vector2 end)
	{
		foreach (PathData path in PathAnswer) {
			if (start == new Vector2 (-1, -1)) {
				return true;
			}
			if ((path.startCor == start && path.endCor == end) || (path.startCor == end && path.endCor == start)) {
				ListNodeLatest.Push (path);
				if (path.repeat == 1) {
					PathAnswer.Remove (path);
				} else {
					path.repeat -= 1;
				}
				return true;
			}
		}
		return false;
	}

	public StatusGame CheckStatusGame (Vector2 currentCor)
	{
		if (PathAnswer.Count == 0) {
			return StatusGame.Win;
		}
		return StatusGame.Play;
	}

	public void Reset ()
	{
		InitPlay (Data);
	}

	public void Revert ()
	{
		PathData pathData = (PathData)ListNodeLatest.Pop ();
		if (pathData.startCor == GraphPlay.currentNode) {
			GraphPlay.currentNode = pathData.endCor;
		} else if (pathData.endCor == GraphPlay.currentNode) {
			GraphPlay.currentNode = pathData.startCor;
		}
		PathAnswer.Add(pathData);
		GraphPlay.BackOneLine ();
	}
}
