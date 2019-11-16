using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;
using System.Threading.Tasks;

public enum Item
{
    Tumeric = 0,
    Saffron = 1,
    Cardamom = 2,
    Cinnamon = 3,
    Clove = 4,
    Pepper = 5,
    Sumac = 6
}

public class Player : MonoBehaviour
{
    public static Text inventoryTxt;
    public static Text caravanTxt;
    NavMeshAgent agent;
    public int[] inventory;
    public int[] caravan;
    public List<int> plan = new List<int>();
    bool count = false;
    float time = 0;

    public static List<List<int>> presets;

    void Start()
    {
        presets = new List<List<int>>
        {
            new List<int>{0 },  //get two tumeric
            new List<int>{0,1 },    //get a saffron
            new List<int>{0,1,0,1,2},   //get a cardamom
            new List<int>{0,3,0,3},   //get a cinnamon
            new List<int>{0,1,0,1,2,0,4},   //get a clove
            new List<int>{0,3,0,3,0,1,0,5},   //get a pepper
            new List<int>{0,1,0,1,2,0,4,0,1,0,1,2,0,2},   //get a pepper
        };
        inventory = new int[7];
        caravan = new int[7];
        inventoryTxt = GameObject.Find("inventoryTxt").GetComponent<Text>();
        caravanTxt = GameObject.Find("caravanTxt").GetComponent<Text>();
        agent = GetComponent<NavMeshAgent>();
        nextsteps();
        
    }
    void Update()
    {
        if (count && (time += Time.deltaTime) > 0.5)
        {
            count = false;
            agent.isStopped = false;
        }
        inventoryTxt.text = "inventory:\n";
        for (int i = 0; i < inventory.Length; i++)
        {
            inventoryTxt.text += $"{(Item)i} {inventory[i]}\n";
        }
        caravanTxt.text = "caravan:\n";
        for (int i = 0; i < inventory.Length; i++)
        {
            caravanTxt.text += $"{(Item)i} {caravan[i]}\n";
        }

    }
    void nextsteps()
    {
        plan = new List<int>();
        int next = 0;
        for (int i = 0; i < presets.Count(); i++)
        {
            if (caravan[i] < 2)
            {
                next = i;
                break;
            }
        }
        if (next!=presets.Count()-1)
        {
            for (int i = 0; i < presets[next].Count(); i++)
            {
                plan.Add(presets[next][i]);
            }
        }
        else
        {
            caravan[1]--;
            caravan[3]--;
            caravan[4]--;
            inventory[1]++;
            inventory[3]++;
            inventory[4]++;
            plan.Add(7);
        }
        agent.SetDestination(Main.traders.First(x=>x.n==plan.First()).getposition());
        plan.RemoveAt(0);
        Debug.Log("removed next is 0");
    }
    public void OnCollisionEnter(Collision collision)
    {
        Trader t = null;
        if (!count&&(t = Main.traders.FirstOrDefault(x=>x.gameObject == collision.other.gameObject))!=null)
        {
            count = true;
            agent.isStopped = true;
            time = 0;
            inventory = t.trade(inventory);
            if (plan.Count()>=1)
            {
                Debug.Log($"nextposition {plan.First()}");
                agent.SetDestination(Main.traders.First(x => x.n == plan.First()).getposition());
                plan.Remove(plan.First());
            }
            else
            {
                agent.SetDestination(new Vector3(0, 0, 0));
            }
        }
        if (!count && collision.other.name.Equals("Caravan"))
        {
            Debug.Log("to caravan");
            for (int i = 0; i < caravan.Length; i++)
            {
                caravan[i] += inventory[i];
                inventory[i] = 0;
            }
            nextsteps();
        }
        
    }

    /// <summary>
    /// evaluate heuristic score of current state
    /// higher is better
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    int distanceToGoal(int[] state)
    {
        int d = 0;
        for (int i = 0; i < state.Length; i++)
        {
            d += Mathf.Min(state[i]*i, 2*i);
        }
        return d;
    }
}
