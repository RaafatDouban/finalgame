using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject boltPrefab;
    public Transform fireSpwan;
    public float fireSpawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(boltSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator boltSpawn()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return new WaitForSeconds(fireSpawnInterval);
            //GameObject obj = Instantiate(boltPrefab, fireSpwan.position, fireSpwan.rotation);
            GameObject obj = BulletPooler.current.GetPooledObject();
            obj.transform.position = fireSpwan.position;
            obj.GetComponent<Bullet>().strength = 5f;
            obj.SetActive(true);

        }


    }
}
