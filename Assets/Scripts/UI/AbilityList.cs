using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityList : MonoBehaviour
{
    // ability slot object pool 생성 -> ability 갯수만큼
    private ObjectPool abilitySlotOP;
    private List<int> abilityIds;

    private void Awake()
    {
        abilityIds = new List<int>();
        abilitySlotOP = GameObject.Find("Ability Slot Object Pool").GetComponent<ObjectPool>();
    }

    public void Add(Ability ability)
    {
        // if ()
        // ability.Id가 abilityIds에 포함되어 있으면 그 index를 가져와서
        // GetChild(index).GetComponent<AbilitySlot>().UpdateCount()
        // else
        
        // Object pool에서 가져옴
        GameObject go_slot = abilitySlotOP.Get();
        
        // Add
        go_slot.GetComponent<AbilitySlot>().Set(ability);
        go_slot.transform.parent = this.transform;
        abilityIds.Add(ability.Id);
    }
}