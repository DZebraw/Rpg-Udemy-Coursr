using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed;
    
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1,1,1,sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            
            if(sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform,float _cloneDuration,bool _canAttack,Vector3 _offest)
    {
        if(_canAttack)
            anim.SetInteger("AttackNumber",Random.Range(1,3));
        
        transform.position = _newTransform.position + _offest;
        cloneTimer = _cloneDuration;

        FaceClosestTarget();
    }
    
    //此方法由第一次攻击最后一帧调用
    private void AnimationTrigger()
    {
        cloneTimer = -.1f;//攻击完后消失
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    //使面朝最近的敌人
    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        
        float closestDistance = Mathf.Infinity;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }

            if (closestEnemy != null)
            {
                if (transform.position.x > closestEnemy.position.x)
                {
                    transform.Rotate(0,180,0);
                }
            }
        }
    }
}
