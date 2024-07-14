using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public Collider mainHitbox;
    public NavMeshAgent agent;
    public MeshRenderer meshRenderer;
    public GameObject attackHitbox;
    public EnemyAnimator animator;

    public int maxHealth;
    public float chaseDistance;
    public float attackTime;
    public bool startActive;

    private int health;
    private Transform target;
    private float attackTimer;
    private Vector3 spawnLocation;
    private bool dead;
   
    private void Awake()
    {
        spawnLocation = transform.position;
    }

    private void Start()
    {
        health = maxHealth;
        dead = false;

        if (startActive)
            Activate();
    }

    private void Update()
    {
        if (!dead)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackHitbox.SetActive(true); 
            }
        }

        Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0f, agent.velocity.z);
        animator.SetMoveSpeed(horizontalVelocity.magnitude / agent.speed);
    }

    public void Activate()
    {
        if (target == null)
            target = PlayerController.Instance.transform;

        attackTimer = 0f;

        StartCoroutine(ChaseTick());
    }

    private IEnumerator ChaseTick()
    {
        while (!dead)
        {
            if (Vector3.Distance(transform.position, target.position) < chaseDistance)
                agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        StopDamageFlash(); // so they don't overlap
        health -= damage;

        if (health <= 0)
            Death();
        else
            animator.Hit(); //StartCoroutine(DoDamageFlash());
    }

    private void Death()
    {
        Debug.Log($"{gameObject.name} died.");
        dead = true;
        mainHitbox.enabled = false;
        attackHitbox.SetActive(false);
        animator.Die();
    }

    private IEnumerator DoDamageFlash()
    {
        meshRenderer.enabled = false;

        yield return new WaitForSeconds(0.05f);

        meshRenderer.enabled = true;
    }

    private void StopDamageFlash()
    {
        StopCoroutine(DoDamageFlash());

        meshRenderer.enabled = true;
    }

    public void OnPlayerHit()
    {
        PlayerController.PlayerHealth.TakeDamage(1);
        attackHitbox.SetActive(false);
        animator.Attack();

        attackTimer = attackTime;
    }

    public void Reset()
    {
        StopAllCoroutines();
        target = null;
        agent.ResetPath();
        transform.position = spawnLocation;
        health = maxHealth;
        animator.Reset();
        dead = false;
        mainHitbox.enabled = true;
        attackHitbox.SetActive(true);
        attackTimer = attackTime;
    }
}
