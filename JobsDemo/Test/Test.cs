using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int count = 5000;
    public float radius = 100;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        float randomValue(float r) => Random.Range(-r, r);

        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(prefab, transform);
            go.transform.localPosition = new Vector3(randomValue(radius), randomValue(radius), randomValue(radius));
            //go.GetComponent<MoverJob>().speed = Random.Range(1, 20);
            go.GetComponent<SimpleMover>().speed = Random.Range(1, 20);
            //go.GetComponent<Mover>().speed = Random.Range(1, 20);
        }
        GetComponentInParent<SimpleMoverSystem>().OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
