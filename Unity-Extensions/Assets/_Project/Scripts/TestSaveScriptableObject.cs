using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveScriptableObject : SaverScriptableObject<SaveDataTest>
{
	protected override string SaverPath => "test";
	protected override string SaverFileName => "scriptableObject.test";

	protected override void OnDataLoaded(SaveDataTest data)
	{

	}
}