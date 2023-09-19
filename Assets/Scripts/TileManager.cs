using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RGVA;
using Sirenix.OdinInspector;
using DG.Tweening;


public class TileManager : MonoBehaviour
{
    [System.Serializable]
    public class Tiles
    {
        public int ID;
        public string name;
        public Sprite sprite;
        public int Quantity;
    }


    [SerializeField] GameObject tilePrefab;
    public Tiles[] tiles;

    public List<Vector2> tileCoord = new List<Vector2>();

    

    int firstTile = -1;
    public int fracturedCount = 0;
    public int xMin = 0;
    public int xMax = 0;
    public int yMax = 0;

    private void OnEnable()
    {
        RandomizeTiles();
        GameEvents.Instance.matchedPair = 0;
        //InstantiateTiles();
    }

    public int clusterPairCount = 0;

    [Button]
    private void RandomizeTiles()
    {
        tileCoord.Clear();

        for (int j = transform.childCount - 1; j >= 0; j--)
        {
            int x, y;
            do
            {
                x = XRandomizer();
                y = -YRandomizer();

            }
            while (tileCoord.Contains(new Vector2(x, y)));

            tileCoord.Add(new Vector2(x, y));
            transform.GetChild(j).gameObject.transform.localPosition = new Vector3(x,y,0);


        }


        





    }




    [Button]
    private void InstantiateTiles()
    {
        clusterPairCount = 0;
        foreach (Tiles t in tiles) { clusterPairCount += t.Quantity; }

        tileCoord.Clear();
        for (int j = transform.childCount - 1; j >= 0; j--)
        {
            DestroyImmediate(transform.GetChild(j).gameObject);
        }



        int i = 0;
        foreach (Tiles t in tiles)
        {
            for (int k = 0; k < t.Quantity; k++)
            {




                int x, y;
                do
                {
                    x = XRandomizer();
                    y = -YRandomizer();

                }
                while (tileCoord.Contains(new Vector2(x, y)));



                tileCoord.Add(new Vector2(x, y));


                GameObject temp = Instantiate(tilePrefab, new Vector3(x, y, 0) + transform.position, Quaternion.identity, transform);
                temp.name = "TileSlab " + i + "";
                temp.GetComponent<Tile>().name = t.name;
                temp.GetComponent<Tile>().ID = t.ID;
                temp.GetComponent<Tile>().sprite.sprite = t.sprite;





                do
                {
                    x = XRandomizer();
                    y = -YRandomizer();

                }
                while (tileCoord.Contains(new Vector2(x, y)));

                tileCoord.Add(new Vector2(x, y));


                temp = Instantiate(tilePrefab, new Vector3(x, y, 0) + transform.position, Quaternion.identity, transform);
                temp.name = "TileSlab " + i + "";
                temp.GetComponent<Tile>().name = t.name;
                temp.GetComponent<Tile>().ID = t.ID;
                temp.GetComponent<Tile>().sprite.sprite = t.sprite;





                i++;
            }

        }


    }

    private int XRandomizer()
    {
        int num;
        do
        {
            num = Random.Range(xMin, xMax);
        }
        while (num % 2 != 0);

        return num;

    }
    private int YRandomizer()
    {
        int num;
        
       



        do
        {
            num = Random.Range(0, yMax);
        }
        while (num % 2 != 0);

        return num;

    }


    public bool TileValidator(int ID)
    {
        //if first box broken
        if (firstTile == -1)
        {
            firstTile = ID;
            return false;
        }
        else
        {
            //if correct pair is opened
            if (firstTile == ID)
            {
                GameEvents.Instance.matchedPair++;

                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).gameObject.GetComponent<Tile>().ID == ID && transform.GetChild(i).gameObject.GetComponent<Tile>().opened)
                    {


                        Matched(transform.GetChild(i).gameObject);
                        firstTile = -1;

                    }
                }
                return true;
            }
            //If wrong pair, return the tile to boxed state again
            else
            {
                firstTile = ID;
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).gameObject.transform.childCount < 2)
                    {
                        transform.GetChild(i).gameObject.GetComponent<Tile>().SpawnBox();

                    }
                }

                return false;
            }
        }

    }



    void Matched(GameObject tile)
    {
        Sequence tileAnim = DOTween.Sequence();
        tileAnim.Append(tile.transform.DOScale(new Vector3(1 * 2, 1.28888893f * 2, 0.157635227f * 2), 0.2f));
        tileAnim.Append(tile.transform.DOScale(new Vector3(1, 1.28888893f, 0.157635227f), 0.2f));
        tileAnim.Append(tile.transform.DOLocalRotate(new Vector3(0, 360, 0), 0.2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        tileAnim.Append(tile.transform.DOLocalRotate(new Vector3(0, 360, 0), 0.2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        tileAnim.Append(tile.transform.DOLocalRotate(new Vector3(0, 360, 0), 0.2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));

        tileAnim.OnComplete(() => WinCheck(tile));






    }

    void WinCheck(GameObject tile)
    {
        AudioManager.Instance.Play("Matched");
        PoolManager.Instance.Dequeue(ePoolType.ParticleSystem, tile.transform.position);
        tile.SetActive(false);
        if (GameEvents.Instance.matchedPair == clusterPairCount)
        {
            if (TileClusterManager.Instance.NoClustersRemaining())
            {
                GameEvents.Instance.timerOn = false;
                StartCoroutine(DelayWinUI());
            }
            else
            {
                this.gameObject.SetActive(false);
            }

        }

    }

    IEnumerator DelayWinUI()
    {
        yield return new WaitForSeconds(1f);
        LevelsAvailable.Instance.UnlockedNewLevel(SceneManager.GetActiveScene().buildIndex);
        UIManager.Instance.Win();
    }




}
