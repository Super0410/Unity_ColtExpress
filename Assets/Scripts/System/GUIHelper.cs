using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHelper : Singleton<GUIHelper>
{
	public void DestroyChildImmediatly<T> (Transform parent) where T : MonoBehaviour
	{
		if (parent.childCount > 0) {
			T[] childIdentityArr = parent.GetComponentsInChildren<T> ();
			for (int i = 0; i < childIdentityArr.Length; i++) {
				DestroyImmediate (childIdentityArr [i].gameObject);
			}
		}
	}

	public K[] InstantiateTUnderParent<K,T> (T[] instantiateArr, K prefab, Transform parent) where K : MonoBehaviour
	{
		K[] returnArr = new K[instantiateArr.Length];
		for (int i = 0; i < instantiateArr.Length; i++) {
			GameObject newT = Instantiate (prefab.gameObject) as GameObject;
			newT.transform.SetParent (parent, false);
			returnArr [i] = newT.GetComponent<K> ();
		}
		return returnArr;
	}
}
