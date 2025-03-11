using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
	/// <summary>
	/// ScriptableObject型のイベント
	/// </summary>
	public abstract class GameEventWithArgs<T> : ScriptableObject
	{
		/// <summary>
		/// イベントリスナーのリスト
		/// </summary>
		private List<GameEventListenerWithArgs<T>> listeners =
			new List<GameEventListenerWithArgs<T>>();

		/// <summary>
		/// イベントリスナーの登録
		/// </summary>
		/// <param name="listener"></param>
		public void RegisterListener(GameEventListenerWithArgs<T> listener)
		{
			listeners.Add(listener);
		}

		/// <summary>
		/// イベントリスナーの解除
		/// </summary>
		/// <param name="listener"></param>
		public void UnregisterListener(GameEventListenerWithArgs<T> listener)
		{
			listeners.Remove(listener);
		}

		/// <summary>
		/// イベント発行
		/// </summary>
		public void Raise(T value)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i].OnEventRaised(value);
		}
	}
}