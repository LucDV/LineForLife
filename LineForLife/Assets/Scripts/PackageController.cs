using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PackageController : MonoBehaviour {

	public List<GraphData> dataPackage;
	public int packageID;
	public RectTransform Rect;
	public Action<int> SelectAction;

	public void InitPackage(List<GraphData> data, int id, Action<int> select){
		gameObject.SetActive (false);
		dataPackage = data;
		packageID = id;
		SelectAction = select;
		gameObject.GetComponent<Image> ().color = GameManager.Instance.listColor [packageID - 1];
		//Rect.anchoredPosition = pos;
	}

	public void LoadPackage(){

		Debug.Log ("Click package");
		if (SelectAction != null) {
			GameManager.Instance.CurrentPackage = packageID;
			SelectAction (packageID);
		}
	}
}
