using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{
	public RectTransform rect;
	public Vector2 Cordinate;

	private Action<Vector2> Click;

	public void InitVertex(float size, Vector2 cor, Action<Vector2> click){
		rect.sizeDelta = new Vector2 (size, size);
		Cordinate = cor;
		Click = click;
	}

	public void ClickVertex ()
	{
		if (Click != null) {
			Click (Cordinate);
		}
	}
}
