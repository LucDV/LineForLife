using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{

	public UIMeshLine drawLine;
	public RectTransform rect;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ClickVertex ()
	{
		if (drawLine.points.Count == 0) {
			LinePoint linePoint = new LinePoint ();
			linePoint.point = rect.anchoredPosition;
			drawLine.points.Add (linePoint);
			linePoint = new LinePoint ();
			linePoint.point = rect.anchoredPosition;
			drawLine.points.Add (linePoint);
		} else {
			LeanTween.value (drawLine.gameObject, drawLine.points [drawLine.points.Count - 1].point, rect.anchoredPosition, 1).setOnUpdate ((Vector2 value) => {
				drawLine.points [drawLine.points.Count - 1].point = value;
				drawLine.Redraw ();
			}).setOnComplete (() => {
				LinePoint linePoint = new LinePoint ();
				linePoint.point = rect.anchoredPosition;
				drawLine.points.Add (linePoint);
			});
		}
	}
}
