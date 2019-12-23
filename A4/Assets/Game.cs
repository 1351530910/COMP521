using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //size of eat area
    public const int w = 7, h = 3;
    /// <summary>
    /// 0 available
    /// 1 unavailable
    /// </summary>
    public static int[,] map = new int[w, h];

    //static gameobjects
    public static List<GameObject> planters = new List<GameObject>();
    public static List<GameObject> seats = new List<GameObject>();
    public static List<GameObject> tables = new List<GameObject>();
    public static List<GameObject> shops = new List<GameObject>();
    public static List<GameObject> advertisers = new List<GameObject>();

    //prefabs
    public static GameObject flyer;
    public static GameObject customer;
    public static GameObject shop;
    public static GameObject planter;
    public static GameObject table;
    public static GameObject adv;
    //state of seats
    public static Dictionary<GameObject, navigator> occupy = new Dictionary<GameObject, navigator>();

    //
    public static float t = 0;

    //spawn rate and number of advertisers
    public static Slider spawnrate;
    public static Slider nadv;

    [RuntimeInitializeOnLoadMethod]
    public static void init()
    {
        //find static gameobjects
        spawnrate = GameObject.Find("spawnrate").GetComponent<Slider>();
        nadv = GameObject.Find("nadv").GetComponent<Slider>();

        //find prefabs
        customer = GameObject.Find("Customer");
        shop = GameObject.Find("shop");
        planter = GameObject.Find("planter");
        table = GameObject.Find("t");
        adv = GameObject.Find("Advertiser");
        flyer = GameObject.Find("flyer");

        //disable prefabs
        customer.SetActive(false);
        planter.SetActive(false);
        shop.SetActive(false);
        table.SetActive(false);
        adv.SetActive(false);
        flyer.SetActive(false);

        //create shops
        for (int i = 0; i < 17; i++)
        {
            GameObject obj = Instantiate(shop);
            obj.SetActive(true);
            obj.name = "shop";
            obj.transform.position = new Vector3(i - 8, 0, 4);
            shops.Add(obj);
            obj = Instantiate(shop);
            obj.SetActive(true);
            obj.name = "shop";
            obj.transform.position = new Vector3(i - 8, 0, -4);
            shops.Add(obj);
        }
        Destroy(shop);

        //2-5 planters
        int nplanter = Random.Range(2, 6);
        for (int i = 0; i < nplanter; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);
            while (map[x, y] != 0 || x % 2 == 0 || y == h / 2)
            {
                x = Random.Range(0, w);
                y = Random.Range(0, h);
            }
            map[x, y] = 1;
            GameObject obj = Instantiate(planter);
            obj.SetActive(true);
            obj.name = "planter";
            obj.transform.position = new Vector3(x - w / 2, 0, y - h / 2);
            obj.transform.localScale = new Vector3(Random.Range(0.4f, 0.7f), 1, Random.Range(0.4f, 0.7f));
            planters.Add(obj);
        }

        //3-4 tables
        int ntables = Random.Range(3, 5);
        for (int i = 0; i < ntables; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);
            while (map[x, y] != 0 || y == h / 2)
            {
                x = Random.Range(0, w);
                y = Random.Range(0, h);
            }
            map[x, y] = 1;
            GameObject obj = Instantiate(table);
            obj.SetActive(true);
            obj.name = "t";
            obj.transform.position = new Vector3(x - w / 2, 0, y - h / 2);
            obj.transform.Rotate(new Vector3(0, Random.Range(0, 90), 0));
            tables.Add(obj);
        }

        //create advertisers
        for (int i = 0; i < nadv.maxValue; i++)
        {
            var a = Instantiate(adv);
            Destroy(a.GetComponent<advertiser>());
            a.AddComponent<advertiser>();
            advertisers.Add(a);
            a.SetActive(true);
        }

        //build seat map
        seats = FindObjectsOfType<GameObject>().Where(x => x.name == "seat").ToList();
        foreach (var s in seats)
        {
            occupy.Add(s, null);
        }
        
        //create a customer and start
        Instantiate(customer).SetActive(true);
    }
    public void addadvertisers()
    {
        //control number of advertisers on scene
        for (int i = 0; i < nadv.maxValue; i++)
        {
            if (i<nadv.value)
            {
                advertisers[i].SetActive(true);
            }
            else
            {
                advertisers[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //spawn customer by fixed rate
        if (spawnrate.value>0.1&&(t += Time.deltaTime) > 1/spawnrate.value)
        {
            t = 0;
            Instantiate(customer).SetActive(true);
        }
    }

}
