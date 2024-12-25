using UnityEngine;
using TMPro;
using System.Collections;

public class TextScrollAndCameraPan : MonoBehaviour {
    public TMP_Text dialogueText;
    public TMP_Text titleText;
    public TMP_Text titleBackground;
    public RectTransform textContainer;
    public Camera mainCamera;
    public Vector3 cameraTargetPosition;
    public float scrollSpeed = 50f;
    public float panSpeed = 2f;
    public float typingSpeed = 0.1f;
    public string fullText;
    public float fadeDuration = 1f;
    public float titleDisplayTime = 5f;
    public GameObject player;
    public float followSpeed = 5f;

    private bool cameraPanning = false;
    private bool cameraFollowingPlayer = false;

    void Start() {
        ResetTitleAlpha();
        StartCoroutine(MainSequence());
    }

    private void ResetTitleAlpha() {
        Color titleColor = titleText.color;
        Color backgroundColor = titleBackground.color;

        titleColor.a = 0;
        backgroundColor.a = 0;

        titleText.color = titleColor;
        titleBackground.color = backgroundColor;
    }


    private IEnumerator MainSequence() {
        yield return StartCoroutine(TypeText());

        yield return StartCoroutine(FadeOutText(dialogueText));

        cameraPanning = true;

        yield return StartCoroutine(FadeInTitleAndBackground());
        
        yield return new WaitForSeconds(titleDisplayTime);

        yield return StartCoroutine(FadeOutTitleAndBackground());

        while (mainCamera.transform.position.y > cameraTargetPosition.y + 2f)
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                cameraTargetPosition,
                panSpeed * Time.deltaTime
            );
            yield return null;
        }

        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, cameraTargetPosition.y, mainCamera.transform.position.z);

        cameraPanning = false;

        cameraFollowingPlayer = true;
    }

    private IEnumerator TypeText() {
        dialogueText.text = "";
        foreach (char letter in fullText)
        {
            if (letter == '.')
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
                dialogueText.text += "\n";
            }
            else
            {
                dialogueText.text += letter;
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator FadeOutText(TMP_Text text) {
        float elapsedTime = 0f;
        Color color = text.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        text.color = color;
    }

    private IEnumerator FadeInTitleAndBackground() {
        float elapsedTime = 0f;

        titleText.alpha = 0;
        titleBackground.alpha = 0;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            titleText.alpha = alpha;
            titleBackground.alpha = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        titleText.alpha = 1;
        titleBackground.alpha = 1;
    }



    private IEnumerator FadeOutTitleAndBackground() {
        float elapsedTime = 0f;
        Color titleColor = titleText.color;
        Color backgroundColor = titleBackground.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            titleColor.a = alpha;
            backgroundColor.a = alpha;

            titleText.color = titleColor;
            titleBackground.color = backgroundColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        titleColor.a = 0;
        backgroundColor.a = 0;
        titleText.color = titleColor;
        titleBackground.color = backgroundColor;
    }

    void Update() {
        // Debug.Log("Update method is running");
        if (cameraPanning)
        {
            // Debug.Log("Camera Panning");
            if (mainCamera.transform.position.y - 1 > cameraTargetPosition.y)
            {
                mainCamera.transform.position = Vector3.Lerp(
                    mainCamera.transform.position,
                    cameraTargetPosition,
                    panSpeed * Time.deltaTime
                );
                Debug.Log("Camera Position during pan: " + mainCamera.transform.position);
            }
        }

        if (cameraFollowingPlayer)
        {
            // Debug.Log("Camera Following Player");
            // Debug.Log("Camera Position: " + mainCamera.transform.position);
            // Debug.Log("Player Position: " + player.transform.position);

            float yOffset = 10f;

            Vector3 targetPosition = player.transform.position;
            targetPosition.z = mainCamera.transform.position.z;
            targetPosition.y += yOffset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Debug.Log("New Camera Position after follow: " + mainCamera.transform.position);
        }
    }
}
