using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    private GameObject go_abilityPopups;

    // 체력
    private Slider go_HpBar;
    private TextMeshProUGUI text_hpBar;

    // 경험치
    private Slider go_ExpBar;

    // 능력
    private AbilityList abilityList;

    // 게임 오버
    private GameObject gameOverUI;

    void Awake()
    {
        go_abilityPopups = transform.GetChild(0).gameObject;
        go_HpBar = transform.GetChild(1).GetComponent<Slider>();
        text_hpBar = go_HpBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        go_ExpBar = transform.GetChild(2).GetComponent<Slider>();
        abilityList = transform.GetChild(3).GetComponent<AbilityList>();
        gameOverUI = transform.GetChild(4).gameObject;
    }

    // 체력
    public void UpdateHpBar(int newHp, int maxHp)
    {
        go_HpBar.value = (float)newHp / (float)maxHp;
        text_hpBar.text = newHp + " / " + maxHp;
    }

    // 경험치
    public void UpdateExpBar(float now)
    {
        go_ExpBar.value = now;
    }

    // 어빌리티 (레벨 업 관련)
    public void OpenAbilityPopups()
    {
        go_abilityPopups.SetActive(true);
    }

    public void CloseAbilityPopups()
    {
        go_abilityPopups.SetActive(false);
    }

    public void FillAbilityPopups(List<Ability> abilities)
    {
        // 기본적으로 ability.Length == 3
        for (int i = 0; i < abilities.Count; i++)
        {
            go_abilityPopups.transform.GetChild(i).GetComponent<AbilityPopup>().Set(abilities[i]);
        }
    }

    // 현재 소지 중인 어빌리티
    public void AddAbilitySlot(Ability ability)
    {
        abilityList.Add(ability);
    }

    // 게임 재시작
    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    // 게임 종료
    public void ExitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OpenGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
