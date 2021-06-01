using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Linq;

namespace Extensions.Unity.Editor
{
	public class AssetUtils
	{
		public static T[] LoadAssetsAtPath<T>(string path) where T : UnityEngine.Object
		{
			if (path.StartsWith("Assets/")) path = path.Substring("Assets/".Length, path.Length - "Assets/".Length);

			ArrayList al = new ArrayList();
			var fileEntries = Directory.GetFiles(Application.dataPath + "/" + path).Select(x => x.Replace("\\", "/"));
			foreach (var fileName in fileEntries)
			{
				int index = fileName.LastIndexOf("/");
				string localPath = path;

				if (index > 0)
					localPath += fileName.Substring(index);

				var t = AssetDatabase.LoadAssetAtPath<T>("Assets/" + localPath);

				if (t != null)
					al.Add(t);
			}
			T[] result = new T[al.Count];
			for (int i = 0; i < al.Count; i++)
				result[i] = (T)al[i];

			return result;
		}
	}
}