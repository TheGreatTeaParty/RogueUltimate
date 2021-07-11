using System.Collections;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager.Scenes nextLevel;
    [SerializeField] private Vector3 NextLevelposition = Vector3.zero;
    [SerializeField] private Animator transition;
    [SerializeField] private Type type;
    private bool _isLoaded = false;
    
    public enum Type
    {
        portal = 0,
        door,
    }


    private void Start()
    {
        if (!transition)
            transition = FindObjectOfType<LevelLoader>().crossfadeAnimator;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isLoaded)
        {
            PlayerOnScene.Instance.interactDetaction.DeleteInteractionData();
            InterfaceManager.Instance.fixedJoystick.ResetInput();
            PlayerOnScene.Instance.HidePlayer();
            InterfaceManager.Instance.HideAll();
            _isLoaded = true;
            LoadNextLevel();
        }
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelAnimation());
    }
    
    private IEnumerator LoadLevelAnimation()
    {
        if (type == Type.portal)
        {
            AudioManager.Instance.Play("Teleport");
        }
        else if (type == Type.door)
            AudioManager.Instance.Play("TavernExit");
        //Dungeon sound/Door sound 

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        LevelManager.Instance.LoadScene(nextLevel, NextLevelposition);
    }

    public void SetNextLevel(LevelManager.Scenes next)
    {
        nextLevel = next;
    }
    
}
