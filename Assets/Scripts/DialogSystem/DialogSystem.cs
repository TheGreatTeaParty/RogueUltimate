using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogText;
    [SerializeField]
    private GameObject dialogWindow;
    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log("Talk to " + dialog.name);
        nameText.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) 
        {
            EndDiolog();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(WriteSentence(sentence));
    }

    public void EndDiolog()
    {
        //This is needed to give information to AI that he can move
        GetComponentInParent<Citizen>().Talk(false);
        dialogWindow.gameObject.SetActive(false);
        InterfaceOnScene.instance.gameObject.SetActive(true);
        Debug.Log("End of diolog ");
    }

    IEnumerator WriteSentence (string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

}
