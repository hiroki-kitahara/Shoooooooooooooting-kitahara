/*===========================================================================*/
/*
*     * FileName    : OptionData.cs
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
public static class OptionData
{
	public static SaveData.Settings Settings
	{
		get
		{
			if( settings == null )
			{
				settings = new SaveData.Settings( SaveData.Settings.Instance );
			}

			return settings;
		}
	}
	private static SaveData.Settings settings;

	public static void Default()
	{
		settings = new SaveData.Settings();
	}

	public static void Remove()
	{
		settings = null;
	}
}
