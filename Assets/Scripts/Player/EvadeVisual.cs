using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvadeVisual : MonoBehaviour
{
    public PlayerStat playerStat;
    public Transform textPb;

    private MaterialPropertyBlock _collideMaterial;
    private SpriteRenderer _materialInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerStat.OnEvadeTriggered += Evade;
        //Material:
        _collideMaterial = new MaterialPropertyBlock();
        _materialInfo = GetComponent<SpriteRenderer>();
        _materialInfo.GetPropertyBlock(_collideMaterial);
    }

    private void Evade()
    {
        var position = transform.position;
        StartCoroutine("WaitAndChangeProperty");
        Transform text = Instantiate(textPb, new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.identity);
        TextMeshProUGUI temp = text.GetComponent<TextMeshProUGUI>();
        temp.text = LocalizationSystem.GetLocalisedValue("Evade");
        temp.fontSize = 2;
        temp.color = new Color32(166, 166, 166, 255);
    }
    private IEnumerator WaitAndChangeProperty()
    {
        _collideMaterial.SetFloat("Evade", 1f);
        _materialInfo.SetPropertyBlock(_collideMaterial);

        if (playerStat.CurrentHealth <= 0)
        {
            _collideMaterial.SetFloat("Evade", 0f);
            _materialInfo.SetPropertyBlock(_collideMaterial);
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(0.24f);

        _collideMaterial.SetFloat("Evade", 0f);
        _materialInfo.SetPropertyBlock(_collideMaterial);
    }
}
