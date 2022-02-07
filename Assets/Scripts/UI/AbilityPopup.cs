using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AbilityPopup : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image img_abilityImage;
    [SerializeField]
    private TextMeshProUGUI text_abilityName;

    private GameManager gameManager;
    private int _abilityId;
    
    private void Awake() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // 클릭 이벤트
    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager.SelectAbility(_abilityId);
    }

    public void Set(Ability ability)
    {
        _abilityId = ability.Id;
        img_abilityImage.sprite = ability.Sprite;
        text_abilityName.text = ability.Name;
    }

    public void Clear()
    {
        img_abilityImage.sprite = null;
        text_abilityName.text = "";
    }
}