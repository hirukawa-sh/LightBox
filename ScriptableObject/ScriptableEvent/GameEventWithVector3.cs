using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
	/// <summary>
	/// ScriptableObject型のイベント
	/// </summary>
	[CreateAssetMenu(fileName = "GameEventWithVector3", menuName = "ScriptableEvent/GameEventWithVector3", order = 5)]
	public class GameEventWithVector3 : GameEventWithArgs<Vector3>
	{
	}
}