using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private const int playerUnitNum = 4;
    private int thisTurnPlayer = 0; // 0 ������ ����
    private int maxPlayerNum = 2;

    private int selectedUnitNum = 0; //0 ~ 3
    public int SelectedUnitNum { get => selectedUnitNum; }
    private int turnNum = 0;
    public int TurnNum { get => turnNum; }

    Dictionary<int, List<Player>> unitList = new Dictionary<int, List<Player>>(); //ũ�� �ø����� ���� list�̿�, �迭
    //List<List<Player>> playerUnitList = new List<List<Player>>();

    List<int> movementList = new List<int>(); //�������� ����� �޼ҵ� �߰�

    [SerializeField] Places placeContainer; //attach
    [SerializeField] Player[] playerPrefabs; //attach


    private int steps;


    private void Awake()
    {
        if(null == instance)
{
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("Game Start");

        playerUnitAdd();

    }

    void playerUnitAdd()
    {   
        playerPrefabs = Resources.LoadAll<Player>("Prefabs/Players");

        int count = playerPrefabs.Length;// Mathf.Min(playerPrefabs.Length, 2); �����÷��̾ �߰��Ѵ��ϸ� 2�� ������ �����
        for(int i = 0; count > i; i++) //�÷��̾��
        {
            for (int l = 0; l < playerUnitNum; l++) //���ּ�
            {
                Debug.Log("player" + i);

                Player player = Instantiate(playerPrefabs[i]);
                player.unitNumbering(i, l);

                if (!unitList.ContainsKey(i))
                {
                    unitList.Add(i, new List<Player>());
                }

                unitList[i].Add(player);
            }
        }       
        
     }

    public Place FindPlaceToIndex(int num)
    {
        int size = placeContainer.Place.Length;

        if (num < size)
            return placeContainer.Place[num];
        
        return placeContainer.Place[size - 1]; //������
    }

    public Place FindStandingPlace(Vector3 position)
    {
        return placeContainer.FindPlayerPlace(position);
    }


    public void ClickedUnitControl(int controlUnitNum)
    {
        selectedUnitNum = controlUnitNum; //find control unit

        Vector3 clickedUnitPos = unitList[thisTurnPlayer][controlUnitNum].transform.position; //Ŭ���� �÷��̾ �ִ� ��ġ
        Place basePlace = placeContainer.FindPlayerPlace(clickedUnitPos); //Searching place
        Place tmpPlace = basePlace;

    }

   
    public void UnitStepSelection(Place selectedPlace) 
    {

        //steps = selectedPlace.remainSteps;//��ȯ�� ���� ���� �־��־����
        Player unit = unitList[thisTurnPlayer][selectedUnitNum];

        if (unit.playerState == PLAYERSTATE.WATING)
            unit.ChangeUnitState(PLAYERSTATE.PLAY);

        unit.Movement(steps); //unit move      

              
    }


    private void Update() 
    {  



    }


}


