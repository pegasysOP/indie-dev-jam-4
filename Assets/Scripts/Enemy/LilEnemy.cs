using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static IDamageable;

public class LilEnemy : BaseEnemy, IDamageable
{
    public Collider mainHitbox;
    public NavMeshAgent agent;
    public GameObject attackHitbox;

    public int maxHealth;
    public float chaseDistance;
    public float attackTime;
    public bool startActive;

    private int health;
    private Transform target;
    private float attackTimer;
    private Vector3 spawnLocation;
    private bool dead;

    private void Start()
    {
        spawnLocation = transform.position;
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
    }

    public override void Activate()
    {
        if (target == null)
            target = Player.Instance.transform;

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
        if (dead)
            return;

        health -= damage;

        if (health <= 0)
        {
            Death();
        }

        //AudioController.Instance.PlayZombieHurt();
    }

    private void Death()
    {
        Debug.Log($"{gameObject.name} died.");
        dead = true;
        mainHitbox.enabled = false;
        attackHitbox.SetActive(false);

        gameObject.SetActive(false);
    }

    public void OnPlayerHit()
    {
        Player.Health.TakeDamage(1);
        attackHitbox.SetActive(false);

        attackTimer = attackTime;
    }

    public override void Reset()
    {
        StopAllCoroutines();
        target = null;
        agent.ResetPath();
        transform.position = spawnLocation;
        health = maxHealth;
        dead = false;
        mainHitbox.enabled = true;
        attackHitbox.SetActive(true);
        attackTimer = attackTime;

        gameObject.SetActive(true);
    }

    public DamageType GetDamageType()
    {
        return DamageType.Lil;
    }
}
