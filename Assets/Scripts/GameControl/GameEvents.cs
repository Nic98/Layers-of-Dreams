using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class GameEvents : MonoBehaviour
{   // https://www.youtube.com/watch?v=gx0Lt4tCDE0
    
    public static GameEvents current;

    private void Awake() { current = this; }

    /*************************************************************************/
    // for UI
    public event Action onPlayerChangeHealthEnter;
    public void PlayerChangeHealth() {
        if (onPlayerChangeHealthEnter != null) { onPlayerChangeHealthEnter(); }
    }
    public event Action<string, float, GameObject> onPlayerNewBuffEnter;
    public void PlayerNewBuff(string name, float time, GameObject icon) {
        if (onPlayerNewBuffEnter != null) { onPlayerNewBuffEnter.Invoke(name, time, icon); }
    }
    public event Action<string, float> onBuffInterrupedEnter;
    public void InterruptBuff(string name, float time) {
        if (onBuffInterrupedEnter != null) { onBuffInterrupedEnter.Invoke(name, time); }
    }
    public event Action<playerSkillController.PlayerSkill, float> onPlayerUseSkillEnter;
    public void PlayerUseSkill(playerSkillController.PlayerSkill skill, float coolDownTime)
    {
        if (onPlayerUseSkillEnter != null)
        {
            onPlayerUseSkillEnter.Invoke(skill, coolDownTime);
        }
    }
    /*************************************************************************/
    // enemy detection
    public event Action<int> onFindAllEnemyEnter;
    public void FindAllEnemy(int count) {
        if (onFindAllEnemyEnter != null) {onFindAllEnemyEnter.Invoke(count); }
    }
    public event Action<GameObject> onEnemyNearbyEnter;
    public void EnemyNearbyMode(GameObject Enemy)
    {   // set nearest enemy calculated from playerDetectEnemy to playerAttackController
        if (onEnemyNearbyEnter != null) {
            onEnemyNearbyEnter.Invoke(Enemy);
        }
    }
    /*************************************************************************/
    // for player animation
    public event Action<GameObject> onPlayerAttackEnter;
    public void PlayerDoAttack(GameObject Enemy)
    {   // when player does attack, change rotation to face enemy
        if (onPlayerAttackEnter != null) {
            onPlayerAttackEnter.Invoke(Enemy);
        }
    }
    public event Action onPlayerHitGroundEnter;
    public void PlayerHitGround()
    {   // hit ground --> change jump animation
        if (onPlayerHitGroundEnter != null) { onPlayerHitGroundEnter(); }
    }
    /*************************************************************************/
    // (mainly) for scene loading
    public event Action onPlayerDeathEnter;
    public void PlayerDeathEnter()
    {
        if (onPlayerDeathEnter != null) { onPlayerDeathEnter(); }
    }
    public event Action onPortalTriggeredEnter;
    public void TriggerPortal() {
        if (onPortalTriggeredEnter != null) { onPortalTriggeredEnter(); }
    }
    public event Action onInstantiatePortal;
    public void InstantiatePortal(){
        if (onInstantiatePortal != null) {
            onInstantiatePortal();
        }
    }
    public event Action onBackToMainMenuTriggerEnter;
    public void BackToMainMenu() {
        if (onBackToMainMenuTriggerEnter != null) { onBackToMainMenuTriggerEnter(); }
    }
    /*************************************************************************/
    public event Action onEnemyDeathEnter;
    public void EnemyDeathEnter() // bascially for animation
    {
        if (onEnemyDeathEnter != null) { onEnemyDeathEnter(); }
    }
}
