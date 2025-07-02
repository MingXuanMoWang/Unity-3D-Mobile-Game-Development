using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float m_speed = 10;
    public float m_power = 1.0f;

    private void OnBecameInvisible()
    {
        if(this.enabled)
        {
            Despawn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,0,m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Equals("Enemy"))
        {
            return;
        }
        Despawn();
    }

    void Despawn()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        var p = PathologicalGames.PoolManager.Pools["mypool"];

        if(p.IsSpawned(transform))
        {
            p.Despawn(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
