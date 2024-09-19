using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    public Sprite usualSprite; // �摜�iSprite�j
    public float spawnInterval = 3.0f; // ��莞�Ԃ��Ƃ̃X�|�[���Ԋu
    public float minSpawnInterval = 3.0f; // �X�|�[���Ԋu�̍ŏ��l
    public float maxSpawnInterval = 5.0f; // �X�|�[���Ԋu�̍ő�l
    public float speed = 2.0f; // �ړ����x
    public float speedIncreaseRate = 0.1f; // ���Ԍo�߂��Ƃ̃X�s�[�h������
    private float currentSpawnInterval; // ���݂̃X�|�[���Ԋu
    private float timeElapsed = 0.0f; // �o�ߎ���
     //�X�|�[������ꏊ
    public float spawnX = 10.0f;
    //���R����
    [SerializeField]
    private float DestroyX = -20.0f;
    void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnObject", currentSpawnInterval); // �����_���ȊԊu�ŃX�|�[���J�n
    }
    void Update()
    {
        // ���Ԍo�߂ɉ����ăX�s�[�h�𑝉�
        timeElapsed += Time.deltaTime;
        speed += speedIncreaseRate * Time.deltaTime;
    }
    void SpawnObject()
    {
        GameObject newObject = new GameObject("UsualObject");
        // �q�I�u�W�F�N�g�Ƃ��ăX�v���C�g�\���������쐬
        GameObject spriteObject = new GameObject("SpriteObject");
        spriteObject.transform.parent = newObject.transform; // �e�I�u�W�F�N�g�ɐݒ�

        // SpriteRenderer���q�I�u�W�F�N�g�ɒǉ�
        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = usualSprite; // �摜��K�p

        // ��ʂ̉E�[���琶���iY���W�̓����_���j
        newObject.transform.position = new Vector3(spawnX, Random.Range(-4.0f, 4.0f), 0);

        newObject.AddComponent<Rigidbody2D>().gravityScale = 0;
        newObject.AddComponent<BoxCollider2D>().isTrigger = true;

        // �e�I�u�W�F�N�g���ƂɃ����_���Ȉړ�������ݒ�
        Vector3 direction = GetRandomDirection();

        // �����_���ȉ�]���x��ݒ�
        float rotationSpeed = Random.Range(50.0f, 200.0f); // ��]���x
        // �I�u�W�F�N�g���ړ�������
        StartCoroutine(MoveObject(newObject, spriteObject, direction, rotationSpeed));
        // ���̃X�|�[���܂ł̎��Ԃ������_���ɐݒ�
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnObject", currentSpawnInterval); // ���̃I�u�W�F�N�g�������_���ȊԊu�ŃX�|�[��
    }

    // �����_���ȕ����𐶐����郁�\�b�h
    Vector3 GetRandomDirection()
    {
        int curveDirection = Random.Range(0, 3); // �J�[�u�̕����i0=�����J�[�u, 1=�E��J�[�u, 2=���ւ̒��i�j
        if (curveDirection == 0)
        {
            return new Vector3(-1.0f, -0.5f, 0); // �����J�[�u
        }
        else if (curveDirection == 1)
        {
            return new Vector3(-1.0f, 0.5f, 0); // �E��J�[�u
        }
        else
        {
            return new Vector3(-1.0f, 0.0f, 0); // �܂�������
        }
    }

    IEnumerator MoveObject(GameObject obj, GameObject spriteObj, Vector3 direction, float rotationSpeed)
    {
        while (obj != null)
        {
            obj.transform.Translate(direction * speed * Time.deltaTime);
            // �X�v���C�g�I�u�W�F�N�g�݂̂���]
            spriteObj.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            // ��ʊO�ɏo����폜
            if (obj.transform.position.x < DestroyX)
            {
                Destroy(obj);
            }
            yield return null;
        }
    }

    // �����蔻�菈���𓯂��N���X���ɒǉ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[�ɐڐG�����ꍇ�̏���
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            // �v���C���[���j�󂳂�鏈���������ꏊ
            //Destroy(gameObject); // �������g��j��
        }
    }

}
