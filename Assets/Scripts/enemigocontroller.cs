using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigocontroller : MonoBehaviour
{
    public float velocidadenemigo = 1f;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), velocidadenemigo * Time.deltaTime); 
    }
}
