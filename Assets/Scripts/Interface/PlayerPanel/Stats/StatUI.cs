using System;
using System.Data;
using UnityEngine;


public class StatUI : MonoBehaviour
{
   private PlayerStat _playerStat;
   private StatButton[] _statButtons;
   public Transform attributes;

   private void Start()
   {
      _playerStat = PlayerStat.Instance;
      _playerStat.onChangeCallback += UpdateStats;
      
      _statButtons = attributes.GetComponentsInChildren<StatButton>();
      for (int i = 0; i < _statButtons.Length; i++)
      {
         _statButtons[i].onClickEvent += SetAttributePoint;
      }
   }

   private void SetAttributePoint(StatType statType)
   {
      switch (statType)
      {
         case StatType.Will:
         {
            
            break;
         }
         
         case StatType.Vitality:
         {
            
            break;
         }
         
         case StatType.Mind:
         {
            
            break;
         }
         
         case StatType.Agility:
         {
            
            break;
         }
      }

      UpdateStats();
   }

   private void UpdateStats()
   {

      UpdateUI();
   }
   
   private void UpdateUI()
   {
      
   }
   
}