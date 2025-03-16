using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;


public class MovimientoEnemigo : LivingEntity
{
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    float mycollisionRadius;
    float targetCollisionRadius;

    float distanciaAtaque = 2f;

    float NexAttackTime = 0;

    float TimeBetweenAttack = 0.5f;

    bool Atacando = false;

    Material materialInicial;
    Color colorOriginal;

    LivingEntity targetEntity;
    float damage = 1;

    public Animator animator;

    


    void Awake()
    {
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
    protected override void Start()
    {
        base.Start();
        mycollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        materialInicial = GetComponent<Renderer>().material;
        colorOriginal = materialInicial.color;
        targetEntity = target.GetComponent<LivingEntity>();
    }

    
    void Update()
    {


        if (!Atacando)
        {


            Vector3 dirtoTarget = (target.position - transform.position).normalized;
            Vector3 targetPosition = target.position - dirtoTarget * (mycollisionRadius + targetCollisionRadius + distanciaAtaque);

            if (pathfinder.enabled && pathfinder.isOnNavMesh)
            {
                pathfinder.SetDestination(targetPosition);
            }



            if (Time.time > NexAttackTime)
            {
                NexAttackTime = Time.time + TimeBetweenAttack;

                float sqrDsttoTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDsttoTarget <= Mathf.Pow(mycollisionRadius + targetCollisionRadius + distanciaAtaque, 2))
                {

                    Debug.Log("estoy al lado");
                    StartCoroutine(Attack());

                }

            }

        }

    }
    IEnumerator Attack()
    {
        pathfinder.enabled = false;
        Atacando = true;

        materialInicial.color = Color.cyan;

        Vector3 originalPosition = transform.position;

        Vector3 dirtoTarget = (target.position - transform.position).normalized;

        Vector3 AttackPosition = target.position - dirtoTarget * (mycollisionRadius + targetCollisionRadius);

        float percent = 0;
        float attackSpeed = 1;

       bool  hasAppliedDamage = false;

        while (percent <= 1)
        {
            if(percent >= 0.5f && !hasAppliedDamage){

                targetEntity.takehit(damage);
                hasAppliedDamage = true;
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolacion = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, AttackPosition, interpolacion);
            yield return null;
        }


        pathfinder.enabled = true;
        Atacando = false;
        materialInicial.color = colorOriginal;
    }





    public override void takehit(float damage)
    {
        base.takehit(damage);

    }


}
