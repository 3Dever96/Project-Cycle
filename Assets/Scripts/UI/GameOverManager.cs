using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ProjectCycle.GameSystems;

namespace ProjectCycle.UI
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] TMPro.TMP_Text text;
        [SerializeField] GameObject button;

        float a = 0;

        private void Start()
        {
            a = 0;

            background.color = new Color(0f, 0f, 0f, a);
            text.color = new Color(1f, 0f, 0f, a);
            button.SetActive(false);
        }

        private void OnEnable()
        {
            GameManager.instance.gameOver += OnGameOver;
        }

        private void OnDisable()
        {
            GameManager.instance.gameOver -= OnGameOver;
        }

        void OnGameOver()
        {
            StartCoroutine(FadeInScreen());
        }

        public void OnContinue()
        {
            GameManager.instance.SceneHandler.MoveToScene(1);
            GameManager.instance.DungeonManager.completedDungeons = 0;
            GameManager.instance.gameState = GameState.Play;
        }

        IEnumerator FadeInScreen()
        {
            while (a < 1f)
            {
                a += Time.deltaTime;
                background.color = new Color(0f, 0f, 0f, a);
                text.color = new Color(1f, 0f, 0f, a);
                yield return null;
            }

            a = 1f;
            button.SetActive(true);
        }
    }
}
