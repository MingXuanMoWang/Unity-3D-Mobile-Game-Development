using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_speed = 1;
    public float m_life = 10;
    public GameObject m_explosionFX;
    public int m_point = 10;
    protected float m_rotSpeed = 30;
    internal Renderer m_renderer;
    internal bool m_isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    private void OnBecameInvisible()
    {
        m_isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        if(m_isActive && !this.m_renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateMove()
    {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("PlayerRocket"))
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if(rocket != null )
            {
                m_life -= rocket.m_power;
                if(m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }else if(other.tag.Equals("Player"))
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }
}
