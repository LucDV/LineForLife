using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphData {
	public int PackageID;
	public int GraphID;
	public int numberLine;
	public List<PathData> listPath;
}
[System.Serializable]
public class PathData{
	public Vector2 startCor;
	public Vector2 endCor;
	public bool oneWay;
	public int repeat;
}