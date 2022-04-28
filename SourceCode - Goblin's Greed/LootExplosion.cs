using UnityEngine;

public abstract class LootExplosion
{
    private static readonly Vector3[] Directions =  {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
    public static void InstantiateExplosion(GameObject[] loot)
    {
        foreach (var item in loot)
        {
            var direction = Directions[Random.Range(0, Directions.Length)];
            item.GetComponent<Rigidbody>().AddForce(direction * Random.Range(5f, 10f), ForceMode.Impulse);
        }
    }

    public static void InstantiateExplosion(GameObject loot)
    {
        var direction = Directions[Random.Range(0, Directions.Length)];
        loot.GetComponent<Rigidbody>().AddForce(direction * Random.Range(5f, 10f), ForceMode.Impulse);
    }
}


