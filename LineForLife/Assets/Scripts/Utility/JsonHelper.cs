using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


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
		if (!File.Exists (Application.dataPath + "/" + GameData.NAME_FILE_GRAPH)) {
			StreamWriter fileWriter = File.CreateText (Application.dataPath + "/" + GameData.NAME_FILE_GRAPH);
			fileWriter.WriteLine (json);
			fileWriter.Close ();
		} else {
			StreamWriter fileWriter = new StreamWriter (Application.dataPath + "/" + GameData.NAME_FILE_GRAPH, true);
			fileWriter.WriteLine (json);
			fileWriter.Close ();
		}
	}

	public static List<string> ReadJson (string nameFile)
	{
		List<string> list = new List<string>();
		using (StreamReader sr = new StreamReader (Application.dataPath + "/" + nameFile)) {
			string line;
			while ((line = sr.ReadLine ()) != null) {
				list.Add (line);
			}
		}
		return list;
	}
}

