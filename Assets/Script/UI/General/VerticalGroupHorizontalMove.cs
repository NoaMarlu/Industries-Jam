using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGroupHorizontalMove : MonoBehaviour
{
    [Space(10)]
    [Header("�g�p����ۂ̓A���J�[�|�C���g��^�񒆂ɂ��Ă�������")]

    [SerializeField, NamedArray("�ړ����������Q�[���I�u�W�F�N�g")] private List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField, Label("�����ʒu")] private float startPos;

    [SerializeField, Label("�I���ʒu")] private float endPos;

    [SerializeField, Label("�I���܂ł̎���")] private float waitTime = 10;

    [SerializeField, Label("�e��̏I��鎞��")] private float waitTimes = 10;

    [SerializeField, Label("�����炠��R���|�[�l���g���g�p���邩")] private bool useOriginComponent = false;

    [SerializeField, Label("�t�F�[�h�����邩�ǂ���")] private bool isFade = false;

    [SerializeField, Label("��A�N�e�B�u�̍ێ����Ń��Z�b�g")] private bool isAutoInitialize = true;

    // �C�[�W���O�^�C�}�[
    private float et = 0;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // ������
    private void Initialize()
    {
        et = 0;
        if (gameObjects == null) return;
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<Transform>().localPosition =
            new Vector3(startPos, gameObject.GetComponent<Transform>().localPosition.y, gameObject.GetComponent<Transform>().localPosition.z);
            if (!useOriginComponent)
            {
                var horizontalMove = gameObject.GetComponent<HorizontalMove>();
                Destroy(horizontalMove);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObjects == null) return;

        et += 1.0f * Time.deltaTime;

        // �w�肵���I�u�W�F�N�g�̎q���̈ʒu�f�[�^���Q�Ƃ���
        bool[] index = new bool[gameObjects.Count];
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (Mathf.Floor(et / (waitTime / (gameObjects.Count + 1))) >= i)
                index[i] = true;

            var fade = gameObjects[i].GetComponent<Fade>();
            if (!useOriginComponent)
            {
                var horizontalMove = gameObjects[i].GetComponent<HorizontalMove>();

                if (index[i] && !horizontalMove)
                {
                    gameObjects[i].SetActive(true);
                    HorizontalMove childrenMove = gameObjects[i].AddComponent<HorizontalMove>();
                    childrenMove.SetStartPos(startPos);
                    childrenMove.SetEndPos(endPos);
                    childrenMove.SetWaitTime(waitTimes);
                }
            }
            else
            {
                if (index[i])
                {
                    gameObjects[i].SetActive(true);
                }
            }
            if (isFade && !fade)
            {
                Fade addFade = gameObjects[i].AddComponent<Fade>();
                addFade.SetWaitTime(waitTimes);
            }
        }

        const float adJust = 0.9f;
        // �\������Ȃ������p
        if (et >= waitTimes * gameObjects.Count * adJust) 
        {
            if (!useOriginComponent)
            {
                foreach (var gameObj in gameObjects)
                {
                    gameObj.transform.localPosition = new Vector3(endPos, gameObj.transform.localPosition.y, gameObj.transform.localPosition.z);
                }
            }
        }
    }

    // ��A�N�e�B�u�̍ۂ̍X�V����
    private void OnDisable()
    {
        if (isAutoInitialize) Initialize();
    }

    public bool Finish() { return (et > waitTime); }

    public void SetGameObject(GameObject addObj) { gameObjects.Add(addObj); }

    public void ClearGameObject() { gameObjects.Clear(); }

    public void SetStartPos(float startPos) { this.startPos = startPos; }

    public void SetEndPos(float endPos) { this.endPos = endPos; }

    public void SetWaitTime(float waitTime) { this.waitTime = waitTime; }

    public void SetWaitTimes(float waitTimes) { this.waitTimes = waitTimes; }

    public void SetIsFade(bool isFade) { this.isFade = isFade; }
}
