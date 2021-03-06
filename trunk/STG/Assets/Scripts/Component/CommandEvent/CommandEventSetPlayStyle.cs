﻿/*===========================================================================*/
/*
*     * FileName    : CommandEventSetPlayStyle.cs
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// .
/// </summary>
public class CommandEventSetPlayStyle : MonoBehaviour
{
	[SerializeField]
	private GameDefine.PlayStyleType playStyle;

	void OnCommandEvent()
	{
		Debug.Log( "Set PlayStyle = " + playStyle );
		GameStatusInterfacer.PlayStyle = playStyle;
	}
}
