using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Berserk")] 
public class Berserk : ActiveAbility
{
    private float _counter = 0;
    private float _lastHealth;
    [SerializeField]
    private Effect furyEffect;
    
    
    public override void Activate()
    {
        base.Activate();
        
        var _stats = PlayerOnScene.Instance.stats;
        _lastHealth = _stats.CurrentHealth;
        _stats.EffectController.AddEffect(Instantiate(furyEffect), _stats);
    }

    protected override void Update()
    {
        base.Update();
    }
    
}