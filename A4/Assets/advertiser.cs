using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class advertiser : MonoBehaviour
{
    static readonly Vector3[] destinations =
    {
        new Vector3(-7.25f, 0, 2.35f),
        new Vector3(7.25f, 0, 2.35f),
        new Vector3(-7.25f, 0, -2.35f),
        new Vector3(7.25f, 0, -2.35f),
        new Vector3(-6.39f, 0, 0f),
        new Vector3(6.39f, 0, 0f),
        new Vector3(-3.16f, 0, 2.84f),
        new Vector3(3.16f, 0, 2.84f),
        new Vector3(-3.16f, 0, -2.84f),
        new Vector3(3.16f, 0, -2.84f),
    };
    Vector3 dest { get; set; }  //current destination
    public GameObject target;   //advertise target
    public Vector3 direction = new Vector3(0, 0, 0);    //sum of steering forces
    const float velocity = 2.5f;    //velocity
    public int count { get; set; }  //number of person convinced

    //time counters 
    public float cumtime;
    const float targettime = 4;
    public float seektime;
    public float mag;

    const float flee = 0.2f;    //global flee force from other advertisers
    public static Slider probability;   //put a flyer
    public static Slider interval;      //put a flyer
    public static Slider pitchdistance; //distance to a person to stay inside

    void Start()
    {
        probability = GameObject.Find("flyerprob").GetComponent<Slider>();
        interval = GameObject.Find("flyerinterval").GetComponent<Slider>();
        pitchdistance = GameObject.Find("pitchd").GetComponent<Slider>();
        transform.position = destinations[Random.Range(0, destinations.Length)];
        dest = destinations[Random.Range(0, destinations.Length)];
        target = null;
    }
    void Update()
    {
        //wandering
        if (target==null)
        {
            cumtime = 0;
            //number of ppl who has taken flyer
            if (Flyer.available.Count > 0)
            {
                target = Flyer.available[Random.Range(0,Flyer.available.Count)];
            }
            //seek force to the target location
            if ((transform.position - dest).sqrMagnitude < 1)
            {
                dest = destinations[Random.Range(0, destinations.Length)];
            }
            //compute next location
            transform.position += (direction + (dest - transform.position).normalized).normalized * velocity * Time.deltaTime;
        }
        else
        {
            //if target is in range
            if ((mag = (target.transform.position - transform.position).magnitude) < pitchdistance.value)
            {
                //count time and disable other advertisers to that target (may cause concurrency)
                cumtime += Time.deltaTime;
                GameObject t = target;
                foreach (var adv in Game.advertisers)
                {
                    if (adv.GetComponent<advertiser>().target==target)
                    {
                        adv.GetComponent<advertiser>().target = null;
                    }
                }
                target = t;

                //follow the target without collision
                if ((target.transform.position - transform.position).magnitude > 0.1)
                {
                    transform.position += (target.transform.position - transform.position).normalized * velocity * Time.deltaTime;
                }

                //check if 4 sec reached
                if (cumtime > targettime)
                {
                    cumtime = 0;
                    seektime = 0;
                    target.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                    target = null;
                    if (++count == 3)
                    {
                        reset();
                    }
                }

                //remove target from available list
                if (Flyer.available.Contains(target))
                {
                    Flyer.available.Remove(target);
                }
                
            }
            else
            {
                //rush to that target before other advertiser
                seektime += Time.deltaTime;
                //5 sec is timeout
                if (seektime > 5)
                {
                    target = null;
                    seektime = 0;
                }
                //seek steering force
                transform.position += (target.transform.position - transform.position).normalized * velocity * Time.deltaTime;
            }
        }
        //reset direction for next frame
        direction = new Vector3(0, 0, 0);
        //global freeing steering force to other advertisers
        foreach (var adv in Game.advertisers)
        {
            direction -=  (adv.transform.position - transform.position);
        }
        direction = direction.normalized*flee;
    }
    void reset()
    {
        transform.position = destinations[Random.Range(0, destinations.Length)];
        dest = destinations[Random.Range(0, destinations.Length)];
        direction = new Vector3(0, 0, 0);
        target = null;
        cumtime = 0;
        count = 0;
        seektime = 0;
    }

    float flyertime = 0;
    void FixedUpdate()
    {
        //put a flyer by fixed interval
        if ((flyertime+=Time.fixedDeltaTime)>interval.value)
        {
            flyertime = 0;
            if (Random.Range(0f,1f)<probability.value)
            {
                var flyer = Instantiate(Game.flyer);
                flyer.SetActive(true);
                flyer.transform.position = gameObject.transform.position;
            }
        }
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        //avoid stepping in something
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            transform.position -= Time.deltaTime * 10 * (contact.otherCollider.gameObject.transform.position - transform.position);
        }
    }
}
