using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Berserk")] 
public class Berserk : ActiveAbility
{
    private float _counter = 0;
    private float _lastHealth;
    private float _currentHealth;
    [SerializeField]
    private Effect immortalEffect;
    
    
    public override void Activate()
    {
        base.Activate();
        
        _currentHealth = PlayerOnScene.Instance.stats.CurrentHealth;
        _lastHealth = _currentHealth;
        var _stats = PlayerOnScene.Instance.stats;
        _stats.EffectController.AddEffect(Instantiate(immortalEffect), _stats);
    }

    protected override void Update()
    {
        //base.Update();
        Debug.Log(_counter.ToString());
        if (_counter > 0)
        {
            _currentHealth = _lastHealth;
            _counter -= Time.deltaTime;
            Debug.Log("++");
        }
    }
    
}