using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public Collider physicalHitBox;
    public NavMeshAgent agent;
    public GameObject attackHitbox;

    public int maxHealth;
    public float agroDistance;
    public float timeBetweenAttacks;
    public bool startActive;

    protected int health;
    protected Transform target;
    protected float attackTimer;
    protected Vector3 spawnLocation;
    protected bool dead;

    protected virtual void Start()
    {
        spawnLocation = transform.position;
        health = maxHealth;
        dead = false;

        if (startActive)
            Activate();
    }

    protected virtual void Update()
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

    public virtual void Activate()
    {
        if (target == null)
            target = Player.Instance.transform;

        attackTimer = 0f;

        StartCoroutine(ChaseTick());
    }

    protected virtual IEnumerator ChaseTick()
    {
        while (!dead)
        {
            if (Vector3.Distance(transform.position, target.position) < agroDistance)
                agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (dead)
            return;

        health -= damage;

        if (health <= 0)
            Death();
    }

    protected virtual void Death()
    {
        Debug.Log($"{gameObject.name} died.");
        dead = true;
        physicalHitBox.enabled = false;
        attackHitbox.SetActive(false);
        agent.ResetPath();
        GetComponent<AudioSource>().Stop();
    }

    public virtual void Reset()
    {
        StopAllCoroutines();
        target = null;
        agent.ResetPath();
        transform.position = spawnLocation;
        health = maxHealth;
        dead = false;
        physicalHitBox.enabled = true;
        attackHitbox.SetActive(true);
        attackTimer = timeBetweenAttacks;
        GetComponent<AudioSource>().Play();
    }

    public virtual void OnPlayerHit()
    {
        Player.Health.TakeDamage(1);
        attackHitbox.SetActive(false);

        attackTimer = timeBetweenAttacks;
    }
}
