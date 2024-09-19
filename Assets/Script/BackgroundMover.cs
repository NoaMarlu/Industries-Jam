using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMover : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_propName = "_MainTex";

    [SerializeField]
    private Vector2 m_offsetSpeed; // x��y�̒l���傫���قǑ����X�N���[������

    [SerializeField]
    private float m_accelSpeed = 0.1f; // �����x

    private Material m_material;

    [SerializeField]
    private bool m_getParts = false; // �p�[�c����肵���t���O

    [SerializeField]
    private bool m_lostParts = false; // �p�[�c�����X�g�����t���O

    private float m_time = 0.0f;
    private float m_timeInterval = 0.0f;


    private void Start()
    {
        if (GetComponent<Image>() is Image i)
        {
            m_material = i.material;
        }

        m_offsetSpeed.x = 0.1f;
    }

    private void Update()
    {
        if (m_material)
        {
            // x��y�̒l��0 �` 1�Ń��s�[�g����悤�ɂ���
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
            var offset = new Vector2(x, y);
            m_material.SetTextureOffset(k_propName, offset);

            m_time += Time.deltaTime;
            // �p�[�c����肵����X�N���[�����x���グ��
            if (m_getParts /*|| m_time > 10*/) 
            {
                //m_offsetSpeed.x += m_accelSpeed;

                m_offsetSpeed = new Vector2(m_offsetSpeed.x + m_accelSpeed, m_offsetSpeed.y);
                m_getParts = false;
                m_time = 0.0f;
            }

            if(m_lostParts)
            {
                // �p�[�c�����X�g������X�N���[�����x��������
                m_offsetSpeed.x -= m_accelSpeed;
                m_offsetSpeed.x = Mathf.Max(m_offsetSpeed.x, 0.1f);
                m_lostParts = false;
            }
        }
    }

    private void OnDestroy()
    {
        // �Q�[������߂���Ƀ}�e���A����Offset��߂��Ă���
        if (m_material)
        {
            m_material.SetTextureOffset(k_propName, Vector2.zero);
        }
    }
    
}
