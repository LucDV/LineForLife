using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public List<List<GraphData>> listPackageData = new List<List<GraphData>>();
	// Use this for initialization
	void Start ()
	{
		int currentPackage = 0;
		int currentGraph = 0;

		List<GraphData> listGraph = new List<GraphData> ();
		List<string> listdata = JsonHelper.ReadJson (GameData.NAME_FILE_GRAPH);

		foreach (string line in listdata) {
			GraphData data = JsonUtility.FromJson<GraphData> (line);
			if (data.Package != currentPackage) {
				if (listGraph.Count > 0) {
					listPackageData.Add (listGraph);
					listGraph.Clear ();
				}
				currentPackage = data.Package;
				currentGraph = 1;
				data.Graph = currentGraph;
				listGraph.Add (data);
			} else {
				currentGraph += 1;
				data.Graph = currentGraph;
				listGraph.Add (data);
			}
		}
		listPackageData.Add (listGraph);
		Debug.Log (listPackageData.Count);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}



	public GameObject SpawnObject (GameObject gameObj, Transform parent, Vector2 pos)
	{
		GameObject obj = gameObj.Spawn (parent);
		obj.transform.localScale = Vector3.one;
		obj.GetComponent<RectTransform> ().anchoredPosition = pos;
		return obj;
	}

}
