﻿/*===========================================================================*/
/*
*     * FileName    : CreatePrefabFromMiss.cs
*
*     * Description : ミス時にプレハブを生成するコンポーネント.
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;

public class CreatePrefabFromMiss : GameMonoBehaviour, I_MissEvent
{
	[SerializeField]
	private GameObject prefab;

	/// <summary>
	/// ミス処理
	/// </summary>
	public void OnMiss()
	{
		ObjectPool.Instance.GetGameObject( prefab, transform.position, prefab.transform.rotation );
	}
}
