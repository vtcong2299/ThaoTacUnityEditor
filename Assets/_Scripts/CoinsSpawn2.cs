using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawn2 : SpawnCoins
{
    private void Reset()
    {
        this.preFabName = "CoinsPreFab2";
    }
    public override void GetPosition()
    {
        base.GetPosition();
        Vector3 position1 = new Vector3(Random.Range(-70, -74), 1f, Random.Range(-85, 40));
        AddPositionToList(position1);
        Vector3 position2 = new Vector3(Random.Range(50, 46), 1f, Random.Range(-85, 40));
        AddPositionToList(position2);
    }
}
