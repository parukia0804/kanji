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

    private GameObject currentKanji; // ���쒆�̃I�u�W�F�N�g
    private bool isPlaced = false;   // �E�N���b�N�ŌŒ肳�ꂽ��




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAllButton();
        SpawnKanji();
    }

    // Update is called once per frame
    void Update()
    {

        // ���ݑI�𒆂̊���������΃}�E�X�ɒǏ]
        if (currentKanji != null && !isPlaced)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // 2D�Ȃ̂� Z �� 0

            currentKanji.transform.position = mousePos;

            // �E�N���b�N�ŌŒ�
            if (Input.GetMouseButtonDown(1))
            {
                isPlaced = true;
                Debug.Log($"���� {currentKanji.name} ���Œ肵�܂����I");
            }
        }

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

                resetKanji();

                currentKanji = kanjiPrefabs[num];

                Vector3 mousePosa = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosa.z = 0f;
                currentKanji = Instantiate(kanjiPrefabs[num], mousePosa, Quaternion.identity);


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

            kanji.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            kanjis.Add(kanji); // ���X�g�ɕۑ�
        }
    }

    void resetKanji()
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
