using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject Gun ;

    Transform player;
    List<Vector2> gunPositions = new List<Vector2>();

    int spawnedGuns = 0;

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        gunPositions.Add(new Vector2(0.8f, 0.5f));
        gunPositions.Add(new Vector2(1f, -0.38f));
        gunPositions.Add(new Vector2(0.8f, -1.24f));
        
        gunPositions.Add(new Vector2(-1.8f, 0.5f));
        gunPositions.Add(new Vector2(-2f, -0.38f));
        gunPositions.Add(new Vector2(-1.8f, -1.24f));

        AddGun();
        AddGun();

    }

    private void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.G))
        { 
            if (spawnedGuns < 6)
            {
                AddGun();
            }
           
        }
           
    }

    void AddGun()
    {
        var pos = gunPositions[spawnedGuns];

        var newGun = Instantiate(Gun, pos, Quaternion.identity);

        newGun.GetComponent<Gun>().SetOffset(pos);
        spawnedGuns++;
    }

}
