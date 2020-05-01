using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판 수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y 값
    public float yMax = 1.5f; // 배치할 위치의 최대 y 값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성한 발판을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start()
    {
        platforms = new GameObject[count]; // count만큼의 공간을 가지는 새로운 발판 배열 생성

        for(int i = 0; i < count; i++) // count만큼 루프하면서 발판 생성
        {
            // platformPrefab을 원본으로 새 발판을 pooPosition 위치에 복제 생성
            // 생성된 발판을 platform 배열에 할당
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f; // 마지막 배치 시점 초기화
        timeBetSpawn = 0f; // 다음번 배치까지의 시간 간격을 0으로 초기화
    }

    
    void Update()
    {
        if(GameManager.instatnce.isGameover) 
        {
            // 게임오버 상태에서는 동작하지 않음
            return;
        }

        if(Time.time >= lastSpawnTime + timeBetSpawn) // 마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렸다면
        {
            lastSpawnTime = Time.time; // 기록된 마지막 배치 시점을 현재 시점으로 갱신

            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 다음 배치까지의 시간간격을 랜덤 설정

            float yPos = Random.Range(yMin, yMax); // 배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 설정

            // 사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화
            // 이때 발판의 Platform 컴포넌트의 OnEnable 메서드가 실행됨
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos); // 현재 순번의 발판을 화면 오른쪽에 재배치
            currentIndex++; // 순번 넘기기

            if(currentIndex >= count) // 마지막 순번에 도달했다면 순번을 리셋
            {
                currentIndex = 0;
            }

           

        }
    }
}
