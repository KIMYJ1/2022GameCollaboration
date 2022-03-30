using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    WATING,
    PLAY,
    GOAL
}

public class Player : MonoBehaviour
{
    public int attack { get; private set; }
    public int hp { get; private set; }
    public bool destination { get; private set; }
    public bool isActivate { get; private set; } = false;

    public Vector3 stanbyPos { get; private set; }
    public bool isMove { get; private set; } = false;

    public PLAYERSTATE playerState { get; private set; } = PLAYERSTATE.WATING;

    private int unitNumber;


    IEnumerator Move(int count)
    {
        if (isMove) yield break;

        count = Mathf.Clamp(count, 1, 5); //count�� ����1-5 ���̷� ����
        isMove = true;

        Place standingPlace = GameManager.instance.FindStandingPlace(transform.position);
        if (standingPlace)
        {          
            Vector3 startPos = transform.position;
            Place targetPlace = standingPlace.PostPlace[0];

            for (int i = 0; count > i; i++)
            {
                if(standingPlace.PostPlace.Length == 0)
                {
                    Debug.Log("Can`t find PostPlace");
                }
                else if (i == 0 && standingPlace.PostPlace.Length > 1)//count�� 0�� ����, �������ִ� ������ �ڳ����� Ȯ���ؼ� [1]�� �Ѱ��ٰ�, postplace ũ�Ⱑ 1�ϰ�� 0������ ����
                {
                    Debug.Log("shortcut");
                    targetPlace = standingPlace.PostPlace[1];
                }
                else
                {
                    targetPlace = standingPlace.PostPlace[0];                    
                }                    

                Vector3 targetPos = targetPlace.transform.position;


                float time = 0;

                while (1 > time)
                {
                    time += Time.deltaTime * 3.333f; // ������ 0.3f

                    transform.position = Vector3.Lerp(startPos, targetPos, time);

                    yield return null;
                }
                startPos = targetPos;
                standingPlace = targetPlace;
            }
            isMove = false;
            //GameManager.instance.removeStepsAtList();
        }
    }


    public void Movement(int rand1to5)//�̵� �ڵ�� �÷��̾ update ȣ���ϴ� ���� �ƴ� gm�� update���� ȣ���ؾ���
    {
        switch (playerState) //case�� �ϳ��� ���Ƶ� �Ǵ°�
        {
            case PLAYERSTATE.WATING:
                
                break;
            case PLAYERSTATE.PLAY:                

                Debug.Log(rand1to5);

                if (!isMove)
                {
                    StartCoroutine(Move(rand1to5)); //�����̴� �����߿� ���ٺҰ�
                }

                break;
            case PLAYERSTATE.GOAL:
                Debug.Log("fin");

                //if("Ư�����Ǹ���")
                //    playerState = PLAYERSTATE.PLAY;
                break;
        }
    }

    public void ConvertActivate()
    {
        isActivate = !isActivate;
        gameObject.SetActive(isActivate);
    }

    public void PositionToEntrance()
    {
        transform.position = GameManager.instance.FindPlaceToIndex(29).transform.position;
    }

    public void unitNumbering(int playerNum, int unitNum)
    {
        unitNumber = unitNum;

        stanbyPos = new Vector3(24 * (-playerNum) + unitNumber * 2, 2, 30);
        transform.position = stanbyPos;
    }

    private void OnMouseUpAsButton() //������ ���� ������ Ŭ���ȵ�
    {
        Debug.Log(unitNumber + " is clicked");
        GameManager.instance.ClickedUnitControl(unitNumber);
    }

    public void ChangeUnitState(PLAYERSTATE state)
    {
        playerState = state;
    }



    public void EncountMyUnit()//GameManager���� Ȯ�ε� �Ʊ������� �� ��ŭ �ɷ�ġ���� �� �ۼ�
    {
        //�ɷ�ġ �߰� HOST, GUEST ������

        gameObject.SetActive(false);
        transform.position = stanbyPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == gameObject.tag)
        {

        }
    }

}
