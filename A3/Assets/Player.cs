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
    NavMeshAgent agent;
    public int[] inventory;
    public int[] caravan;
    public List<Vector3> plan = new List<Vector3>();
    bool count = false;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new int[7];
        caravan = new int[7];
        inventoryTxt = GameObject.Find("inventoryTxt").GetComponent<Text>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Main.traders[0].getposition());
    }
    void Update()
    {
        if (count && (time += Time.deltaTime) > 0.5)
        {
            count = false;
            agent.SetDestination(Main.traders[2].getposition());
            agent.isStopped = false;
        }
        inventoryTxt.text = "inventory:\n";
        for (int i = 0; i < inventory.Length; i++)
        {
            inventoryTxt.text += $"{inventory[i]}\n";
        }
    }
    void nextsteps()
    {

    }
    public void OnCollisionEnter(Collision collision)
    {
        Trader t = null;
        if (!count&&(t = Main.traders.FirstOrDefault(x=>x.gameObject == collision.other.gameObject))!=null)
        {
            agent.isStopped = true;
            time = 0;
            count = true;
            int[] i = (int[])inventory.Clone();
            inventory = t.trade(inventory);
        }
        if (collision.other.name.Equals("caravan"))
        {
            for (int i = 0; i < caravan.Length; i++)
            {
                caravan[i] += inventory[i];
                inventory[i] = 0;
            }
        }
    }

    /// <summary>
    /// evaluate heuristic distance to goal state
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    float distanceToGoal(int[] state)
    {
        int d = inventory.Length*2;
        for (int i = 0; i < state.Length; i++)
            d -= Mathf.Max(state[i], 2);
        return d;
    }
}
