using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Rank
    {
        Common,
        UnCommon,
        Rare,
        Unique,
        Legendary,
        Epic
    }
    
    protected int _id;
    protected string _name;
    protected string _description;
    protected Rank _rank; 

    public int ItemId { get { return _id; } }
    public string ItemName { get { return _name; } }
    public string ItemDescription { get { return _description; } }
    public Rank ItemRank { get { return _rank; } }
}
