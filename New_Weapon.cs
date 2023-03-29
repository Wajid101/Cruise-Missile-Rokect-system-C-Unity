using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class New_Weapon : MonoBehaviour
{
    public string[] TargetTag = new string[1] { "E" };

    
    public Rigidbody Messaile;
    public List<Rigidbody> Roketlist;
    public int bcount = 2;

    public GameObject closest;
    public GameObject[] gos;
    public GameObject[] gos11;
    public GameObject[] gos1;

    //public List<GameObject> gos;
    public float rangeOfEncounter = 11000, dely_time_fire;
    float Nextfire = 0;
    public Transform[] barrels;
    public AudioSource LanchAduio;



    private void Start()
    {
        Roketlist = new List<Rigidbody>();
        for (int ii = 0; ii < bcount; ii++)
        {
            //Debug.Log("call");
            Rigidbody bomb = (Rigidbody)Instantiate(Messaile);
            bomb.gameObject.SetActive(false);
            Roketlist.Add(bomb);
            Roketlist[ii].GetComponent<MoverMissile>().instance = this;
        }
    }

    private void Update()
    {
        ClosestObj();

     
        if (closest != null)
        {
            if (Time.time >= Nextfire)
            {
                Nextfire = Time.time + dely_time_fire;
                
                Fire();

            }
        }



    }


    GameObject ClosestObj()
    {
        if (TargetTag.Length > 1)
        {
            gos = GameObject.FindGameObjectsWithTag(TargetTag[0]);
            gos11 = GameObject.FindGameObjectsWithTag(TargetTag[1]);
            gos1 = gos.Concat(gos11).ToArray();

        }
        else
        {
            gos = GameObject.FindGameObjectsWithTag(TargetTag[0]);
            gos1 = gos.ToArray();

        }


        closest = null;
        float distance = rangeOfEncounter;
        Vector3 position = transform.position;
        foreach (GameObject go in gos1)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

       

        return closest;



    }





    public void Fire()
    {

        for (int i = 0; i < Roketlist.Count; i++)
        {


            if (!Roketlist[i].gameObject.activeInHierarchy)
            {

                Roketlist[i].transform.position = barrels[i].position;
                Roketlist[i].transform.rotation = barrels[i].rotation;
                LanchAduio.Play();
                Roketlist[i].gameObject.SetActive(true);
                break;

            }
        }
    }


}


