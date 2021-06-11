﻿using System;
using UnityEngine;

public class Character
{
    private int level;
    private int exp;
    private int str, bal, vtp;
    private Nature nature;
    private int hp;
    private int rank, potential;

    private GameObject prefab;

    public GameObject Prefab { get => prefab; set => prefab = value; }

    public Character(string prefabPath, int str, int bal, int vtp, int potential, Nature nature)
    {
        level = 1;
        exp = 0;

        this.str = str;
        this.bal = bal;
        this.vtp = vtp;
        this.nature = nature;
        this.potential = potential;
        hp = vtp * 400;
        prefab = Resources.Load<GameObject>(prefabPath);
    }

    public Character(string prefabPath, int level, int str, int bal, int vtp, int potential, Nature nature)
        : this(prefabPath, str, bal, vtp, potential, nature)
    {
        LevelUp(level);
    }

    public void LevelUp()
    {
        if (GetReqExp() > exp)
            return;

        level++;
        int statusSum = (int)(Math.Log10(100 + level) * 15 + potential * 1.2);
        str += statusSum / 3;
        vtp += statusSum / 3;
        bal += statusSum / 3;

        exp = 0;
    }

    public void LevelUp(int level)
    {
        while (this.level < level)
        {
            exp = GetReqExp();
            LevelUp();
        }
    }

    public int GetCP()
    {
        return str * 8 + bal * 3 + vtp * 4;
    }

    public Nature GetNature()
    {
        return nature;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetExp()
    {
        return exp;
    }

    public int GetReqExp()
    {
        int levelWeight = (int)Math.Pow(level, 1.3);
        return levelWeight * (200 + (potential * 80) * (rank + 5) / 10);
    }

    public int GetMaxHP()
    {
        return vtp * 400;
    }

    public int GetHP()
    {
        return hp;
    }

    public void GetHit(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public int GetDamage()
    {
        int maxDamage = str * 15;
        int minDamage = str * 15 * bal / 2;

        return UnityEngine.Random.Range(minDamage, maxDamage);
    }

    public int GetMaxDamage()
    {
        return str * 15;
    }

    public void SetBoss()
    {
        str = str * 4;
        bal = bal * 4;
        vtp = vtp * 4;
    }

    public void AddExp(int addExp)
    {
        int reqExp = GetReqExp();
        exp += addExp;
        while(reqExp < exp)
        {
            int remainExp = exp - reqExp;
            LevelUp();
            reqExp = GetReqExp();
            exp += remainExp;
        }
    }
}
