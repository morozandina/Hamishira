using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDataDefault : MonoBehaviour
{
    void Awake() {
        PlayerPrefs.SetInt( "LevelCollect", 0 );
        // Delete up for forever saves current xp an max xp for change level
        PlayerPrefs.SetInt( "PlusDamage", 0 );
        PlayerPrefs.SetInt( "KilledMonsters", 0 );
        PlayerPrefs.SetInt( "CurrentLevel", 0 );
        // XP
        PlayerPrefs.SetInt( "MaxXP", 100 );
        PlayerPrefs.SetInt( "CurrentXP", 0 );
        PlayerPrefs.SetInt( "CurrentLevel", 0 );
        // HP
        PlayerPrefs.SetInt( "MaxHP", 50 );
        PlayerPrefs.SetInt( "MaxHP", PlayerPrefs.GetInt("MaxHP") + PlayerPrefs.GetInt("PreocentageUpgradeHP") );
        PlayerPrefs.SetInt( "CurrentHP", PlayerPrefs.GetInt("MaxHP") );
        // Enemy
        PlayerPrefs.SetInt( "EnemyHP", 0 );
        PlayerPrefs.SetInt( "EnemyDamage", 0 );
        // Ability reset for UI
        for (int i = 0; i < 17; i++) {
            PlayerPrefs.SetInt( "AbilityCount" + i, 0 );
        }
        // Ability auxiliar
        PlayerPrefs.SetInt( "ExtraJump", 0 );
        PlayerPrefs.SetFloat( "ExtraSpeed", 1 );
        PlayerPrefs.SetInt( "MinusDamage", 0 );
        PlayerPrefs.SetInt( "Ulta", 0 );
    }
}
