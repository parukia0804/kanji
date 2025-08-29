using UnityEngine;

public class Button : MonoBehaviour
{
    public int buttonNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonNumber = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
