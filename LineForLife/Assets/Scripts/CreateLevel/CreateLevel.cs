using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour
{

	public RectTransform rect;
	public RectTransform Matrix;
	public RectTransform ListLine;
	public GameObject pixel;
	public UIMeshLine meshLine;
	public GameObject line;

	public Vector2 prePoint;
	public GraphData graphData = new GraphData ();
	public bool newLine;

	public InputField package;
	public InputField numberLine;
	public InputField repeate;
	public Toggle oneWay;
	// Use this for initialization
	void Start ()
	{
//		graphData.Graph = 1;
//		graphData.Package = 1;
//		graphData.numberLine = 1;
		rect = GetComponent<RectTransform> ();
		newLine = true;
		InitMatrix ();
	}

	public void InitMatrix ()
	{
		for (int i = 0; i < GameData.SIZE_GRAPH; i++) {
			for (int j = 0; j < GameData.SIZE_GRAPH; j++) {
				GameObject gameobj = Instantiate (pixel, Matrix.transform);
				gameobj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-350 + 12.5f + j * 25, 350 - 12.5f - i * 25);
				gameobj.GetComponent<PixelController> ().InitPixel (i, j, CreateLine);
			}
		}
	}

	public void CreateLine (Vector2 pos, int x, int y)
	{
		if (newLine) {
			GameObject gameobj = Instantiate (line, ListLine.transform);
			meshLine = gameobj.GetComponent<UIMeshLine> ();
			newLine = false;
		} else {
			PathData path = new PathData ();
			path.startCor = prePoint;
			path.endCor = new Vector2 (x, y);
			path.oneWay = oneWay.isOn;
			oneWay.isOn = false;
			path.repeat = int.Parse (repeate.text);
			repeate.text = "1";
			graphData.listPath.Add (path);
		}
		prePoint = new Vector2 (x, y);
		LinePoint linePoint = new LinePoint ();
		linePoint.point = pos;
		meshLine.points.Add (linePoint);
		meshLine.Redraw ();
	}

	public void Click ()
	{
		graphData.Package = int.Parse(package.text);
		graphData.numberLine = int.Parse(numberLine.text);
		JsonHelper.WriteJson (JsonUtility.ToJson (graphData));
		graphData.listPath.Clear ();
		meshLine.points.Clear ();
		meshLine.Redraw ();
	}

	public void Back(){
		graphData.listPath.RemoveAt (graphData.listPath.Count - 1);
		meshLine.points.RemoveAt (meshLine.points.Count - 1);
		prePoint = graphData.listPath [graphData.listPath.Count - 1].endCor;
		meshLine.Redraw ();
	}
}
