using UnityEngine;
using System.Collections;

/************************************************
 *         ImpermanetParticleSystem             *
 ************************************************
 *      Each particle needs to be moved counter *
 * to the implied movement of the world.        *
 ************************************************/

[RequireComponent(typeof(ParticleSystem))]
public class RelativeParticleSystem : MonoBehaviour {
	
	private ParticleSystem m_System;
	private ParticleSystem.Particle[] m_Particles;
	
	void Start() {
		m_System = GetComponent<ParticleSystem>();
		m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
	}
	
	// Update is called once per frame
	void Update () {
		int numParticlesAlive = m_System.GetParticles(m_Particles);
		for(int i=0;i<numParticlesAlive;i++)
		{
			m_Particles[i].position -= Impermanent.worldMovement*Time.deltaTime;
		}
		m_System.SetParticles(m_Particles,numParticlesAlive);
	}
}
