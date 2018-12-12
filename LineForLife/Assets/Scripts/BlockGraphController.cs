using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum CurrentStatus{
	Lock = -1,
	Unlock,
	Complete
}

public class BlockGraphController : MonoBehaviour
{

	public GraphController graph;

	[SerializeField]
	private Button button;
	[SerializeField]
	private Text ID;
	[SerializeField]
	private Image Star;
	[SerializeField]
	private Image Lock;
	[SerializeField]
	private GraphData dataGraph;
	private float sizeGraph;
	private float sizeLine;
	private CurrentStatus Status;
	private Action<GraphData> SelectBlock;

	public void InitBlock (GraphData data, float sizegraph, float sizeline, Action<GraphData> selectBlock = null, CurrentStatus currenStatus = CurrentStatus.Unlock )
	{
		dataGraph = data;
		sizeGraph = sizegraph;
		sizeLine = sizeline;
		Status = currenStatus;
		SelectBlock = selectBlock;
		Color color = GameManager.Instance.listColor[UnityEngine.Random.Range(0, 8)];
		graph.InitGraph (dataGraph, sizeGraph, sizeLine, GameData.SIZE_VERTEX_MENU, color);
		ID.text = dataGraph.GraphID.ToString ();
		switch (currenStatus) {
		case CurrentStatus.Lock:
			InitLock ();
			break;
		case CurrentStatus.Unlock:
			InitUnlock ();
			break;
		case CurrentStatus.Complete:
			InitComplete ();
			break;
		}
	}

	private void InitLock ()
	{
		Star.gameObject.SetActive (false);
		Lock.gameObject.SetActive (true);
		Color color = button.image.color;
		color.a = 1;
		button.image.color = color;
	}

	private void InitUnlock ()
	{
		Star.gameObject.SetActive (false);
		Lock.gameObject.SetActive (false);
		Color color = button.image.color;
		color.a = 0;
		button.image.color = color;
	}

	private void InitComplete ()
	{
		Star.gameObject.SetActive (true);
		Lock.gameObject.SetActive (false);
		Color color = button.image.color;
		color.a = 0;
		button.image.color = color;
	}

	public void OnSelect(){
		if (Status != CurrentStatus.Lock) {
			if (SelectBlock != null) {
				GameManager.Instance.CurrentGraphID = dataGraph.GraphID;
				SelectBlock (dataGraph);
			}
		}
	}
}
