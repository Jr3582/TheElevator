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
    public Image feetPics;
    public Image staminaBackground;
    public Image staminaBarBackground;
    public Image staminaFill;
    public Image chickenStick;
    public Image HungerBar;
    //Inventory images;
    public Image InventorySlot1;
    public Image InventorySlot2;
    public Image InventorySlot3;
    public Image InventorySlot4;
    public Image InventorySlot5;
    public Image InventorySlot6;
    public Image Slot1;
    public Image Slot2;
    public Image Slot3;
    public Image Slot4;
    public Image Slot5;
    public Image Slot6;
    public Image InventoryBackground;
    public Image InventoryBorder;
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

    private IEnumerator FadeInCharacterUI()
    {
        float elapsedTime = 0f;

        // Initializing colors for all UI elements
        Color profileColor = profile.color;
        Color characterSpriteColor = characterSprite.color;
        Color heart1Color = heart1.color;
        Color heart2Color = heart2.color;
        Color heart3Color = heart3.color;
        Color bootColor = feetPics.color;
        Color stamBackground = staminaBackground.color;
        Color stamBarBackground = new Color(100f / 255f, 100f / 255f, 0f);
        Color staminaFillColor = staminaFill.color;
        Color chickenStickColor = chickenStick.color;
        Color hungerBarColor = HungerBar.color;
        Color inventoryBorderColor = new Color(141f / 255f, 48f / 255f, 48f / 255f);
        Color inventoryBackgroundColor = new Color(43f / 225f, 35f / 255f, 40f / 255f);
        Color inventorySlot1color = InventorySlot1.color;
        Color inventorySlot2color = InventorySlot2.color;
        Color inventorySlot3color = InventorySlot3.color;
        Color inventorySlot4color = InventorySlot4.color;
        Color inventorySlot5color = InventorySlot5.color;
        Color inventorySlot6color = InventorySlot6.color;
        Color slotColor1 = new Color(75f / 255f, 69f / 255f, 73f/ 255f);
        Color slotColor2 = new Color(75f / 255f, 69f / 255f, 73f/ 255f);
        Color slotColor3 = new Color(75f / 255f, 69f / 255f, 73f/ 255f);
        Color slotColor4 = new Color(75f / 255f, 69f / 255f, 73f/ 255f);
        Color slotColor5 = new Color(75f / 255f, 69f / 255f, 73f/ 255f);
        Color slotColor6 = new Color(75f / 255f, 69f / 255f, 73f/ 255f);

        // Set initial alpha to 0 for all elements
        profileColor.a = 0;
        characterSpriteColor.a = 0;
        heart1Color.a = 0;
        heart2Color.a = 0;
        heart3Color.a = 0;
        bootColor.a = 0;
        stamBackground.a = 0;
        stamBarBackground.a = 0;
        staminaFillColor.a = 0;
        chickenStickColor.a = 0;
        hungerBarColor.a = 0;
        inventoryBackgroundColor.a = 0;
        inventoryBorderColor.a = 0;
        inventorySlot1color.a = 0;
        inventorySlot2color.a = 0;
        inventorySlot3color.a = 0;
        inventorySlot4color.a = 0;
        inventorySlot5color.a = 0;
        inventorySlot6color.a = 0;
        slotColor1.a = 0;
        slotColor2.a = 0;
        slotColor3.a = 0;
        slotColor4.a = 0;
        slotColor5.a = 0;
        slotColor6.a = 0;

        // Apply the initial alpha
        profile.color = profileColor;
        characterSprite.color = characterSpriteColor;
        heart1.color = heart1Color;
        heart2.color = heart2Color;
        heart3.color = heart3Color;
        feetPics.color = bootColor;
        staminaBackground.color = stamBackground;
        staminaBarBackground.color = stamBarBackground;
        staminaFill.color = staminaFillColor;
        chickenStick.color = chickenStickColor;
        HungerBar.color = hungerBarColor;
        InventoryBackground.color = inventoryBackgroundColor;
        InventoryBorder.color = inventoryBorderColor;
        InventorySlot1.color = inventorySlot1color;
        InventorySlot2.color = inventorySlot2color;
        InventorySlot3.color = inventorySlot3color;
        InventorySlot4.color = inventorySlot4color;
        InventorySlot5.color = inventorySlot5color;
        InventorySlot6.color = inventorySlot6color;
        Slot1.color = slotColor1;
        Slot2.color = slotColor2;
        Slot3.color = slotColor3;
        Slot4.color = slotColor4;
        Slot5.color = slotColor5;
        Slot6.color = slotColor6;

        // Gradually increase the alpha value over time
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);

            profileColor.a = alpha;
            characterSpriteColor.a = alpha;
            heart1Color.a = alpha;
            heart2Color.a = alpha;
            heart3Color.a = alpha;
            bootColor.a = alpha;
            stamBackground.a = alpha;
            stamBarBackground.a = alpha;
            staminaFillColor.a = alpha;
            chickenStickColor.a = alpha;
            hungerBarColor.a = alpha;
            inventoryBackgroundColor.a = alpha;
            inventoryBorderColor.a = alpha;
            inventorySlot1color.a = alpha;
            inventorySlot2color.a = alpha;
            inventorySlot3color.a = alpha;
            inventorySlot4color.a = alpha;
            inventorySlot5color.a = alpha;
            inventorySlot6color.a = alpha;
            slotColor1.a = alpha;
            slotColor2.a = alpha;
            slotColor3.a = alpha;
            slotColor4.a = alpha;
            slotColor5.a = alpha;
            slotColor6.a = alpha;

            profile.color = profileColor;
            characterSprite.color = characterSpriteColor;
            heart1.color = heart1Color;
            heart2.color = heart2Color;
            heart3.color = heart3Color;
            feetPics.color = bootColor;
            staminaBackground.color = stamBackground;
            staminaBarBackground.color = stamBarBackground;
            staminaFill.color = staminaFillColor;
            chickenStick.color = chickenStickColor;
            HungerBar.color = hungerBarColor;
            InventoryBackground.color = inventoryBackgroundColor;
            InventoryBorder.color = inventoryBorderColor;
            InventorySlot1.color = inventorySlot1color;
            InventorySlot2.color = inventorySlot2color;
            InventorySlot3.color = inventorySlot3color;
            InventorySlot4.color = inventorySlot4color;
            InventorySlot5.color = inventorySlot5color;
            InventorySlot6.color = inventorySlot6color;
            Slot1.color = slotColor1;
            Slot2.color = slotColor2;
            Slot3.color = slotColor3;
            Slot4.color = slotColor4;
            Slot5.color = slotColor5;
            Slot6.color = slotColor6;


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        profileColor.a = 1;
        characterSpriteColor.a = 1;
        heart1Color.a = 1;
        heart2Color.a = 1;
        heart3Color.a = 1;
        bootColor.a = 1;
        stamBackground.a = 1;
        stamBarBackground.a = 1;
        staminaFillColor.a = 1;
        chickenStickColor.a = 1;
        hungerBarColor.a = 1;
        inventoryBackgroundColor.a = 1;
        inventoryBorderColor.a = 1; 
        inventorySlot1color.a = 1;
        inventorySlot2color.a = 1;
        inventorySlot3color.a = 1;
        inventorySlot4color.a = 1;
        inventorySlot5color.a = 1;
        inventorySlot6color.a = 1;
        slotColor1.a = 1;
        slotColor2.a = 1;
        slotColor3.a = 1;
        slotColor4.a = 1;
        slotColor5.a = 1;
        slotColor6.a = 1;

        profile.color = profileColor;
        characterSprite.color = characterSpriteColor;
        heart1.color = heart1Color;
        heart2.color = heart2Color;
        heart3.color = heart3Color;
        feetPics.color = bootColor;
        staminaBackground.color = stamBackground;
        staminaBarBackground.color = stamBarBackground;
        staminaFill.color = staminaFillColor;
        chickenStick.color = chickenStickColor;
        HungerBar.color = hungerBarColor;
        InventoryBackground.color = inventoryBackgroundColor;
        InventoryBorder.color = inventoryBorderColor;
        InventorySlot1.color = inventorySlot1color;
        InventorySlot2.color = inventorySlot2color;
        InventorySlot3.color = inventorySlot3color;
        InventorySlot4.color = inventorySlot4color;
        InventorySlot5.color = inventorySlot5color;
        InventorySlot6.color = inventorySlot6color;
        Slot1.color = slotColor1;
        Slot2.color = slotColor2;
        Slot3.color = slotColor3;
        Slot4.color = slotColor4;
        Slot5.color = slotColor5;
        Slot6.color = slotColor6;
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
