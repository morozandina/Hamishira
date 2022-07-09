using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPortalParticle : MonoBehaviour
{
    private Transform Target;
    
    private ParticleSystem system;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
    private bool StartUpdate;

    void Start() {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if(StartUpdate) {
            if (system == null)
                system = GetComponent<ParticleSystem>();
            
            var count = system.GetParticles(particles);
            for (int i = 0; i < count; i++) {
                var particle = particles[i];
                float distance = Vector3.Distance(Target.position, particle.position);
                if (distance > 0.1f)
                {
                    particle.position = Vector3.Lerp(particle.position, (Target.position - new Vector3(.5f, 0, 0)), Time.deltaTime / 0.1f);
                    particles[i] = particle;
                }
            }
            system.SetParticles(particles, count);
        }
    }

    public void OnStart() {
        StartCoroutine(HandleIt());
    }

    IEnumerator HandleIt()
    {
        yield return new WaitForSeconds(1.2f);
        StartUpdate = true;
        Destroy(gameObject, 3f);
    }
}
