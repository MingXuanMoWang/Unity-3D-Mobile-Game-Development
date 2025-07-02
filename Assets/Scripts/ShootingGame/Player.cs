using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_speed = 1;
    public float m_life = 3;
    public Transform m_rocket;

    public AudioClip m_shootClip;
    protected AudioSource m_audio;
    public Transform m_explosionFX;

    private Transform m_transform;
    private float m_rocketTimer = 0;

    protected Vector3 m_targetPos;
    public LayerMask m_inputMask;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
        m_audio = GetComponent<AudioSource>();
        m_targetPos = this.m_transform.position;
    }

    void MoveTo()
    {
        if(Input.GetMouseButton(0))
        {
            UnityEngine.Debug.Log("≤‚ ‘");
            Vector3 ms = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(ms);
            RaycastHit hitinfo;
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);
            if(iscast)
            {
                m_targetPos = hitinfo.point;
                UnityEngine.Debug.Log("”–…Êœ”" + hitinfo.point);
            }

      
        }
        Vector3 pos = Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed * Time.deltaTime);
        m_transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();

        //float movev = 0;
        //float moveh = 0;

        //if(Input.GetKey(KeyCode.UpArrow))
        //{
        //    movev += m_speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    movev -= m_speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    moveh -= m_speed * Time.deltaTime;
        //}
        //if(Input.GetKey(KeyCode.RightArrow))
        //{
        //    moveh += m_speed * Time.deltaTime;
        //}

        //m_transform.Translate(new Vector3 (moveh, 0, movev));

        m_rocketTimer -= Time.deltaTime;
        if(m_rocketTimer < 0)
        {
            m_rocketTimer = 0.1f;
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                //Instantiate(m_rocket, m_transform.position, m_transform.rotation);
                var p = PathologicalGames.PoolManager.Pools["mypool"];
                p.Spawn("Rocket", m_transform.position, m_transform.rotation, null);
                m_audio.PlayOneShot(m_shootClip);
            }
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Equals("PlayerRocket"))
        {
            m_life -= 1;
            GameManager.Instance.ChangeLife(m_life);
            if(m_life <= 0)
            {
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
