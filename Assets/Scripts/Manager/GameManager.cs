using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private UI ui;
    private AbilityManager abilityManager;

    private void Awake()
    {
        player = GameObject.Find("Player");
        ui = GameObject.Find("Canvas").GetComponent<UI>();

        abilityManager = new AbilityManager();
    }

    public void LevelUp()
    {
        FillAbility();
    }

    private void FillAbility()
    {
        ui.OpenAbilityPopups();

        List<int> abilityIds = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int abilityId = Random.Range(0, abilityManager.Count);
            int abilityImageId = (int)abilityManager.data[abilityId]["imageId"];
            string abilityName = abilityManager.data[abilityId]["name"].ToString();
            ui.FillAbilityPopup(i, abilityId, abilityImageId, abilityName);
        }
    }

    public void SelectAbility(int abilityId)
    {
        ui.CloseAbilityPopups();

        // Stat up -> player
    }
}
