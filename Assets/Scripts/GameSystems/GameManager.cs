using UnityEngine;
using UnityEngine.InputSysten;

namespace ProjectCycle.GameSystems
{
	[RequireComponent(typeof(PlayerInput))]
	public class GameManager : Monobehavior
	{
		public PlayerInput Input { get; private set; }
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
		}
	}
}