using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class JsonHelper
{
	public static T[] FromJson<T> (string json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
		return wrapper.Items;
	}

	public static string ToJson<T> (T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.Items = array;
		return JsonUtility.ToJson (wrapper);
	}

	public static string ToJson<T> (T[] array, bool prettyPrint)
	{
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.Items = array;
		return JsonUtility.ToJson (wrapper, prettyPrint);
	}

	private class Wrapper<T>
	{
		public T[] Items;
	}

	public static void WriteJson (string json)
	{
		if (!System.IO.File.Exists (Application.dataPath + "/" + GameData.NAME_FILE_GRAPH)) {
			System.IO.StreamWriter fileWriter = System.IO.File.CreateText (Application.dataPath + "/" + GameData.NAME_FILE_GRAPH);
			fileWriter.WriteLine (json);
			fileWriter.Close ();
		} else {
			System.IO.StreamWriter fileWriter = new System.IO.StreamWriter (Application.dataPath + "/" + GameData.NAME_FILE_GRAPH, true);
			fileWriter.WriteLine (json);
			fileWriter.Close ();
		}
	}
}

