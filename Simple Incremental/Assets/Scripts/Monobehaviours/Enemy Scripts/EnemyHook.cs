﻿using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(CharacterLoot))]
[RequireComponent(typeof(EnemyStateData))]
[RequireComponent(typeof(EnemyAttackRanged))]
[RequireComponent(typeof(EnemyAttackMelee))]
[RequireComponent(typeof(EnemyExperience))]
public class EnemyHook : MonoBehaviour
{
    public EnemyTemplate enemyTemplate = null;
    SpriteRenderer spriteRenderer = null;
    CharacterHealth characterHealth = null;
    CharacterLoot characterLoot = null;
    EnemyStateData enemyStateData = null;
    EnemyAttackRanged enemyAttackRanged = null;
    EnemyAttackMelee enemyAttackMelee = null;
    EnemyExperience enemyExperience = null;
    Animator anim = null;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterHealth = GetComponent<CharacterHealth>();
        characterLoot = GetComponent<CharacterLoot>();
        enemyStateData = GetComponent<EnemyStateData>();
        enemyAttackRanged = GetComponentInChildren<EnemyAttackRanged>();
        enemyAttackMelee = GetComponentInChildren<EnemyAttackMelee>();
        enemyExperience = GetComponent<EnemyExperience>();
        anim = GetComponent<Animator>();
    }

    public void Hook()
    {
        if (enemyTemplate == null)
        {
            return;
        }
        characterHealth.maxHealth = enemyTemplate.health;
        characterHealth.ResetHealth();
        enemyStateData.moveSpeed = enemyTemplate.moveSpeed;
        characterLoot.items = enemyTemplate.lootableItems;
        enemyExperience.experience = enemyTemplate.experience;
        if (enemyTemplate is BasicMob basicTemplate)
        {
            anim.enabled = false;
            spriteRenderer.sprite = basicTemplate.inGameSprite;
            enemyAttackRanged.enabled = false;
            enemyAttackMelee.enabled = true;
            enemyAttackMelee.damage = basicTemplate.meleeDamage;
            enemyAttackMelee.punchForce = basicTemplate.meleePunchForce;
            anim.enabled = true;
        }
    }
}