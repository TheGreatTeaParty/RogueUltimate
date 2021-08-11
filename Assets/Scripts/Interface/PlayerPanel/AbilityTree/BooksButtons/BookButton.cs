using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButton : MonoBehaviour
{
    [SerializeField]
    private SkillTree.TreeType _treeType;
    private Image sprite;

    private void Start()
    {
        sprite = GetComponent<Image>();
        AbilityManager.Instance.OnTreeChanged += UpdateButtonColor;
    }

    public void ChangeTree()
    {
        AbilityManager.Instance.OnTreeChanged?.Invoke(_treeType);
    }

    private void UpdateButtonColor(SkillTree.TreeType type)
    {
        if(type != _treeType)
        {
            sprite.color = Color.grey;
        }
        else
        {
            sprite.color = Color.white;
        }
    }
}
