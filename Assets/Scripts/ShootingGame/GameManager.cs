using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform m_canvas_main;
    public Transform m_canvas_gameover;
    public Text m_text_score;
    public Text m_text_best;
    public Text m_text_life;
    public Button m_restart_button;

    protected int m_score = 0;
    public static int m_hiscore = 0;
    protected Player m_player;

    public AudioClip m_musicClip;
    protected AudioSource m_Audio;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_Audio = this.gameObject.AddComponent<AudioSource>();
        m_Audio.clip = m_musicClip;
        m_Audio.loop = true;
        m_Audio.Play();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        m_text_score.text = string.Format("分数    {0}", m_score);
        m_text_best.text = string.Format("最高分   {0}", m_hiscore);
        m_text_life.text = string.Format("生命  {0}", m_player.m_life);

        m_restart_button.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        m_canvas_gameover.gameObject.SetActive(false);
    }

    public void AddScore(int point)
    {
        m_score += point;
        if(m_hiscore < m_score)
            m_hiscore = m_score;
        m_text_score.text = string.Format("分数    {0}", m_score);
        m_text_best.text = string.Format("最高分   {0}", m_hiscore);
    }

    public void ChangeLife(float life)
    {
        m_text_life.text = string.Format("生命  {0}", m_player.m_life);
        if(life <= 0)
        {
            m_canvas_gameover.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
