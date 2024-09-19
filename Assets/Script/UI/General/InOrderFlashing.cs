using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InOrderFlashing : MonoBehaviour
{
    [SerializeField, Label("�_�ł�����I�u�W�F�N�g")] private List<GameObject> flashObjects;

    [SerializeField, Label("�_�ŊԊu")] private float flashTime;

    private float timer;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var flashObjects in flashObjects)
        {
            flashObjects.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1.0f * Time.deltaTime;

        if (timer >= flashTime)
        {
            foreach (var flashObjects in flashObjects)
            {
                flashObjects.SetActive(false);
            }

            if (index < flashObjects.Count)
                flashObjects[index].SetActive(true);

            index++;
            timer = 0;

            if (index >= flashObjects.Count) index = 0;
        }
    }
}
