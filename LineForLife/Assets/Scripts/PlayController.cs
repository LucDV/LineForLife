using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{

	// Use this for initialization

	public GraphController GraphPlay;

	private GraphData Data;

	public void InitPlay (GraphData data)
	{
		Data = data;
		InitGraphPlay (Data);
		gameObject.SetActive (true);
	}

	public void InitGraphPlay (GraphData data)
	{
		GraphPlay.InitGraph (data, 700, 10);
		GraphPlay.SetForPlay ();
	}
}
