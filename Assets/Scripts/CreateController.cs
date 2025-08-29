using UnityEngine;
using System.Collections.Generic;

public class CreateController : MonoBehaviour
{
    private GameObject selectedObject;

    [Header("生成するボタン")]
    [SerializeField] GameObject buttonPrefab; // 生成したいPrefab

    [Header("スポーンポイント")]
    [SerializeField] Transform[] buttonSpawnPoints;   // Inspectorで位置を指定

    [Header("生成する漢字")]
    [SerializeField] GameObject[] kanjiPrefabs; // 生成したい漢字

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
        // マウス位置をワールド座標に変換
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 画面上のすべてのSpriteをチェック
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();


        foreach (var sprite in sprites)
        {
            if (sprite.bounds.Contains(mousePos))
            {
                selectedObject = sprite.gameObject;
                Debug.Log($"選択されたオブジェクト: {selectedObject.name}");
                break;
            }
        }

        if (selectedObject != null)
        {
            Button btn = selectedObject.GetComponent<Button>();
            Debug.Log("ボタン認識");
            if (btn != null)
            {
                int num = btn.buttonNumber; // ここで参照できる
                Debug.Log("このボタンの番号: " + num);

                SetKanji(num);
            }
            else
            {
                Debug.Log("Buttonスクリプトがついていません");
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
                                           0f); // 2Dなので z = 0
            GameObject selectButton = Instantiate(buttonPrefab, spawnPos, Quaternion.identity);

            selectButton.name = "SelectButton" + (i + 1);

            buttons.Add(selectButton); // リストに保存
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
            kanjiPrefabs[num],                    // どの漢字を出すか
            buttons[j].transform.position + new Vector3(0, 0, -1f),        // ボタンと同じ位置
            Quaternion.identity                   // 回転なし
            );

            kanji.transform.localScale = new Vector3(0.2f, 0.2f, 1f);

            kanjis.Add(kanji); // リストに保存
        }
    }

    void SetKanji(int knum)
    {
        // ボタンを破棄
        foreach (var btn in buttons)
        {
            if (btn != null)
                Destroy(btn);
        }
        buttons.Clear();

        // 漢字を破棄
        foreach (var k in kanjis)
        {
            if (k != null)
                Destroy(k);
        }
        kanjis.Clear();
    }


}
