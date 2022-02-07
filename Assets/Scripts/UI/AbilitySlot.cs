using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField]
    private Image img_abilityImage;
    [SerializeField]
    private TextMeshProUGUI text_abilityName;
    [SerializeField]
    private TextMeshProUGUI text_count;
    
    private int _abilityId;
    private int _count;

    private void Awake()
    {
        _count = 1;
    }

    public void Set(Ability ability)
    {
        _abilityId = ability.Id;
        img_abilityImage.sprite = ability.Sprite;
        text_abilityName = ability.Name;

        if (_count == 1)
        {
            text_count.text = "";
        }
        else
        {
            UpdateCount(0);
        }
    }

    public void UpdateCount(int count = 1)
    {
        _count += count;
        text_count.text = _count;
    }
}