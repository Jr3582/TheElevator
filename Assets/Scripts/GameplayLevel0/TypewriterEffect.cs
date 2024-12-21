using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text dialogueText;
    public string fullText;
    public float typingSpeed = 0.1f;

    private string currentWord = "";
    private string displayedText = "";

    void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = "";
        displayedText = "";

        foreach (char letter in fullText)
        {
            if (letter == '.')
            {
                AppendCurrentWordToDialogue();
                displayedText += ".\n";
                dialogueText.text = displayedText;

                currentWord = "";
                yield return new WaitForSeconds(typingSpeed);
            }
            else if (char.IsWhiteSpace(letter) || char.IsPunctuation(letter))
            {
                AppendCurrentWordToDialogue();
                displayedText += letter;
                dialogueText.text = displayedText;

                currentWord = "";
                yield return new WaitForSeconds(typingSpeed);
            }
            else
            {
                currentWord += letter;
                dialogueText.text = displayedText + currentWord;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        AppendCurrentWordToDialogue();
    }

    private void AppendCurrentWordToDialogue()
    {
        if (!string.IsNullOrEmpty(currentWord)) {
            if (IsSpecialWord(currentWord))
            {
                displayedText += $"<color=#FF0000>{currentWord}</color>";
            }
            else
            {
                displayedText += currentWord;
            }
        }
        dialogueText.text = displayedText;
    }

    private bool IsSpecialWord(string word)
    {
        string[] specialWords = { "extinction", "hysteria", "gone", "infected", "die", "10", "7", "days", "dead", "infection" };
        foreach (string specialWord in specialWords)
        {
            if (word.TrimEnd('.', '!', '?') == specialWord)
            {
                return true;
            }
        }
        return false;
    }
}
