using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator crossfadeAnimator;

    private void Start()
    {
        if (!crossfadeAnimator)
            crossfadeAnimator = GetComponentInChildren<Animator>();
    }
    
}