using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : Singleton<PointSystem>
{
    public int points
    {
        get { return _points; }
        private set { _points = Mathf.Clamp(value, int.MinValue, int.MaxValue); }
    }
    protected int _points;

    public float happiness
    {
        get { return _happiness; }
        private set { _happiness = Mathf.Clamp(value, 0, 1000); }
    }
    protected float _happiness = 500f;

    protected float timer;
    float defaultTimer = 15f;
    float elapsed;
    bool coroutineRunning = false;
    Coroutine currentCoroutine;

    delegate void RewardSystem();

    RewardSystem defaultRewardSystem;


    private void Start()
    {
        defaultRewardSystem = DefaultReward;
    }
    private void Update()
    {
        if (!coroutineRunning && points != 0)
        {
            currentCoroutine = StartCoroutine(PointReset());
            coroutineRunning = true;
        }
        else if (points == 0 && currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            coroutineRunning = false;
            defaultRewardSystem = DefaultReward;
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f; ;
                defaultRewardSystem();
            }
        }

        if (points > 5)
        {
            defaultRewardSystem = HighReward;
        }
        else if (points > 0)
        {
            defaultRewardSystem = LowReward;
        }
        else if (points < 0)
        {
            defaultRewardSystem = LowPunish;
        }
    }

    IEnumerator PointReset()
    {
        timer = defaultTimer;

        while (timer > 0.0f)
        {
            defaultRewardSystem();
            yield return new WaitForSeconds(0.1f);
            timer -= 0.1f;
        }
        timer = defaultTimer;
        points = 0;
    }

    public void AddPoints(int value)
    {
        points += value;
        timer += 5f;
    }

    public void SubstractPoints(int value)
    {
        points -= value;
        timer += 5f;
    }

    #region reward/punish System

    void DefaultReward()
    {
        happiness -= 5f;

        if (ResourcesManager.Instance.viewers > 0)
        {
            ResourcesManager.Instance.SubstractViewers(2);
        }
    }
    void LowPunish()
    {
        happiness -= 1f;
        ResourcesManager.Instance.SubstractViewers(5);
    }
    void LowReward()
    {
        happiness += 0.25f;

        float randValue = UnityEngine.Random.value;

        if (randValue < 0.001)
        {
            int value = UnityEngine.Random.Range(1, 26);
            DonationsHandler.Instance.AddDonation(value);
        }
        else if (randValue < 0.033)
        {
            ResourcesManager.Instance.AddSubscriber();
        }
        else if (randValue < 0.1)
            ResourcesManager.Instance.AddViewers(1);
    }

    void HighReward()
    {
        happiness += 0.50f;

        float randValue = UnityEngine.Random.value;

        if (randValue < 0.001)
        {
            int value = UnityEngine.Random.Range(25, 100);
            DonationsHandler.Instance.AddDonation(value);
        }
        else if (randValue < 0.005)
        {
            ResourcesManager.Instance.AddSubscriber();
        }
        else if (randValue < 0.25)
            ResourcesManager.Instance.AddViewers(25);
    }


    #endregion
}
