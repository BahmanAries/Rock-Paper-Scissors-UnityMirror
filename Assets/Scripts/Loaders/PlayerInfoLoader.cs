using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
	public delegate void OnLoadedAction(Hashtable playerData);
	public event OnLoadedAction OnLoaded;

	public void load(uint id, string name, int coins)
	{
		Hashtable mockPlayerData = new Hashtable();
		mockPlayerData["userId"] = id;
		mockPlayerData["name"] = name;
		mockPlayerData["coins"] = coins;

		OnLoaded(mockPlayerData);
	}
}