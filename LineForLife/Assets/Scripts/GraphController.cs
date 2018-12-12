using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GraphController : MonoBehaviour
{
	public RectTransform blockTouch;
	public RectTransform Rect;
	public Transform placeHolder;
	public Transform showLine;
	public Transform Vertexs;

	public GameObject UIMesh;
	public GameObject Vertex;

	public GraphData Data;
	public Vector2 currentNode;

	public bool isNewLine;
	private float sizePixel;
	private float SizeGraph;
	private float SizeLine;
	private float SizeVertex;
	private Color color;
	private List<PathData> listPath = new List<PathData> ();
	private UIMeshLine currentMeshLine;
	[SerializeField]
	private List<Vector2> listVertex = new List<Vector2> ();
	private Action<Vector2, Vector2> ClickNode;

	void Start ()
	{
//		GraphData graph = JsonUtility.FromJson<GraphData> ("{\"Package\":1,\"Graph\":0,\"numberLine\":1,\"listPath\":[{\"startCor\":{\"x\":5.0,\"y\":13.0},\"endCor\":{\"x\":22.0,\"y\":8.0},\"oneWay\":false,\"repeat\":1},{\"startCor\":{\"x\":22.0,\"y\":8.0},\"endCor\":{\"x\":11.0,\"y\":19.0},\"oneWay\":false,\"repeat\":1},{\"startCor\":{\"x\":11.0,\"y\":19.0},\"endCor\":{\"x\":11.0,\"y\":7.0},\"oneWay\":false,\"repeat\":1},{\"startCor\":{\"x\":11.0,\"y\":7.0},\"endCor\":{\"x\":22.0,\"y\":18.0},\"oneWay\":false,\"repeat\":1},{\"startCor\":{\"x\":22.0,\"y\":18.0},\"endCor\":{\"x\":5.0,\"y\":13.0},\"oneWay\":false,\"repeat\":1}]}");
//		InitGraph (graph, 700, 15);
//		isNewLine = true;
	}

	public void InitGraph (GraphData data, float sizeGraph, float sizeLine, float sizeVertex, Color color, Action<Vector2, Vector2> clickNode = null)
	{
		ResetGraph ();
		Data = data;
		SizeGraph = sizeGraph;
		SizeLine = sizeLine;
		SizeVertex = sizeVertex;
		this.color = color;
		Rect.sizeDelta = new Vector2 (sizeGraph, sizeGraph);
		ClickNode = clickNode;
		currentNode = new Vector2 (-1, -1);
		sizePixel = sizeGraph / GameData.SIZE_GRAPH;
		CreateGraph ();
		gameObject.SetActive (true);
	}

	public void SetForPlay ()
	{

		currentMeshLine.color = GameManager.Instance.listColor [9];
		currentMeshLine.Redraw ();
		isNewLine = true;
		blockTouch.gameObject.SetActive (false);
	}

	private void CreateGraph ()
	{
		listPath.AddRange (Data.listPath);
		Vector2 prePos = new Vector2 (-1, -1);
		foreach (PathData path in listPath) {
			if (path.startCor != prePos) {
				GameObject gameObj = GameManager.Instance.SpawnObject (UIMesh, placeHolder, Vector2.zero);
				currentMeshLine = gameObj.GetComponent<UIMeshLine> ();
				LinePoint point = new LinePoint ();
				point.point = GetPosByCor (path.startCor);
				currentMeshLine.points.Add (point);
			}

			if (!listVertex.Contains (path.startCor)) {
				listVertex.Add (path.startCor);
			}
			prePos = path.endCor;
			LinePoint pointLine = new LinePoint ();
			pointLine.point = GetPosByCor (path.endCor);
			currentMeshLine.points.Add (pointLine);
		}
		currentMeshLine.width = SizeLine;
		currentMeshLine.color = color;
		currentMeshLine.Redraw ();
		AddVertex ();
	}

	private void AddVertex ()
	{
		foreach (Vector2 cor in listVertex) {
			GameObject gameobj = GameManager.Instance.SpawnObject (Vertex, Vertexs, GetPosByCor (cor));
			gameobj.GetComponent<VertexController> ().InitVertex (SizeVertex, cor, color, (Vector2 cordinate) => {
				if (ClickNode != null) {
					ClickNode (currentNode, cordinate);
				}
			});
		}
	}

	private Vector2 GetPosByCor (Vector2 cor)
	{
		Vector2 pos = new Vector2 ();
		pos.x = -SizeGraph / 2 + sizePixel / 2 + cor.y * sizePixel;
		pos.y = SizeGraph / 2 - sizePixel / 2 - cor.x * sizePixel;
		return pos;
	}



	public void DrawLineWhenClick (Vector2 cor)
	{
		if (isNewLine) {
			GameObject gameObj = GameManager.Instance.SpawnObject (UIMesh, showLine, Vector2.zero);
			currentMeshLine = gameObj.GetComponent<UIMeshLine> ();
			currentMeshLine.width = SizeLine;
			isNewLine = false;
		}
		if (currentMeshLine.points.Count == 0) {
			LinePoint linePoint = new LinePoint ();
			linePoint.point = GetPosByCor (cor);
			currentMeshLine.points.Add (linePoint);
			currentNode = cor;
		} else {
			blockTouch.gameObject.SetActive (true);
			currentMeshLine.color = color;

			LinePoint linePoint = new LinePoint ();
			linePoint.point = currentMeshLine.points [currentMeshLine.points.Count - 1].point;
			currentMeshLine.points.Add (linePoint);
			LeanTween.value (currentMeshLine.gameObject, currentMeshLine.points [currentMeshLine.points.Count - 1].point, GetPosByCor (cor), 1).setOnUpdate ((Vector2 value) => {
				currentMeshLine.points [currentMeshLine.points.Count - 1].point = value;
				currentMeshLine.Redraw ();
			}).setOnComplete (() => {
				blockTouch.gameObject.SetActive (false);
				currentNode = cor;
				Debug.Log (GameManager.Instance.Play.CheckStatusGame (currentNode));
			});
		}
	}

	public void BackOneLine ()
	{
		blockTouch.gameObject.SetActive (true);
		LeanTween.value (currentMeshLine.gameObject, currentMeshLine.points [currentMeshLine.points.Count - 1].point, currentMeshLine.points [currentMeshLine.points.Count - 2].point, 1).setOnUpdate ((Vector2 value) => {
			currentMeshLine.points [currentMeshLine.points.Count - 1].point = value;
			currentMeshLine.Redraw ();
		}).setOnComplete (() => {
			blockTouch.gameObject.SetActive (false);
			currentMeshLine.points.RemoveAt (currentMeshLine.points.Count - 1);
		});
	}

	private void ResetGraph ()
	{
		listPath.Clear ();
		listVertex.Clear ();
		ClearChild (placeHolder);
		ClearChild (showLine);
		ClearChild (Vertexs);
	}

	private void ClearChild (Transform tranf)
	{
		Transform[] listChild = tranf.GetComponentsInChildren<Transform> ();
		foreach (Transform child in listChild) {
			if (child != tranf) {
				child.Recycle ();
			}
		}
	}

}
