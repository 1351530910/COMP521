using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Thief : MonoBehaviour
{
    public static NavMeshAgent agent;
    float time;
    int status; //wait, to player, to caravan
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        time = 0;
        status = 0;
        int xsign = Random.Range(0, 2) * 2 - 1;
        int ysign = Random.Range(0, 2) * 2 - 1;
        gameObject.transform.position = new Vector3(-2.3f*xsign, 0, -2.3f*ysign);
    }

    // Update is called once per frame
    void Update()
    {
        if (status == 1)
        {
            agent.SetDestination(Player.agent.gameObject.transform.position);
        }
        if (status == 0 )
        {
            time += Time.deltaTime;
            if(agent.remainingDistance<0.2f)
                agent.SetDestination(new Vector3(-2.3f * (Random.Range(0, 2) * 2 - 1), 0, -2.3f * (Random.Range(0, 2) * 2 - 1)));
            if (time > 5.0) action();
        }
    }

    void action()
    {
        agent.isStopped = false;
        if (Random.Range(0f, 1.0f) < 0.33f)
        {
            if (Random.Range(0f, 1.0f) < 0.5f)
            {
                status = 2;
                agent.SetDestination(new Vector3(0, 0, 0));
            }
            else
            {
                status = 1;
            }
        }
        else
        {
            time = 0;
        }
        
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        switch (status)
        {
            case 1:
                if (collision.other.gameObject.name.Equals("PlayerAgent"))
                {
                    status = 0;
                    time = 0;
                    while (Player.inventory.Sum()>0)
                    {
                        int index = Random.Range(0, Player.inventory.Length);
                        if (Player.inventory[index] > 0)
                        {
                            Player.inventory[index]--;
                            agent.SetDestination(new Vector3(-2.3f, 0, -2.3f));
                            Player.plan.Clear();
                            Player.agent.destination = new Vector3(0, 0, 0);
                            Player.trade = true;
                            return;
                        }
                    }
                }
                break;
            case 2:
                if (collision.other.gameObject.name.Equals("Caravan"))
                {
                    
                    time = 0;
                    while (Player.caravan.Sum() > 0)
                    {
                        int index = Random.Range(0, Player.caravan.Length);
                        if (Player.caravan[index] > 0)
                        {
                            Player.caravan[index]--;
                            agent.SetDestination(new Vector3(-2.3f, 0, -2.3f));
                            status = 0;
                            return;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
}
