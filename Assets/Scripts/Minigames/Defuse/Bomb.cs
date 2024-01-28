using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public DefuseController controller;
    GameObject fire;
    public bool ignited;
    // Start is called before the first frame update
    void Start()
    {
        controller=FindObjectOfType<DefuseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ignite()
    {
        ignited = true;
        fire=Instantiate(controller.firePrefab,transform.position+new Vector3(0,controller.fireOffset,0), Quaternion.identity);
        StartCoroutine(IgnitionRoutine());
    }
    public void Defuse()
    {
        if (ignited)
        {
            controller.bombs.Remove(this);
            controller.bombAmount--;
            ignited = false;
            Destroy(fire);
            Destroy(gameObject);
        }
    }
    IEnumerator IgnitionRoutine()
    {
        yield return new WaitForSeconds(controller.bombIgnitionTime);
        controller.controller.Lose();
    }
}
