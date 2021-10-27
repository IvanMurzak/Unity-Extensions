using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveMonoBehaviour : SaverMonoBehaviour<SaveDataTest>
{
	protected override string SaverPath => "test";
	protected override string SaverFileName => "monoBehaviour.test";
}