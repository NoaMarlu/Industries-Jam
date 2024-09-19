using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Item
{
    Back,
    Up,
    Down,

    None,
}


public class ItemSpawn : MonoBehaviour
{
    //�A�C�e���̃v���n�u
    public GameObject BackBoosterPrefab;
    public GameObject UpBoosterPrefab;
    public GameObject DownBoosterPrefab;

    //�X�|�[������܂ł̎���
    [SerializeField] private float spawnInterval;
    [SerializeField] private float moveSpeed;//�A�C�e�����ړ����鑬�x

    [SerializeField] private float spawnDistance;//�v���C���[�ǂꂾ�����ꂽ�ʒu����o�����邩
    [SerializeField] private float spawnRange;   //�A�C�e���̉������̃����_�� 

    [SerializeField] private int maxspawn;   //�A�C�e���̍ő�X�|�[����
    [SerializeField] private int minspawn;   //�A�C�e���̍ŏ��X�|�[���� 
    private float spawntime = 0;
    private Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);
    void Update()
    {
        spawntime += Time.deltaTime;

        if(spawntime > spawnInterval)
        {
            //�A�C�e���o��
            SpawnItem();
            spawntime = 0.0f;
        }

    }

    //�A�C�e���̃����_���o���p
    void SpawnItem()
    {
        int randomcount = Random.Range(minspawn, maxspawn);
        for (int i = 0; i < randomcount; i++)
        {
            float randomY = Random.Range(-4.5f, 4.5f);
            Vector3 SpawnPosition = new Vector3(
                                                spawnDistance + Random.Range(-spawnRange, spawnRange),
                                                randomY,
                                                PlayerScript.instance.transform.position.z);

            int spawntype = Random.Range((int)Item.Back, (int)Item.Down);


            switch (spawntype)
            {
                case (int)Item.Back:
                    GameObject newBackBooster = Instantiate(BackBoosterPrefab, SpawnPosition, spawnRotation);
                    BackBooster backBoosterScript = newBackBooster.GetComponent<BackBooster>();
                    backBoosterScript.SetMoveSpeed(moveSpeed);
                    break;
                case (int)Item.Up:
                    GameObject newUpBooster = Instantiate(UpBoosterPrefab, SpawnPosition, spawnRotation);
                    UpBooster upBoosterScript = newUpBooster.GetComponent<UpBooster>();
                    upBoosterScript.SetMoveSpeed(moveSpeed);
                    break;
                case (int)Item.Down:
                    GameObject newDownBooster = Instantiate(DownBoosterPrefab, SpawnPosition, spawnRotation);
                    DownBooster downBoosterScript = newDownBooster.GetComponent<DownBooster>();
                    downBoosterScript.SetMoveSpeed(moveSpeed);
                    break;
            }
        }
    }

}
