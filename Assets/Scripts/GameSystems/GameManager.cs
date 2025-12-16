using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.GameSystems
{
	[RequireComponent(typeof(PlayerInput))]
	public class GameManager : MonoBehaviour
	{
		public PlayerInput Input { get; private set; }
		public PlayerManager PlayerManager { get; private set; }
		public SceneHandler SceneHandler { get; private set; }

		public static GameManager instance;
		public GameState gameState;
				
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				if (instance != this)
				{
					Destroy(gameObject);
				}
			}
								
			DontDestroyOnLoad(gameObject);
			
			Input = GetComponent<PlayerInput>();
			PlayerManager = GetComponent<PlayerManager>();
			SceneHandler = GetComponent<SceneHandler>();
		}
	}
}
