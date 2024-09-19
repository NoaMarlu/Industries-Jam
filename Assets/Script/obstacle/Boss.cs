using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Sprite bossSprite; // �{�X�̉摜�i�X�v���C�g�j
    public float initialSpeed = 5.0f; // �ŏ��̍��ړ��̃X�s�[�h
    public float ellipseSpeed = 2.0f; // �ȉ~�O���̃X�s�[�h
    public float ellipseWidth = 2.0f; // �ȉ~�O���̉���
    public float ellipseHeight = 4.0f; // �ȉ~�O���̏c��
    public float leftMoveDistance = 3.0f; // ���Ɉړ����鋗��

    private bool isMovingLeft = true; // ���ړ������ǂ���
    private Vector3 startPosition; // �ȉ~�O���̊J�n�ʒu
    private float timeElapsed = 0.0f; // �o�ߎ���
    private SpriteRenderer spriteRenderer; // SpriteRenderer
    private bool isBossActivated; // �{�X���L��������Ă��邩�ǂ���
    //�X�|�[������ꏊ
    public float spawnX = 10.0f;

    void Start()
    {
        // �{�X����ʂ̉E�O�ɔz�u
        //�Ȃɂ�if���ǉ����ď������o�Ă��Ȃ�����boss�͏o�Ă��Ȃ��悤�ɂ���
        transform.position = new Vector3(spawnX, 0.0f, 0.0f);

        // SpriteRenderer��ǉ����ăX�v���C�g��ݒ�
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = bossSprite;
        // Rigidbody2D��ǉ��i�d�͎͂g�p���Ȃ��j
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // Collider2D��ǉ��i�����蔻��p�j
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true; // �g���K�[�Ƃ��ċ@�\
        isBossActivated = true;
    }
    public void SpawnBoss()
    {
        // �{�X����ʓ��ɏo��������
        Instantiate(gameObject);
        transform.position = new Vector3(spawnX, 0.0f, 0.0f);
        isMovingLeft = true; // �{�X�����ړ����J�n
        isBossActivated = true; // �{�X���L���������
    }
    void Update()
    {

        if (!isBossActivated)
        {
            return; // �{�X���L���������܂őҋ@
        }

        if (isMovingLeft)
        {
            // ���Ɉړ�
            transform.Translate(Vector3.left * initialSpeed * Time.deltaTime);

            // ���̋����܂ňړ�������ȉ~�O����
            if (transform.position.x <= 10.0f - leftMoveDistance)
            {
                isMovingLeft = false;
                
                startPosition = transform.position; // �ȉ~�O���̊�_
            }
        }
        else
        {
            // �E���̏c�^�ȉ~�`�̓���������
            timeElapsed += Time.deltaTime * ellipseSpeed;

            float x = Mathf.Cos(timeElapsed) * ellipseWidth;
            float y = Mathf.Sin(timeElapsed) * ellipseHeight;

            // �ȉ~�O���ɉ����ă{�X���ړ�
            transform.position = new Vector3(startPosition.x + x, startPosition.y + y, 0);
        }
    }

    // �Փˎ��̏���
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �Ⴆ�΁A�v���C���[���Q���ɏՓ˂����Ƃ��̏����������ɋL�q
        if (other.gameObject.CompareTag("Player"))
        {
            // �v���C���[�ƏՓ˂����ꍇ�̏����i��F�v���C���[��j��j
            Debug.Log("Boss collided with the player!");
            // �ǉ��̏����������ɋL�q�i��F�v���C���[��HP������Q�[���I�[�o�[�����j

        }
    }
}
