using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectCycle.GameSystems
{
    public class SceneHandler : MonoBehaviour
    {
        private Image fade;
        private float fadeAlpha;
        private bool isFading;

        private void Start()
        {
            fade = GetComponentInChildren<Image>();
            fadeAlpha = 1;
            isFading = false;

            if (fade != null)
            {
                Color color = fade.color;
                color.a = fadeAlpha;
                fade.color = color;
            }

            MoveToScene(1);
        }

        public void MoveToScene(int scene)
        {
            if (!isFading)
            {
                StartCoroutine(FadeInOut(scene));
            }
        }

        IEnumerator FadeInOut(int scene)
        {
            isFading = true;

            // Fade out
            while (fadeAlpha < 1f)
            {
                fadeAlpha += Time.deltaTime;
                if (fade != null)
                {
                    Color color = fade.color;
                    color.a = fadeAlpha;
                    fade.color = color;
                }
                yield return null;
            }

            fadeAlpha = 1f;

            yield return SceneManager.LoadSceneAsync(scene);

            // Fade in
            while (fadeAlpha > 0f)
            {
                fadeAlpha -= Time.deltaTime;
                if (fade != null)
                {
                    Color color = fade.color;
                    color.a = fadeAlpha;
                    fade.color = color;
                }
                yield return null;
            }

            fadeAlpha = 0f;
            isFading = false;
        }
    }
}
