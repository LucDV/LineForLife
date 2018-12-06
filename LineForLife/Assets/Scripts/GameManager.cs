using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusGame
{
	Menu,
	Play,
	Win,
	Lose
}

public class GameManager : Singleton<GameManager>
{
	public int CurrentPackage;
	public int CurrentGraphID;

	public MenuController Menu;
	public PlayController Play;
	public List<Color> listColor;
	public List<List<GraphData>> listPackageData = new List<List<GraphData>> ();

	// Use this for initialization
	void Start ()
	{
		LoadDataGame ();
		Menu.InitMenu (listPackageData);
	}

	public GameObject SpawnObject (GameObject gameObj, Transform parent, Vector2 pos)
	{
		GameObject obj = gameObj.Spawn (parent);
		obj.transform.localScale = Vector3.one;
		obj.GetComponent<RectTransform> ().anchoredPosition = pos;
		return obj;
	}

	private void LoadDataGame ()
	{
		int currentPackage = 0;
		int currentGraph = 0;

		List<GraphData> listGraph = new List<GraphData> ();
		List<string> listdata = JsonHelper.ReadJson (GameData.NAME_FILE_GRAPH);

		foreach (string line in listdata) {
			GraphData data = JsonUtility.FromJson<GraphData> (line);
			if (data.PackageID != currentPackage) {
				if (listGraph.Count > 0) {
					listPackageData.Add (listGraph);
					listGraph = new List<GraphData> ();
				}
				currentPackage = data.PackageID;
				currentGraph = 1;
				data.GraphID = currentGraph;
				listGraph.Add (data);
			} else {
				currentGraph += 1;
				data.GraphID = currentGraph;
				listGraph.Add (data);
			}
		}
		listPackageData.Add (listGraph);
	}

	public void GotoPlay (GraphData data)
	{
		Menu.Packages.gameObject.SetActive (false);
		Play.InitPlay (data);
		Menu.ClickBack = () => {
			Play.gameObject.SetActive (false);
			Menu.ShowListGraph (CurrentPackage);
			Menu.Packages.gameObject.SetActive (true);
		};
	}
}