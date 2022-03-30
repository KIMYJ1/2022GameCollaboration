using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private List<Place> prePlace = new List<Place>();
    [SerializeField] private List<Place> postPlace = new List<Place>();

    public Place[] PrePlace { get { return postPlace.ToArray(); } }
    public Place[] PostPlace { get { return postPlace.ToArray(); } }

    //public int remainSteps { get; private set; }



    private void Start()
    {
        Debug.Log("preplace count : " + prePlace.Count);

        for (int i = 0; i < prePlace.Count; i++)
        {
            prePlace[i].AddNextPos(this);
            Debug.Log("Add Place");
        }
    }

    public void AddNextPos(Place place)
    {
        postPlace.Add(place);       
    }

    //public void NoticeControl(bool activate) //with boxcollider
    //{
    //    if (0 < transform.childCount)
    //    {
    //        transform.GetChild(0).gameObject.SetActive(activate); //place��  �¿����� notice�� �����ϴ��� �ؾ���
    //        gameObject.GetComponent<BoxCollider>().enabled = activate;
    //    }
    //}

    //public void GainRemainSteps(int steps)
    //{
    //    remainSteps = steps;
    //}

    private void OnMouseUpAsButton() //�߰��Ұ�
    {
        Debug.Log("place is clicked");
        GameManager.instance.UnitStepSelection(this);
        
    }

}
