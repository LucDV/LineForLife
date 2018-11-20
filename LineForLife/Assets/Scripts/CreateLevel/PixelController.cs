using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PixelController : MonoBehaviour {

	public RectTransform rect;
	public int X, Y;
	public Action<Vector2, int, int> choosePixel;

	public void InitPixel(int x, int y, Action<Vector2, int, int> action){
		X = x;
		Y = y; 
		choosePixel = action;
	}

	public void Choose(){
		if (choosePixel != null) {
			choosePixel (rect.anchoredPosition, X, Y);
		}
	}
}
