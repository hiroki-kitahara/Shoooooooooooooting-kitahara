﻿/*===========================================================================*/
/*
*     * FileName    : A_StageChangeConditioner.cs
*
*     * Description : ステージ切り替えの条件処理の抽象.
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class A_StageChangeConditioner : GameMonoBehaviour
{
	/// <summary>
	/// 通常ルートか？.
	/// </summary>
	[SerializeField]
	protected bool isBasicRoot;

	/// <summary>
	/// 条件処理.
	/// trueなら裏ルート.
	/// </summary>
	public abstract bool Condition();
}
