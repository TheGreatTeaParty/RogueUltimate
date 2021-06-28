using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsUI : MonoBehaviour
{
    [SerializeField] NavigatorButton[] _navigatorButtons;
    private TraitSlotUI[] slots;
    private PlayerStat playerStat;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = CharacterManager.Instance.Stats;
        slots = GetComponentsInChildren<TraitSlotUI>();
        for (int i = 0; i < _navigatorButtons.Length; i++)
            _navigatorButtons[i].onWindowChanged += UpdateTraits;
    }

    public void UpdateTraits(WindowType windowType, NavigatorButton navigatorButton)
    {
        if(windowType == WindowType.SkillTree)
        {
            for(int i = 0; i < playerStat.PlayerTraits.Traits.Count; ++i)
            {
                slots[i].SetTraitProperties(playerStat.PlayerTraits.Traits[i].Icon, playerStat.PlayerTraits.Traits[i].Name, playerStat.PlayerTraits.Traits[i].BriefDescription);
            }
        }
    }
}
