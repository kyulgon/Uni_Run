using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았는가

    private void OnEnable()
    {
        stepped = false; // 밝힘 상태를 리셋

        for(int i=0; i<obstacles.Length; i++) // 장애물의 수만큼 루프
        {
            if(Random.Range(0,3) == 0) // 현재 순번의 장애물을 1/3의 확률로 활성화
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 상대방의 태그가 Player이고 이전에 플레이어 캐릭터가 밟지 않았다면
        if(collision.collider.tag == "Player" && !stepped)
        {
            // 점수를 추가하고 밟힘 상태를 참으로 변경
            stepped = true;
            GameManager.instatnce.AddScore(1);
        }
    }

}
