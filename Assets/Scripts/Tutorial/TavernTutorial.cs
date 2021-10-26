using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TavernTutorial : MonoBehaviour
{
    [SerializeField]
    PlayableDirector director;
    [SerializeField]
    protected Transform DialogueUI;
    protected Sprite _sprite;
    [SerializeField]
    protected DialogSystem.ECharacterNames characterName;
    [SerializeField]
    protected int PhrasesInSpeech = 3;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Talk()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        var UI = Instantiate(DialogueUI);
        UI.GetComponent<DialogueUI>().Init(_sprite, characterName, PhrasesInSpeech);
        UI.GetComponent<DialogueUI>().onDialogEnded += CloseDialogAndContinueAnimation;
    }

    private void CloseDialogAndContinueAnimation()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
