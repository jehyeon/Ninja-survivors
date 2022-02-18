using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySlot : MonoBehaviour
{
    // 경험치 바 아래에 현재 소유 중인 어빌리티 각 슬롯
    // 어빌리티 이미지, 보유 개수를 보여줌
    [SerializeField]
    private Image img_abilityImage;
    [SerializeField]
    private TextMeshProUGUI text_count;
    
    private int _abilityId;
    private int _count;
    private int _maxCount;

    private void Awake()
    {
        _count = 1;
    }

    public void Set(Ability ability)
    {
        _abilityId = ability.Id;
        img_abilityImage.sprite = ability.Sprite;
        _maxCount = ability.MaxCount;

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
        // 어빌리티 popup을 통해서 한번에 1개씩 밖에 못먹음
        _count += count;
        if (_count == _maxCount)
        {
            // 최대 레벨 표기
            text_count.text = "Max";
            // 별도의 컬러 (ex. 테두리 수정)
        }
        else
        {
            text_count.text = _count.ToString();
        }
    }
}