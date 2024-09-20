using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollider : MonoBehaviour
{
    public GameObject player;
    public GameObject Item;
    public GameObject ItemLeft;
    public GameObject ItemRight;
    public GameObject obstale;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerScript.instance != null)
        {
            player = PlayerScript.instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameObject spawnedItem = null; // �������ꂽ�A�C�e���̎Q�Ƃ�ۑ�

            if (Random.Range(0, 3) == 0)
            {

                spawnedItem= Instantiate(Item);
               // Item.transform.position= gameObject.transform.position;
            }
            else if (Random.Range(0, 3) == 1)
            {
                spawnedItem= Instantiate(ItemLeft);
               // ItemLeft.transform.position = gameObject.transform.position;
            }
            else if (Random.Range(0, 3) == 2)
            {
                spawnedItem= Instantiate(ItemRight);
               // ItemRight.transform.position = gameObject.transform.position;
            }

            // �������ꂽ�A�C�e�������݂���΁A���̈ʒu���v���C���[�̈ʒu�ɐݒ�
            if (spawnedItem != null)
            {
                spawnedItem.transform.position = player.transform.position;
            }

            Destroy(gameObject);
            
            
        }

    }
}
