using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListPackageController : MonoBehaviour
{

	public List<RectTransform> listContent;

	public void InitListPack ()
	{
		StartCoroutine (ShowListPack ());
	}

	IEnumerator ShowListPack ()
	{
		foreach (RectTransform rect in listContent) {
			yield return new WaitForSeconds (0.1f);
			LeanTween.value (0.5f, 1, 0.3f).setOnStart (() => {
				rect.gameObject.SetActive (true);
			}).setOnUpdate ((float value) => {
				rect.localScale = new Vector3 (value, value, 1); 
				Color color = rect.GetComponent<Image> ().color;
				color.a = value;
				rect.GetComponent<Image> ().color = color;
			});
		}
	}

	IEnumerator ShowListGraph(){
		foreach (RectTransform rect in listContent) {
			yield return new WaitForSeconds (0.1f);
			LeanTween.value (0.5f, 1, 0.3f).setOnStart (() => {
				rect.gameObject.SetActive (true);
			}).setOnUpdate ((float value) => {
				rect.localScale = new Vector3 (value, value, 1); 
				Color color = rect.GetComponent<Image> ().color;
				color.a = value;
				rect.GetComponent<Image> ().color = color;
			});
		}
	}
}
