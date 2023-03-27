using UnityEngine;
using General;

public class playerAnimationController : MonoBehaviour
{
    private Animator animator;
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";

    private float DamageAnimationCoolDownTime;
    private HandyCoolDown DamageCoolDown;

    void Start()
    {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();

        DamageAnimationCoolDownTime = this.GetComponent<playerAttribute>().DamageAnimationCoolDownTime;
        DamageCoolDown = null;

        GameEvents.current.onPlayerHitGroundEnter += AfterJump;
        GameEvents.current.onPlayerDeathEnter += PlayDead;
    }

    public void IdleToMove()
    { this.animator.SetBool(key_isRun, true); }

    public void MoveToIdle()
    { this.animator.SetBool(key_isRun, false); }

    public void ToJump()
    {   this.animator.SetBool(key_isJump, true); }

    public void AfterJump()
    { this.animator.SetBool(key_isJump, false); }

    public void TakeDamage()
    {   
        this.animator.SetBool(key_isDamage, true);
        DamageCoolDown = new HandyCoolDown(DamageAnimationCoolDownTime, "Player Damage Animation");
    }

    public void AfterDamage()
    { this.animator.SetBool(key_isDamage, false); }

    public void MeleeAttack()
    {
        this.animator.SetBool(key_isAttack01, true);
    }
    public void AfterMeleeAttack()
    {
        this.animator.SetBool(key_isAttack01, false);
    }
    void PlayDead()
    {
        this.animator.SetBool(key_isDead, true);
    }

    void Update() {
        if (DamageCoolDown != null)
        {
            bool done = DamageCoolDown.check();
            if (done) {
                AfterDamage();
                DamageCoolDown = null;
            }
        }
    }
}
