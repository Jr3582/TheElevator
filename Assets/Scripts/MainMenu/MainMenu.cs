using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void Start() {
        if(SceneManager.GetActiveScene().buildIndex != 0 ) {
            StartCoroutine(FadeIn());
        }
    }

    public void LoadGameplayLevel()
    {
        StartCoroutine(FadeOutAndLoadScene("GameplayLevel0"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        if (fadeImage != null)
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        SceneManager.LoadScene(sceneName);

        yield return new WaitForEndOfFrame();

        if (fadeImage != null)
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
    }

    private IEnumerator FadeIn()
    {
        if (fadeImage != null)
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
    }
}
