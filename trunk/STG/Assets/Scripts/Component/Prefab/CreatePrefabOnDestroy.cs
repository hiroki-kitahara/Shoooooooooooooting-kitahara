﻿/*===========================================================================*/
/*
*     * FileName    : CreatePrefabOnDestroy.cs
*
*     * Description : 死亡時にプレハブを生成するコンポーネント.
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CreatePrefabOnDestroy : MonoBehaviourExtension
{
	/// <summary>
	/// 生成するプレハブ.
	/// </summary>
	public GameObject prefab;
	
	/// <summary>
	/// 生成する座標を持つオブジェクト参照.
	/// </summary>
	public Transform refTarget;
	
	/// <summary>
	/// アプリケーションが終了しているか？.
	/// </summary>
	private bool isApplicationQuit = false;

	[SerializeField]
	private GameDefine.CreateType createType = GameDefine.CreateType.Instantiate;
	
	void OnApplicationQuit()
	{
		// アプリケーションが終了していればプレハブ生成をしないようにする.
		isApplicationQuit = true;
	}
	void OnDestroy()
	{
		if( !isApplicationQuit )
		{
			if( this.createType == GameDefine.CreateType.Instantiate )
			{
				Instantiate( prefab, refTarget.position, prefab.transform.rotation );
			}
			else
			{
				ObjectPool.Instance.GetGameObject( prefab, refTarget.position, prefab.transform.rotation );
			}
		}
	}
}
