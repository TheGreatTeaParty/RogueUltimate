using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueManager
{
    void StartDialogue(Dialogue dialogue);
    void NextLine();
    void ChangeDialogueUIState(bool state);
    void ChangeLayout(Line.Position position);
}
