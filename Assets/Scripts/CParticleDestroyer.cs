using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParticleDestroyer : MonoBehaviour {

	ParticleSystem _particleSystem;

	void Awake()
	{
		_particleSystem = GetComponentInChildren<ParticleSystem>();
	}

	void Start () {

		if (!_particleSystem.isPlaying)
		{
			_particleSystem.Play();
		}
		Destroy(gameObject, _particleSystem.main.duration);
		
	}
}
