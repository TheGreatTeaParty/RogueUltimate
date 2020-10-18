using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Berserk")] 
public class Berserk : ActiveAbility
{
    private float _counter = 0;
    private float _lastHealth;
    private float _currentHealth;
    public float effectTime; 
    
    
    public override void Activate()
    {
        base.Activate();
        _counter = effectTime;
        
        _currentHealth = PlayerOnScene.Instance.stats.CurrentHealth;
        _lastHealth = _currentHealth;
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