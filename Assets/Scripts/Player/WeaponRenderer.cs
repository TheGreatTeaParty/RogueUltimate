using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private SpriteRenderer _playerSprite;
    [SerializeField]
    private SpriteRenderer _weaponSprite;
    private Animator _weaponAnimator;
    private int _prevIndex;
    private int _currentWeaponLayer;
    private PlayerStat _playerStat;
    private Vector3 position;
    public int PrevIndex => _prevIndex;
    private void Start()
    {
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        _weaponAnimator = GetComponent<Animator>();
        _playerSprite = PlayerOnScene.Instance.GetComponent<SpriteRenderer>();

        _playerStat = PlayerOnScene.Instance.GetComponent<PlayerStat>();
        PlayerOnScene.Instance.playerAttack.onAttacked += OnAttacked;
        PlayerOnScene.Instance.playerAttack.EndAttack += EndAttack;
        _weaponSprite.sortingOrder = _playerSprite.sortingOrder + 3;
        position = transform.localPosition;
    }
    
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new && _new.EquipmentType == EquipmentType.Weapon)
        {
            if(_old)
                _weaponAnimator.SetLayerWeight((int)_old.AttackAnimationType, 0);
            _weaponAnimator.SetLayerWeight((int)_new.AttackAnimationType, 1);
        }
        else if(!_new && _old.EquipmentType == EquipmentType.Weapon)
        {
            _weaponAnimator.SetLayerWeight((int)_old.AttackAnimationType, 0);
        }
    }
    private void Update()
    {
        _weaponSprite.sortingOrder = _playerSprite.sortingOrder + 3;
    }
    private void OnAttacked(AttackType attackType)
    {
        if(attackType == AttackType.None)
        {
            return;
        }

        else if(attackType == AttackType.Melee)
        {
            _weaponAnimator.SetInteger("Set", GenerateAttackSet());
        }
        ChangeAnimationSpeed(attackType);

        _weaponAnimator.SetBool("Attack",true);
    }

    private void EndAttack(AttackType attackType)
    {
        if (attackType != AttackType.None)
        {
            _weaponAnimator.SetBool("Attack", false);
            transform.localPosition = position;
        }
    }

    private int GenerateAttackSet()
    {
        if (_prevIndex == 0)
        {
            _prevIndex = 1;
            return _prevIndex;
        }
        else
        {
            _prevIndex = 0;
            return _prevIndex;
        }
    }

    private void ChangeAnimationSpeed(AttackType attackType)
    {
        if (attackType == AttackType.Magic)
            _weaponAnimator.speed = (0.8f / _playerStat.CastSpeed.Value);       //0.5 base attack animation duration in seconds
        else
        {
            _weaponAnimator.speed = (0.8f / _playerStat.AttackSpeed.Value);
        }
    }
}
