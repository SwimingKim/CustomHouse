using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawnEffect : MonoBehaviour
{
    ParticleSystem _particleSystem;
    public GameObject effect;

    void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void ShowEffect()
    {
        Instantiate(effect, transform.position, Quaternion.identity);

        Destroy(gameObject, _particleSystem.main.duration);
    }


}
