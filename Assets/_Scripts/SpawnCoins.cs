using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnCoins : MonoBehaviour
{
    protected List<GameObject> objPreFab;
    //protected GameObject spawnPos;
    protected GameObject preFab;
    protected float spawnTimer = 0f;
    protected float spawnDelay = 5f;
    protected int maxObj = 5;
    protected int index = 0;
    protected float randPosX = 0f;
    protected float randPosZ = 0f;
    public string preFabName = "";
    public List<Vector3> positions = new List<Vector3>();
    private void Awake()
    {        
        this.preFab = GameObject.Find(preFabName);
        this.preFab.SetActive(false);
        this.objPreFab = new List<GameObject>();
    }
    protected virtual void Spawn()
    {
        //Nếu player chết thì ngưng spawn enemy
        if (CarMove.instance.dameReceiver.IsDead())
        {
            return;
        }
        this.spawnTimer += Time.deltaTime;
        if (this.spawnTimer < spawnDelay)
        {
            return;
        }
        this.spawnTimer = 0;
        //Nếu enemy spawn nhiều hơn số lượng enemy cần spawn sẽ ngừng
        if (this.objPreFab.Count >= this.maxObj)
        {
            return;
        }
        //Tạo gameobject mới dựa trên gameobject cho sẵn
        GameObject obj = Instantiate(this.preFab);
        obj.name = "Coins " + index;
        index++;
        if (index >= this.maxObj)
        {
            index = 0;
        }
        //Thay đổi vị trí các gameobject mới tạo theo vị trí player
        GetPosition();
        obj.transform.position = GetRandomPosition();
        obj.transform.parent = transform;
        //Set các gameobject mới tạo ở dạng có sử dụng
        obj.gameObject.SetActive(true);
        this.objPreFab.Add(obj);
    }
    public virtual void GetPosition()
    {

    }
    public void AddPositionToList(Vector3 newPosition)
    {
        positions.Add(newPosition);
        if (positions.Count >= 8)
        {
            positions.RemoveRange(0, 4);
        }
    }
    Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, positions.Count); // Tạo số ngẫu nhiên giữa 0 và 3
        return positions[randomIndex]; // Trả về vị trí tương ứng
    }
    protected virtual void CheckDead()
    {
        GameObject minion;
        for (int i = 0; i < this.objPreFab.Count; i++)
        {
            minion = this.objPreFab[i];
            if (minion == null)
            {
                this.objPreFab.RemoveAt(i);
            }
        }
    }    
    void Update()
    {
        this.Spawn();
        this.CheckDead();
    }
}
