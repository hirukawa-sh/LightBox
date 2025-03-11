using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
	/// <summary>
	/// ScriptableObject型のイベント
	/// </summary>
	[CreateAssetMenu(fileName = "GameEventWithFloat", menuName = "ScriptableEvent/GameEventWithFloat", order = 2)]
	public class GameEventWithFloat : GameEventWithArgs<float>
	{
	}
}