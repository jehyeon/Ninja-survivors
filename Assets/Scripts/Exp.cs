using UnityEngine;

public class Exp
{
    private static int firstRequireExp = 10;
    private float _amount;    // 누적 경험치
    private int _level;     // 현재 레벨
    private float _now;     // 현재 경험치 percent
    private int _beforeExp;
    private int _requireExp;

    public float Now { get { return _now; } }
    
    public Exp()
    {
        this._amount = 0;
        this._level = 1;
        this._now = 0f;
        this._requireExp = firstRequireExp;
    }

    private void LevelUp()
    {
        this._level += 1;
        _beforeExp = _requireExp;
        _requireExp = (int)Mathf.Floor(_requireExp * 1.2f) + _beforeExp;
        this._now = (_amount - _beforeExp) / (_requireExp - _beforeExp);
    }

    public bool GainExp(int amount)
    {
        _amount += amount;
        this._now = (_amount - _beforeExp) / (_requireExp - _beforeExp);

        if (_amount >= _requireExp)
        {
            LevelUp();
            
            return true;    // 레벨업하면 return true
        }

        return false;
    }
}