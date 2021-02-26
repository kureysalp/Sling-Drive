using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public delegate void EnterCorner(GameObject barrel);
    public static event EnterCorner EnteredCorner;

    public delegate void ExitCorner();
    public static event ExitCorner ExitedCorner;

    PlayerController pc;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Corner"))
            EnteredCorner?.Invoke(other.transform.Find("Hook_Barrel").gameObject);

        if (other.CompareTag("Level Pass"))        
            EventManager.PassLevel();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Straight"))        
            pc.AlignCarDirection(other.transform.forward);                    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Corner"))        
            ExitedCorner?.Invoke();

        if (other.CompareTag("Level Exit"))
            EventManager.ExitLevel();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Border"))
            EventManager.FailGame();

    }
}
