using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    private PlayerController playerController;      // for mouse sensitivity;
    private SoundManager soundManager;              // for set volume
    private GameObject go_abilityPopups;
    [SerializeField]
    private ParticleSystem particle_levelUp;

    // 체력
    private Slider slider_HpBar;
    private TextMeshProUGUI text_hpBar;

    // 경험치
    private Slider slider_ExpBar;

    // 능력
    private AbilityList abilityList;

    // 게임 카운트
    private TextMeshProUGUI text_playTime;

    // 게임 오버
    private GameObject gameOverUI;

    // 옵션
    private GameObject optionUI;
    [SerializeField]
    private Slider slider_mouseSensitivity;
    [SerializeField]
    private GameObject go_mouseSensitivityInputField;
    [SerializeField]
    private Slider slider_masterVolume;
    [SerializeField]
    private Slider slider_BGMVolume;
    [SerializeField]
    private Slider slider_SFXVolume;

    private bool optionMode = false;
    public bool OptionMode { get { return optionMode; } }
    private bool editingMouseSensitivity;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        // 순서 중요
        go_abilityPopups = transform.GetChild(0).gameObject;
        slider_HpBar = transform.GetChild(1).GetComponent<Slider>();
        text_hpBar = slider_HpBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        slider_ExpBar = transform.GetChild(2).GetComponent<Slider>();
        abilityList = transform.GetChild(3).GetComponent<AbilityList>();
        text_playTime = transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        gameOverUI = transform.GetChild(5).gameObject;

        // 옵션
        optionUI = transform.GetChild(6).gameObject;
    }

    // 체력
    public void UpdateHpBar(int newHp, int maxHp)
    {
        slider_HpBar.value = (float)newHp / (float)maxHp;
        text_hpBar.text = newHp + " / " + maxHp;
    }

    // 경험치
    public void UpdateExpBar(float now)
    {
        slider_ExpBar.value = now;
    }

    // 어빌리티 (레벨 업 관련)
    public void OpenAbilityPopups()
    {
        go_abilityPopups.SetActive(true);
        particle_levelUp.gameObject.SetActive(true);
        particle_levelUp.Play();
    }

    public void CloseAbilityPopups()
    {
        go_abilityPopups.SetActive(false);
        particle_levelUp.gameObject.SetActive(false);
        particle_levelUp.Stop();
        ClearAbilityPopups();
    }

    public void FillAbilityPopups(List<Ability> abilities)
    {
        // 기본적으로 ability.Length == 3
        for (int i = 0; i < abilities.Count; i++)
        {
            go_abilityPopups.transform.GetChild(i).GetComponent<AbilityPopup>().Set(abilities[i]);
        }
    }

    private void ClearAbilityPopups()
    {
        // 기본적으로 ability.Length == 3
        for (int i = 0; i < 3; i++)
        {
            go_abilityPopups.transform.GetChild(i).GetComponent<AbilityPopup>().Clear();
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

    // 타이머
    public void UpdatePlayTime(int playTime)
    {
        int minute = playTime / 60;
        int second = playTime % 60;
        text_playTime.text = string.Format("{0:D2}:{1:D2}", minute, second);
    }

    // 게임 옵션
    public void OpenOptionUI()
    {
        optionUI.SetActive(true);
        optionMode = true;
    }

    public void CloseOptionUI()
    {
        optionUI.SetActive(false);
        optionMode = false;
    }
    public void SetMouseSensitivity(float lookSensitivity)
    {
        go_mouseSensitivityInputField.GetComponent<TMP_InputField>().text = string.Format("{0:0.0#}", lookSensitivity);
        slider_mouseSensitivity.value = lookSensitivity / 15f;
        playerController.ChangeLookSensitivity(lookSensitivity);
    }

    public void ChangeSliderMouseSensitivity()
    {
        if (editingMouseSensitivity)
        {
            // 마우스 감도 input 작성 중에는 slider 설정 감도 무시
            return;
        }

        // 마우스 감도 범위: 0.01 ~ 15.00;
        float lookSensitivity = (float)(Math.Truncate(slider_mouseSensitivity.value * 15f * 100) / 100);

        if (lookSensitivity == 0f)
        {
            lookSensitivity = 0.01f;
        }

        // slider_mouseSensitivity;

        go_mouseSensitivityInputField.GetComponent<TMP_InputField>().text = string.Format("{0:0.0#}", lookSensitivity);
        playerController.ChangeLookSensitivity(lookSensitivity);
    }

    public void ChangeInputMouseSensitivity()
    {
        editingMouseSensitivity = false;

        float lookSensitivity = float.Parse(go_mouseSensitivityInputField.GetComponent<TMP_InputField>().text);

        if (lookSensitivity > 15f)
        {
            lookSensitivity = 15f;
        }
        if (lookSensitivity < 0.01f)
        {
            lookSensitivity = 0.01f;
        }
        go_mouseSensitivityInputField.GetComponent<TMP_InputField>().text = string.Format("{0:0.0#}", lookSensitivity);
        slider_mouseSensitivity.value = lookSensitivity / 15f;
        playerController.ChangeLookSensitivity(lookSensitivity);
    }

    public void StartEditingMouseSensitivity()
    {
        editingMouseSensitivity = true;
    }

    public void ChangeMasterVolume()
    {
        soundManager.SetVolume("Master", slider_masterVolume.value);
    }

    public void ChangeBGMVolume()
    {
        soundManager.SetVolume("BGM", slider_BGMVolume.value);
    }

    public void ChangeSFXVolume()
    {
        soundManager.SetVolume("SFX", slider_SFXVolume.value);
    }
}
