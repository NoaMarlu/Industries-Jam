using UnityEngine;

public class BackBooster : ItemScript
{
    //audio
    public AudioClip collectedClip;
    protected AudioSource audioSource;

    //�X�e�[�^�X
    [SerializeField] private float speed;
    [SerializeField] private float energy;

    private float offset = 0.5f;

    private void Start()
    {
        //�I�[�f�B�I����
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�擾��
        if (hit)
        {
            transform.position = new Vector3(PlayerScript.instance.transform.position.x - number * offset,
                                              PlayerScript.instance.transform.position.y,
                                              PlayerScript.instance.transform.position.z - offset);
        }
        //�擾���Ă��Ȃ��ꍇ
        else
        {
            //��ʉE���痬��Ă���
            transform.Translate(moveDirection*moveSpeed*Time.deltaTime);
        }

        //��ʓ���������
        SurvivalCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            //�v���C���[�ɓ������
            PlayerScript script = collision.GetComponent<PlayerScript>();

            if (script != null)
            {
                //�I�[�f�B�I�Đ�
                audioSource.PlayOneShot(collectedClip);

                //�v���C���[�̃��X�g�ɓo�^
                PlayerScript.instance.GetBackBoosters().Add(this);
                AfterBurner.Instance.ResetBuner();
                number = PlayerScript.instance.GetBackBoosters().Count;
                GameSystem.Instance.AddSpeed(speed);
                hit = true;
                GameSystem.Instance.ItemGet = true;
            }
        }
        
    }

}
