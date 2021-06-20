using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public static LevelLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        if (!crossfadeAnimator)
            crossfadeAnimator = GetComponentInChildren<Animator>();
    }
    
}