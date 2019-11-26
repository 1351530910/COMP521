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
    public static NavMeshAgent agent;
    public static int[] inventory;
    public static int[] caravan;
    public static List<int> plan = new List<int>();
    public static bool count = false;
    public static float time = 0;
    public static Trader target;
    public static bool trade;
    public static List<List<int>> presets;
    float timescale;
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
        trade = false;
        nextsteps();
    }
    void Update()
    {
        if (inventory.Sum() == 0 && agent.destination.sqrMagnitude < 0.1f)
        {
            Debug.Log("next");
            nextsteps();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Time.timeScale>0.1)
            {
                timescale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = timescale;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Plus)|| Input.GetKeyUp(KeyCode.UpArrow))
        {
            Time.timeScale *= 2;
        }
        if (Input.GetKeyUp(KeyCode.Minus) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            Time.timeScale /= 2;
        }
        if (count && (time += Time.deltaTime) > 0.5f)
        {
            time = 0;
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
        inventoryTxt.text += "\nplan\n";
        for (int i = 0; i < plan.Count(); i++)
        {
            inventoryTxt.text += $"trade with {plan[i]+1}\n";
        }
        inventoryTxt.text += "go to caravan";



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
    }
    public void OnCollisionEnter(Collision collision)
    {
        Trader t = null;
        if (!count&&(t = Main.traders.FirstOrDefault(x=>x.gameObject == collision.other.gameObject))!=null)
        {
            count = true;
            agent.isStopped= true;
            time = 0;
            
            target = t;
            inventory = target.trade(inventory);
            if (plan.Count()>1)
            {
                plan.Remove(plan.First());
                agent.SetDestination(Main.traders.First(x => x.n == plan.First()).getposition());
            }
            else
            {
                plan.Clear();
                agent.SetDestination(new Vector3(0, 0, 0));
                trade = true;
            }
        }
        if (!count && collision.other.name.Equals("Caravan")&&trade)
        {
            
            for (int i = 0; i < caravan.Length; i++)
            {
                caravan[i] += inventory[i];
                inventory[i] = 0;
            }
            trade = false;
        }
        
    }

}
