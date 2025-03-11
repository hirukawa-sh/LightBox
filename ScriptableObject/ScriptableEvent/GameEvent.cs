using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
	/// <summary>
	/// ScriptableObject型のイベント
	/// </summary>
	[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableEvent/GameEvent", order = 1)]
	public class GameEvent : ScriptableObject
	{
		/// <summary>
		/// イベントリスナーのリスト
		/// </summary>
		private List<GameEventListener> listeners =
			new List<GameEventListener>();

		/// <summary>
		/// イベントリスナーの登録
		/// </summary>
		/// <param name="listener"></param>
		public void RegisterListener(GameEventListener listener)
		{
			listeners.Add(listener);
		}

		/// <summary>
		/// イベントリスナーの解除
		/// </summary>
		/// <param name="listener"></param>
		public void UnregisterListener(GameEventListener listener)
		{
			listeners.Remove(listener);
		}

		/// <summary>
		/// イベント発行
		/// </summary>
		public void Raise()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised();
				Debug.Log($"{listeners[i]} -> {this} Raised.");
			}
		}
	}
}