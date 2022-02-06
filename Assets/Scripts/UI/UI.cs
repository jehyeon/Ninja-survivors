using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    private GameObject go_abilityPopups;
    private Slider go_HpBar;
    private TextMeshProUGUI text_hpBar;
    private Slider go_ExpBar;

    void Awake()
    {
        go_abilityPopups = transform.GetChild(0).gameObject;
        go_HpBar = transform.GetChild(1).GetComponent<Slider>();
        text_hpBar = go_HpBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        go_ExpBar = transform.GetChild(2).GetComponent<Slider>();
    }
    
    public void OpenAbilityPopups()
    {
        go_abilityPopups.SetActive(true);
    }

    public void CloseAbilityPopups()
    {
        go_abilityPopups.SetActive(false);
    }

    public void FillAbilityPopup(int index, int abilityId, int abilityImageId, string abilityName)
    {
        go_abilityPopups.transform.GetChild(index).GetComponent<AbilityPopup>().Set(abilityId, abilityImageId, abilityName);
    }

    public void UpdateHpBar(int newHp, int maxHp)
    {
        go_HpBar.value = (float)newHp / (float)maxHp;
        text_hpBar.text = newHp + " / " + maxHp;
    }

    public void UpdateExpBar(float now)
    {
        go_ExpBar.value = now;
        // level text
    }
}
