using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPropertiesManager : MonoBehaviour
{
	public ItemHolder itemHolderPrefab;
	public int packageCountPerTrain;
	public int demondCountPerTrain;

	[Header ("0:Package 1:Diamond 2:LargePackage")]
	public ItemInfo[] itemInfoArr;
}
