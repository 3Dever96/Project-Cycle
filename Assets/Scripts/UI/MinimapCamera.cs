using UnityEngine;
using ProjectCycle.PlayerControl;
using UnityEditor.SceneManagement;

namespace ProjectCycle.UI
{
    public class MinimapCamera : MonoBehaviour
    {
        private PlayerStateMachine player;

        private void Update()
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerStateMachine>();
                transform.parent = player.transform;
                transform.localPosition = new Vector3(0f, 50f, 0f);
            }
        }
    }
}
