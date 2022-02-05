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

    public void Set(int id, int imageId, string name)
    {
        _abilityId = id;
        img_abilityImage.sprite = Resources.Load<Sprite>("Abilities/" + imageId);
        text_abilityName.text = name;
    }

    public void Clear()
    {
        img_abilityImage.sprite = null;
        text_abilityName.text = "";
    }
}
