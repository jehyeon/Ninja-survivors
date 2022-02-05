using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private GameObject go_abilityPopups;

    void Awake()
    {
        go_abilityPopups = transform.GetChild(0).gameObject;
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
}
