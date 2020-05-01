using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instatnce; // 싱글턴을 할당할 전역변수

    public bool isGameover = false; // 게임오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임오버 시 활성화할 UI 게임 오브젝트

    private int score = 0; // 게임 점수

    private void Awake() // 싱글턴을 구성
    {

        if (instatnce == null) // 싱글턴 변수 instance가 비어 있는가?
        {
            instatnce = this; // instance가 비어 있다면 그곳에 자기 자신을 할당
        }
        else // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
        {
            // 씬에 두 개 이상의 GameManager 오브젝트가 존재한다는 의미
            // 싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            // 게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore)
    {
        if(!isGameover) // 게임오버가 아니라면
        {
            score += newScore; // 점수증가
            scoreText.text = "Score : " + score;
        }
    }

    public void OnPlayerDead() // 플레이어 캐릭터 사망 시 게임오버를 실행하는 메서드
    {
        isGameover = true; // 현재 상태를 게임오버 상태로 변경
        gameoverUI.SetActive(true); // 게임오버 UI 활성화
    }

}
