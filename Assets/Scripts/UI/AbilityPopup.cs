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
    [SerializeField]
    private ParticleSystem abilityShineFx;

    private GameManager gameManager;
    private Ability _ability;
    
    private void Awake() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StopShineFx();
    }

    // 클릭 이벤트
    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager.SelectAbility(_ability);
    }

    public void Set(Ability ability)
    {
        _ability = ability;
        img_abilityImage.sprite = ability.Sprite;
        text_abilityName.text = ability.Name;

        if (ability.Rank == 1)
        {
            text_abilityName.color = new Color(81f / 255f, 81f / 255f, 1f, 1f);
        }
        if (ability.Rank == 2)
        {
            text_abilityName.color = new Color(1f, 100f / 255f, 100f / 255f, 1f);
        }
        
        StartShineFx();
    }

    public void Clear()
    {
        img_abilityImage.sprite = null;
        text_abilityName.text = "";
        text_abilityName.color = new Color(185f / 255f, 185f / 255f, 185f / 255f, 1f);

        StopShineFx();
    }

    private void StartShineFx()
    {
        if (_ability.Rank == 0)
        {
            return;
        }

        abilityShineFx.gameObject.SetActive(true);
        abilityShineFx.Play();
        var main = abilityShineFx.main;

        if (_ability.Rank == 1)
        {
            // Uncommon (blue)
            main.startColor = new Color(81f / 255f, 81f / 255f, 1f, 1f);
        }
        else if (_ability.Rank == 2)
        {
            // Rare (red)
            main.startColor = new Color(150f / 255f, 50f / 255f, 50f / 255f, 1f);
        }
    }

    private void StopShineFx()
    {
        abilityShineFx.gameObject.SetActive(false);
        var main = abilityShineFx.main;
        main.startColor = new Color(0f, 0f, 0f, 1f);
        abilityShineFx.Stop();
    }
}
