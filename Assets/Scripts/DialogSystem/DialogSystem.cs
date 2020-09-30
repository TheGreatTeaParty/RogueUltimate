using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogText;
    public GameObject dialogWindow;
    public GameObject buttonContinue;
    [FormerlySerializedAs("SoundGapTime")] [SerializeField]
    private float soundGapTime = 0.3f;
    private bool _coroutineHasStarted = false;
    private Queue<string> _sentences;
    private Button playerCallBackbutton;


    void Start()
    {
        playerCallBackbutton = PlayerOnScene.Instance.GetComponentInChildren<PlayerButtonCallBack>().GetComponentInChildren<Button>();
        _sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        //Return Joystick to 0 position;
        InterfaceManager.Instance.GetComponentInChildren<FixedJoystick>().ResetInput();
        playerCallBackbutton.gameObject.SetActive(false);
        nameText.text = dialog.name;

        _sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0) 
        {
            EndDialog();
            return;
        }
        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(WriteSentence(sentence));
    }

    public void EndDialog()
    {
        //This is needed to give information to AI that he can move
        GetComponentInParent<Citizen>().Talk(false);
        dialogWindow.gameObject.SetActive(false);
        InterfaceManager.Instance.gameObject.SetActive(true);
        playerCallBackbutton.gameObject.SetActive(true);
        buttonContinue.gameObject.SetActive(false);
    }

    IEnumerator WriteSentence (string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            if(!_coroutineHasStarted)
                StartCoroutine(CallSound());
            yield return null;
        }
    }
    
    IEnumerator CallSound()
    {
        _coroutineHasStarted = true;
        yield return new WaitForSeconds(soundGapTime);
        AudioManager.Instance.Play("Talk");
        _coroutineHasStarted = false;
    }

}
