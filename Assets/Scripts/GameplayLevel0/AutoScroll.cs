using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TMP_Text dialogueText;

    void Update() {
        if (dialogueText.preferredHeight > dialogueText.rectTransform.rect.height)
        {
            AutoScrollToBottom();
        }
    }

    private void AutoScrollToBottom() {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}
