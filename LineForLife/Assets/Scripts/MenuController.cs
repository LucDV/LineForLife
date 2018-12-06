using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public Transform btnBack;
	public Action ClickBack;


	public ListPackageController Packages;
	public GameObject packageObj;
	public GameObject blockObj;
	private List<List<GraphData>> listPackageData = new List<List<GraphData>> ();

	public void InitMenu (List<List<GraphData>> data)
	{
		listPackageData = data;
		CreatePackages ();
	}

	public void ClickBtnBack ()
	{
		if (ClickBack != null) {
			ClickBack ();
		}
	}


	private void CreatePackages ()
	{

		btnBack.gameObject.SetActive (false);
		ClearDataContent ();
		ScrollRect scroll = Packages.GetComponent<ScrollRect> ();
		int numberPackage = listPackageData.Count;

		float sizeContent = numberPackage * 100 + (numberPackage - 1) * 30;
		scroll.content.sizeDelta = new Vector2 (scroll.content.sizeDelta.x, sizeContent);
		for (int i = 0; i < numberPackage; i++) {
			GameObject gameObj = GameManager.Instance.SpawnObject (packageObj, scroll.content.transform, new Vector2 (60, sizeContent / 2 - 50 - i * 130));
			gameObj.GetComponent<PackageController> ().InitPackage (listPackageData [i], i + 1, ShowListGraph);
			Packages.listContent.Add (gameObj.GetComponent<RectTransform> ());
		}
		Packages.InitListPack ();
	}

	public void ShowListGraph (int idData)
	{
		List<GraphData> data = listPackageData [idData - 1];
		ClearDataContent ();
		btnBack.gameObject.SetActive (true);
		ClickBack = CreatePackages;
		ScrollRect scroll = Packages.GetComponent<ScrollRect> ();
		int numberData = data.Count;
		int numberRow = numberData / 2 + numberData % 2;

		float sizeContent = numberRow * 350 + (numberRow - 1) * 30;
		scroll.content.sizeDelta = new Vector2 (scroll.content.sizeDelta.x, sizeContent);
		for (int i = 0; i < numberData; i++) {
			GameObject gameObj = GameManager.Instance.SpawnObject (blockObj, scroll.content.transform, new Vector2 (-360 + 180 + (i % 2) * 360, sizeContent / 2 - 175 - (i / 2) * 380));
			gameObj.GetComponent<BlockGraphController> ().InitBlock (data [i], 280, 7, selectBlock: GameManager.Instance.GotoPlay);
			Packages.listContent.Add (gameObj.GetComponent<RectTransform> ());
		}
	}

	private void ClearDataContent ()
	{
		foreach (RectTransform rect in Packages.listContent) {
			rect.gameObject.Recycle ();
		}
		Packages.listContent.Clear ();
	}
}
