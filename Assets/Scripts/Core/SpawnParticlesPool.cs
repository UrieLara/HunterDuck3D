using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesPool : MonoBehaviour
{
    public static SpawnParticlesPool Instance;

    [SerializeField] private ParticleSystem prefab;
    [SerializeField] private int poolSize = 20;

    private List<ParticleSystem> pool = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem ps = Instantiate(prefab, transform);

            ps.gameObject.SetActive(false);

            pool.Add(ps);
        }
    }

    private ParticleSystem GetParticle()
    {
        foreach (ParticleSystem ps in pool)
        {
            if (!ps.gameObject.activeInHierarchy)
                return ps;
        }

        return null;
    }

    public void Play(Vector3 position, Color color)
    {
        ParticleSystem ps = GetParticle();

        if (ps == null)
        {
            Debug.LogWarning("Particle Pool agotada.");
            return;
        }
            

        ps.transform.position = position + Vector3.up * 0.2f;

        var main = ps.main;
        main.startColor = color;

        ps.gameObject.SetActive(true);
        ps.Play();

        StartCoroutine(ReturnToPool(ps));
    }

    IEnumerator ReturnToPool(ParticleSystem ps)
    {
        yield return new WaitUntil(() => !ps.IsAlive());

        ps.gameObject.SetActive(false);
    }
}
