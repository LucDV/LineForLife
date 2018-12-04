using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexController : MonoBehaviour
{
	public RectTransform rect;
	public Vector2 Cordinate;
	public Image image;
	private Action<Vector2> Click;

	public void InitVertex(float size, Vector2 cor, Color color, Action<Vector2> click){
		rect.sizeDelta = new Vector2 (size, size);
		Cordinate = cor;
		Click = click;
		image.color = color;
	}

	public void ClickVertex ()
	{
		if (Click != null) {
			Click (Cordinate);
		}
	}
}
