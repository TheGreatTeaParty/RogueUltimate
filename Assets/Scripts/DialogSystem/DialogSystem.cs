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
    [SerializeField]
    private float SoundGapTime = 0.3f;
    private bool CourantineHasStarted = false;
    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        //Return Joystick to 0 position;
        InterfaceOnScene.Instance.GetComponentInChildren<FixedJoystick>().ResetInput();

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
        InterfaceOnScene.Instance.gameObject.SetActive(true);
        Debug.Log("End of diolog ");
    }

    IEnumerator WriteSentence (string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            if(!CourantineHasStarted)
                StartCoroutine(CallSound());
            yield return null;
        }
    }
    IEnumerator CallSound()
    {
        CourantineHasStarted = true;
        yield return new WaitForSeconds(SoundGapTime);
        AudioManager.Instance.Play("Talk");
        CourantineHasStarted = false;
    }

}
