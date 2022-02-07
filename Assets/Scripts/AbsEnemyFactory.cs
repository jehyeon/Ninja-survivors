using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsEnemyFactory : MonoBehaviour
{
    public abstract Enemy CreateEnemy(string type);
}
