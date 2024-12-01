using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f;  // 한 칸 이동에 필요한 시간
    private bool isMoved = false;   // 오브젝트의 이동/대기 제어 변수

    public bool MoveTo(Vector3 moveDirection)
    {
        // 이동중이라면 메서드가 실행되지 않게 함
        if (isMoved)
        {
            return false;
        }

        // 현재 위치로부터 이동방향으로 1 단위 이동한 위치를 매개변수로 코루틴 메소드 실행
        StartCoroutine(SmoothGridMovemet(transform.position + moveDirection));

        return true;
    }

    private IEnumerator SmoothGridMovemet(Vector2 endPostion)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        // movTime 시간동안  while() 반복문 출력
        // while() 반복문을 호출하는 동안 isMoved = true, 반복문 종료 시 isMoved = false
        isMoved = true;

        while (percent < 1) 
        {
            percent += Time.deltaTime;
            // startPosition에서 endPosition까지 moveTime 시간 동안 이동
            transform.position = Vector2.Lerp(startPosition, endPostion, percent);

            yield return null;
        }

        isMoved = false;
    }
}
