using System.Collections;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager.Scenes nextLevel;
    [SerializeField] private Vector3 NextLevelposition = Vector3.zero;
    [SerializeField] private Animator transition;
    [SerializeField] private Type type;

    
    public enum Type
    {
        portal = 0,
        stairs,
    }


    private void Start()
    {
        if (!transition)
            transition = FindObjectOfType<LevelLoader>().crossfadeAnimator;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerOnScene.Instance.interactDetaction.DeleteInteractionData();
            InterfaceManager.Instance.fixedJoystick.ResetInput();
            PlayerOnScene.Instance.HidePlayer();
            InterfaceManager.Instance.HideAll();

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
        
        //Dungeon sound/Door sound 

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        
        LevelManager.LoadScene(nextLevel, NextLevelposition);
    }

    public void SetNextLevel(LevelManager.Scenes next)
    {
        nextLevel = next;
    }
    
}
