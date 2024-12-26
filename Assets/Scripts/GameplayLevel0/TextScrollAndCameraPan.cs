using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TextScrollAndCameraPan : MonoBehaviour {
    public TMP_Text dialogueText;
    public TMP_Text titleText;
    public TMP_Text titleBackground;
    public Image profile;
    public Image characterSprite;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image staminaBar;
    public Image staminaFill;
    public Image bookBag;
    public Image Journal;
    public Image chickenStick;
    public Image HungerBar;
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

        yield return StartCoroutine(FadeInCharacterUI());
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

    private IEnumerator FadeInCharacterUI() {
        float elapsedTime = 0f;

        Color profileColor = profile.color;
        Color characterSpriteColor = characterSprite.color;
        Color heart1Color = heart1.color;
        Color heart2Color = heart2.color;
        Color heart3Color = heart3.color;
        Color staminaBarColor = staminaBar.color;
        Color staminaFillColor = staminaFill.color;
        Color bookBagColor = bookBag.color;
        Color journalColor = Journal.color;
        Color chickenStickColor = chickenStick.color;
        Color hungerBarColor = HungerBar.color;

        profileColor.a = 0;
        characterSpriteColor.a = 0;
        heart1Color.a = 0;
        heart2Color.a = 0;
        heart3Color.a = 0;
        staminaBarColor.a = 0;
        staminaFillColor.a = 0;
        bookBagColor.a = 0;
        journalColor.a = 0;
        chickenStickColor.a = 0;
        hungerBarColor.a = 0;

        profile.color = profileColor;
        characterSprite.color = characterSpriteColor;
        heart1.color = heart1Color;
        heart2.color = heart2Color;
        heart3.color = heart3Color;
        staminaBar.color = staminaBarColor;
        staminaFill.color = staminaFillColor;
        bookBag.color = bookBagColor;
        Journal.color = journalColor;
        chickenStick.color = chickenStickColor;
        HungerBar.color = hungerBarColor;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            profileColor.a = alpha;
            characterSpriteColor.a = alpha;
            heart1Color.a = alpha;
            heart2Color.a = alpha;
            heart3Color.a = alpha;
            staminaBarColor.a = alpha;
            staminaFillColor.a = alpha;
            bookBagColor.a = alpha;
            journalColor.a = alpha;
            chickenStickColor.a = alpha;
            hungerBarColor.a = alpha;

            profile.color = profileColor;
            characterSprite.color = characterSpriteColor;
            heart1.color = heart1Color;
            heart2.color = heart2Color;
            heart3.color = heart3Color;
            staminaBar.color = staminaBarColor;
            staminaFill.color = staminaFillColor;
            bookBag.color = bookBagColor;
            Journal.color = journalColor;
            chickenStick.color = chickenStickColor;
            HungerBar.color = hungerBarColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        profileColor.a = 1;
        characterSpriteColor.a = 1;
        heart1Color.a = 1;
        heart2Color.a = 1;
        heart3Color.a = 1;
        staminaBarColor.a = 1;
        staminaFillColor.a = 1;
        bookBagColor.a = 1;
        journalColor.a = 1;
        chickenStickColor.a = 1;
        hungerBarColor.a = 1;

        profile.color = profileColor;
        characterSprite.color = characterSpriteColor;
        heart1.color = heart1Color;
        heart2.color = heart2Color;
        heart3.color = heart3Color;
        staminaBar.color = staminaBarColor;
        staminaFill.color = staminaFillColor;
        bookBag.color = bookBagColor;
        Journal.color = journalColor;
        chickenStick.color = chickenStickColor;
        HungerBar.color = hungerBarColor;
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
                // Debug.Log("Camera Position during pan: " + mainCamera.transform.position);
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
