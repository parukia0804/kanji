using UnityEngine;
using System.Collections.Generic;

public class CreateController : MonoBehaviour
{
    private GameObject selectedObject;

    [Header("��������{�^��")]
    [SerializeField] GameObject buttonPrefab; // ����������Prefab

    [Header("�X�|�[���|�C���g")]
    [SerializeField] Transform[] buttonSpawnPoints;   // Inspector�ňʒu���w��

    [Header("�������銿��")]
    [SerializeField] GameObject[] kanjiPrefabs; // ��������������

    private List<GameObject> buttons = new List<GameObject>();
    private List<GameObject> kanjis = new List<GameObject>();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAllButton();
        SpawnKanji();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }
    }

    void SelectObject()
    {
        // �}�E�X�ʒu�����[���h���W�ɕϊ�
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ��ʏ�̂��ׂĂ�Sprite���`�F�b�N
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();


        foreach (var sprite in sprites)
        {
            if (sprite.bounds.Contains(mousePos))
            {
                selectedObject = sprite.gameObject;
                Debug.Log($"�I�����ꂽ�I�u�W�F�N�g: {selectedObject.name}");
                break;
            }
        }

        if (selectedObject != null)
        {
            Button btn = selectedObject.GetComponent<Button>();
            Debug.Log("�{�^���F��");
            if (btn != null)
            {
                int num = btn.buttonNumber; // �����ŎQ�Ƃł���
                Debug.Log("���̃{�^���̔ԍ�: " + num);

                SetKanji(num);
            }
            else
            {
                Debug.Log("Button�X�N���v�g�����Ă��܂���");
            }
        }


    }

    void SpawnAllButton()
    {
        if (buttonPrefab == null || buttonSpawnPoints.Length == 0) return;

        for (int i = 0; i < buttonSpawnPoints.Length; i++)
        {
            Vector3 spawnPos = new Vector3(buttonSpawnPoints[i].position.x,
                                           buttonSpawnPoints[i].position.y,
                                           0f); // 2D�Ȃ̂� z = 0
            GameObject selectButton = Instantiate(buttonPrefab, spawnPos, Quaternion.identity);

            selectButton.name = "SelectButton" + (i + 1);

            buttons.Add(selectButton); // ���X�g�ɕۑ�
        }
    }
    
    void SpawnKanji()
    {
        for (int j = 0; j < buttons.Count; j++)
        {
            Button btn = buttons[j].GetComponent<Button>();
            if (btn == null) continue;
            int num = Random.Range(0, kanjiPrefabs.Length);
            btn.buttonNumber = num;
            GameObject kanji = Instantiate(
            kanjiPrefabs[num],                    // �ǂ̊������o����
            buttons[j].transform.position + new Vector3(0, 0, -1f),        // �{�^���Ɠ����ʒu
            Quaternion.identity                   // ��]�Ȃ�
            );

            kanji.transform.localScale = new Vector3(0.2f, 0.2f, 1f);

            kanjis.Add(kanji); // ���X�g�ɕۑ�
        }
    }

    void SetKanji(int knum)
    {
        // �{�^����j��
        foreach (var btn in buttons)
        {
            if (btn != null)
                Destroy(btn);
        }
        buttons.Clear();

        // ������j��
        foreach (var k in kanjis)
        {
            if (k != null)
                Destroy(k);
        }
        kanjis.Clear();
    }


}
