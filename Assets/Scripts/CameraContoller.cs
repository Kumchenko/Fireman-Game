using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        pos.z = -10f;
    }

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }
}
