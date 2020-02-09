using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShipManager : MonoBehaviour
{
    float timeLeft;
    public float maxTime;

    [System.Serializable]
    public class CartHolder
    {
        public string key;
        public List<CinemachineDollyCart> cart;
    }

    [SerializeField]
    CartHolder[] holders;

    [SerializeField]
    Dictionary<string, List<CinemachineDollyCart>> carts = new Dictionary<string, List<CinemachineDollyCart>>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(CartHolder holder in holders)
        {
            carts.Add(holder.key, holder.cart);

            foreach (CinemachineDollyCart c in holder.cart)
            {
                c.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        if(timeLeft >= maxTime)
        {
            int random = Random.Range(0, carts.Count);
            string k = "";

            switch(random)
            {
                case 0:
                    k = "Squadron";
                    break;
                case 1:
                    k = "Fighter";
                    break;
                case 2:
                    k = "Goods";
                    break;
                case 3:
                    k = "Transport";
                    break;
                case 4:
                    k = "SquadBack";
                    break;
                case 5:
                    k = "TransportBack";
                    break;
            }

            TriggerTrack(k);
            timeLeft = 0;
        }
    }

    void TriggerTrack(string key)
    {
        foreach(CinemachineDollyCart cart in carts[key])
        {
            cart.enabled = true;
            cart.m_Position = 0;
        }
    }
}
