using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.GameSystems
{
    // This class serves as the central manager for the game, handling global game state and player input.
    [RequireComponent(typeof(PlayerInput))]
    public class GameManager : MonoBehaviour
    {
        // Reference to the PlayerInput component attached to the GameManager GameObject.
        public PlayerInput Input { get; private set; }

        // Singleton instance of the GameManager to ensure only one instance exists.
        public static GameManager instance;

        // Enum or class representing the current state of the game.
        public GameState gameState;

        // Called when the GameManager is initialized.
        private void Awake()
        {
            // Check if an instance of GameManager already exists.
            if (instance == null)
            {
                // If no instance exists, set this as the singleton instance.
                instance = this;
            }
            else
            {
                // If another instance exists, destroy this GameObject to enforce the singleton pattern.
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            // Prevent the GameManager GameObject from being destroyed when loading a new scene.
            DontDestroyOnLoad(gameObject);

            // Initialize the PlayerInput component.
            Input = GetComponent<PlayerInput>();
        }
    }
}
