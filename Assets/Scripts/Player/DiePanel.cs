using System.Collections;
using UnityEngine;


public class DiePanel : MonoBehaviour
{
    [SerializeField]
    private Animator dieAnimator;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject image;
    public Transform results;


    public void PlayerStartDie()
    {
        var UI = InterfaceManager.Instance;
        UI.HideAll();
        background.SetActive(true);
        image.SetActive(true);

        dieAnimator.SetTrigger("Die");

    }
    public void SpawnResults()
    {
        Instantiate(results);
    }

}
