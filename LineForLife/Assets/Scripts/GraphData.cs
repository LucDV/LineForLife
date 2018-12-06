using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphData {
	public int PackageID;
	public int GraphID;
	public int numberLine;
	public List<PathData> listPath = new List<PathData>();

	public GraphData Clone(){
		GraphData data = new GraphData ();
		data.PackageID = PackageID;
		data.GraphID = GraphID;
		data.numberLine = numberLine;
		data.listPath.AddRange(listPath);
		return data;
	}
}
[System.Serializable]
public class PathData{
	public Vector2 startCor;
	public Vector2 endCor;
	public bool oneWay;
	public int repeat;
}