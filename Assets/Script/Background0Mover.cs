using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background0Mover : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_offsetSpeed; // x��y�̒l���傫���قǑ����X�N���[������

    [SerializeField]
    private float m_accelSpeed; // �����x

    private Vector3 m_cameraRectMin;

    [SerializeField]
    private bool m_getParts = false; // �p�[�c����肵���t���O

    [SerializeField]
    private bool m_lostParts = false; // �p�[�c�����X�g�����t���O


    void Start()
    {
        //�J�����͈̔͂��擾
        m_cameraRectMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));

        m_offsetSpeed.x = -0.2f;
        m_accelSpeed = -0.1f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * m_offsetSpeed * Time.deltaTime);   //X�������ɃX�N���[��

        //�J�����̍��[���犮�S�ɏo����A�E�[�ɏu�Ԉړ�
        if (transform.position.x < (m_cameraRectMin.x - Camera.main.transform.position.x) * 2)
        {
            transform.position = new Vector2((Camera.main.transform.position.x - m_cameraRectMin.x) * 2, transform.position.y);
        }

        m_getParts = GameSystem.Instance.ItemGet;
        // �p�[�c����肵����X�N���[�����x���グ��
        if (m_getParts)
        {
            m_offsetSpeed = new Vector2(m_offsetSpeed.x + m_accelSpeed, m_offsetSpeed.y);
            m_offsetSpeed.x = Mathf.Max(m_offsetSpeed.x, -50.0f);
            m_getParts = false;
        }

        // �p�[�c�����X�g������X�N���[�����x��������
        if (m_lostParts)
        {
            m_offsetSpeed = new Vector2(m_offsetSpeed.x - m_accelSpeed, m_offsetSpeed.y);
            m_offsetSpeed.x = Mathf.Min(m_offsetSpeed.x, -1.0f);
            m_lostParts = false;
        }
    }
}