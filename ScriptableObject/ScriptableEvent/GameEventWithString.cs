using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
	/// <summary>
	/// ScriptableObject型のイベント
	/// </summary>
	[CreateAssetMenu(fileName = "GameEventWithString", menuName = "ScriptableEvent/GameEventWithString", order = 4)]
	public class GameEventWithString : GameEventWithArgs<string>
	{
	}
}