using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    public Enemy m_enemy;
    // Start is called before the first frame update
    void Start()
    {
        m_enemy = this.GetComponentInParent<Enemy>();
    }

    private void OnBecameVisible()
    {
        m_enemy.m_isActive = true;
        m_enemy.m_renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
