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
		public DungeonManager DungeonManager { get; private set; }

		public static GameManager instance;
		public GameState gameState;

		public delegate void OnVictory();
		public OnVictory onVictory;

		public delegate void OnGameOver();
		public OnGameOver gameOver;
				
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
			DungeonManager = GetComponent<DungeonManager>();
		}

		public void SetGameOver()
		{
			if (gameOver != null)
			{
				gameState = GameState.GameOver;
				gameOver.Invoke();
			}
		}

		public void SetVictory()
		{
			if (onVictory != null)
			{
				gameState = GameState.Victory;
				onVictory.Invoke();
			}
		}

		public void ResetGame()
		{
			SceneHandler.MoveToScene(1);
        }
	}
}
