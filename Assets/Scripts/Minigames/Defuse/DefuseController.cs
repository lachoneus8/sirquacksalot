using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DefuseController : MonoBehaviour
{
    public MinigameController controller;
    public Transform playerPos;

    public float bombDelay;
    float bombTime;
    public float bombIgnitionTime;
    public int bombAmount;
    public float bombDist;
    public int shotsLeft;
    int startingShots;
    public List<Bomb> bombs;
    public Bomb bombPrefab;
    public GameObject firePrefab;
    public float fireOffset;
    public Slider waterSlider;
    public GameObject waterPrefab;
    public Transform waterAnchor;
    // Start is called before the first frame update
    void Start()
    {
        bombs=new List<Bomb>();
        startingShots = shotsLeft;
        for(int i = 0; i < bombAmount; i++)
        {
            transform.localEulerAngles= new Vector3 (0,((float)i/(float)bombAmount)*360,0);
            Bomb newBomb=Instantiate<Bomb>(bombPrefab,transform.position+(transform.forward*bombDist),transform.rotation);
            newBomb.ignited = false;
            bombs.Add(newBomb);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.running)
        {
            waterSlider.gameObject.SetActive(true);
            var mousePoint=Input.mousePosition;
            var lookRay=Camera.main.ScreenPointToRay(mousePoint,Camera.MonoOrStereoscopicEye.Mono);
            RaycastHit lookHit;
            Physics.Raycast(lookRay, out lookHit);
            var lookPos = lookHit.point;
            lookPos.y=playerPos.transform.position.y;
            playerPos.LookAt(lookPos);

            if (Input.GetMouseButtonDown(0)&&shotsLeft>0)
            {
                RaycastHit waterGunHit;
                shotsLeft--;
                waterSlider.value = (float)shotsLeft / (float)startingShots;
                StartCoroutine(SpawnWater(waterAnchor.position));
                if (Physics.Raycast(playerPos.position, playerPos.forward, out waterGunHit))
                {
                    if(waterGunHit.rigidbody!=null)
                    {
                        var hitBomb = waterGunHit.rigidbody.GetComponent<Bomb>();
                        if (hitBomb != null)
                        {
                            hitBomb.Defuse();
                            if (bombAmount <= 0)
                            {
                                controller.Win();
                                waterSlider.gameObject.SetActive(false);
                            }
                            StartCoroutine(SpawnWater(waterGunHit.point));
                        }
                    }
                }
            }
            bombTime += Time.deltaTime;
            if (bombTime >= bombDelay)
            {
                int index=Random.Range(0,bombs.Count);
                bombs[index].Ignite();
                bombTime = 0;
            }
        }
    }
    IEnumerator SpawnWater(Vector3 pos)
    {
        GameObject water=Instantiate(waterPrefab, pos, playerPos.rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(water);
    }
}
