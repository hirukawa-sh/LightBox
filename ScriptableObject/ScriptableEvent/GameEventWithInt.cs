using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
	/// <summary>
	/// ScriptableObject型のイベント
	/// </summary>
	[CreateAssetMenu(fileName = "GameEventWithInt", menuName = "ScriptableEvent/GameEventWithInt", order = 3)]
	public class GameEventWithInt : GameEventWithArgs<int>
	{
	}
}