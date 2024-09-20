using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class usual : MonoBehaviour
{

    public GameObject obstaclePrefab; // Prefab���C���X�y�N�^�[�Ŏw��
    private PlayerScript playerScript;
    public Sprite usualSprite; // �摜�iSprite�j
    public float spawnInterval = 0.5f; // ��莞�Ԃ��Ƃ̃X�|�[���Ԋu
    public float minSpawnInterval = 3.0f; // �X�|�[���Ԋu�̍ŏ��l
    public float maxSpawnInterval = 5.0f; // �X�|�[���Ԋu�̍ő�l
    public float spawnDuration = 5.0f; // ��Q���𐶐��������
    public float emptyDuration = 3.0f; // ��Q���𐶐����Ȃ��󔒊���
    public float speed = 2.0f; // �ړ����x
    public float speedIncreaseRate = 0.1f; // ���Ԍo�߂��Ƃ̃X�s�[�h������
    private float currentSpawnInterval; // ���݂̃X�|�[���Ԋu
    private float timeElapsed = 0.0f; // �o�ߎ���
    //�X�|�[������ꏊ
  public  float spawnX=10.0f;
  public  float spawnZ=0.0f;
    //���R����
    [SerializeField]
    private float DestroyX=-20.0f;

    void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        //Invoke("SpawnObject", currentSpawnInterval); // �����_���ȊԊu�ŃX�|�[���J�n
        StartCoroutine(SpawnCycle());
    }
    void Update()
    {
        // ���Ԍo�߂ɉ����ăX�s�[�h�𑝉�
        timeElapsed += Time.deltaTime;
        speed += speedIncreaseRate * Time.deltaTime * (GameSystem.Instance.BaseSpeed*0.06f);
    }

    // �����T�C�N���i�������ԂƋ󔒊��Ԃ̌J��Ԃ��j
    IEnumerator SpawnCycle()
    {
        while (true)
        {
            // ��������
            yield return StartCoroutine(SpawnObstacles());

            // �󔒊���
            yield return new WaitForSeconds(emptyDuration);
        }
    }

    // �������Ԓ��̏�Q������
    IEnumerator SpawnObstacles()
    {
        float elapsedTime = 0f;

        // �������Ԓ���0.5�b���Ƃɏ�Q���𐶐�
        while (elapsedTime < spawnDuration)
        {
            SpawnObject();

            // 0.5�b���Ƃɐ���
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }


    void SpawnObject()
    {
        // �v���n�u���C���X�^���X�����Đ���
        GameObject newObject = Instantiate(obstaclePrefab);

        // ��ʂ̉E�[���琶���iY���W�̓����_���j
        newObject.transform.position = new Vector3(spawnX, Random.Range(-4.0f, 4.0f), spawnZ);
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // �d�͂𖳌���
            rb.bodyType = RigidbodyType2D.Kinematic; // �����G���W���ɉe������Ȃ�
        }
        // �e�I�u�W�F�N�g���ƂɃ����_���Ȉړ�������ݒ�
        Vector3 direction = GetRandomDirection();

        // �����_���ȉ�]���x��ݒ�
        float rotationSpeed = Random.Range(50.0f, 200.0f); // ��]���x

        // �I�u�W�F�N�g���ړ�������
        StartCoroutine(MoveAndRotateObject(newObject, direction, rotationSpeed));

        // ���̃X�|�[���܂ł̎��Ԃ������_���ɐݒ�
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // �����_���ȕ����𐶐����郁�\�b�h
    Vector3 GetRandomDirection()
    {
        int curveDirection = Random.Range(0, 5); // �J�[�u�̕����i0=�����J�[�u, 1=�E��J�[�u, 2=���ւ̒��i, 3=�W�O�U�O�ړ�, 4=�g�`�ړ��j
        if (curveDirection == 0)
        {
            return new Vector3(-1.0f, -0.5f, 0); // �����J�[�u
        }
        else if (curveDirection == 1)
        {
            return new Vector3(-1.0f, 0.5f, 0); // �E��J�[�u
        }
        else if (curveDirection == 2)
        {
            return new Vector3(-1.0f, 0.0f, 0); // �܂�������
        }
        else if (curveDirection == 3)
        {
            return Vector3.zero; // �W�O�U�O�ړ��͓��ʂȏ�������Œǉ�
        }
        else
        {
            return Vector3.zero; // �g�`�ړ������ʂȏ�������Œǉ�
        }
    }

    // �ړ��Ɖ�]�𓯎��ɍs���R���[�`��
    IEnumerator MoveAndRotateObject(GameObject obj, Vector3 direction, float rotationSpeed)
    {
        float zigzagFrequency = 2.0f; // �W�O�U�O�ړ��̎��g��
        float waveAmplitude = 1.0f; // �g�`�ړ��̐U��
        float waveFrequency = 3.0f; // �g�`�ړ��̎��g��

        while (obj != null)
        {
            if (direction != Vector3.zero)
            {
                // �ʏ�̕����Ɉړ�
                obj.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                // �����_���ȃW�O�U�O�ړ�
                if (Random.Range(0, 2) == 0)
                {
                    obj.transform.Translate(new Vector3(-1.0f, Mathf.Sin(Time.time * zigzagFrequency), 0) * speed * Time.deltaTime, Space.World);
                }
                // �g�`�ړ�
                else
                {
                    obj.transform.Translate(new Vector3(-1.0f, Mathf.Sin(Time.time * waveFrequency) * waveAmplitude, 0) * speed * Time.deltaTime, Space.World);
                }
            }

            // �X�v���C�g�I�u�W�F�N�g�݂̂���]
            // usualSprite.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            obj.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
            // ��ʊO�ɏo����폜
            if (obj.transform.position.x < DestroyX)
            {
                Destroy(obj);
            }
            yield return null;
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

   


}
