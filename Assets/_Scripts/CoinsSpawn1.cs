using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawn1 : SpawnCoins
{
    private void Reset()
    {
        this.preFabName = "CoinsPreFab1";
    }    
    public override void GetPosition()
    {
        base.GetPosition();
        Vector3 position3 = new Vector3(Random.Range(-60, 40), 1f, Random.Range(-94, -98));
        AddPositionToList(position3);
        Vector3 position4 = new Vector3(Random.Range(-60, 40), 1f, Random.Range(46, 50));
        AddPositionToList(position4);
    }
    
}
