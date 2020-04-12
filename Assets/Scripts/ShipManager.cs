using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Controls menu ships
public class ShipManager : MonoBehaviour
{
    float timeLeft; // Timer
    public float maxTime;

    [System.Serializable] // A container class
    public class CartHolder
    {
        public string key;
        public List<CinemachineDollyCart> cart;
    }

    [SerializeField] // Array of carts
    CartHolder[] holders;

    [SerializeField] // Dictionary of a string to a list
    Dictionary<string, List<CinemachineDollyCart>> carts = new Dictionary<string, List<CinemachineDollyCart>>();

    // Start is called before the first frame update
    void Start()
    {
        // Init
        foreach(CartHolder holder in holders)
        {
            carts.Add(holder.key, holder.cart); // Add a cart to a dictionary

            foreach (CinemachineDollyCart c in holder.cart)
            {
                c.enabled = false; // Disable all carts initially
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime; // If timer
        if(timeLeft >= maxTime)
        {
            int random = Random.Range(0, carts.Count); // Enable random ship(s)
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
            timeLeft = 0; // Reset timer
        }
    }

    void TriggerTrack(string key) // Enable all Cinemachine carts associated with a key
    {
        foreach(CinemachineDollyCart cart in carts[key])
        {
            cart.enabled = true;
            cart.m_Position = 0;
        }
    }
}
