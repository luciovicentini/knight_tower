using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorData
{
    public int powerLevel { get; set; }
    public int floorNumber { get; set; }

    private static System.Random rng = new System.Random();

    public static List<FloorData> GetFloorPowerLevels(int floorAmount, int playerLevel = 1)
    {
        List<FloorData> floorDataList = new List<FloorData>();
        List<int> randomFloorNumberList = GetRandomFloorNumberList(floorAmount);

        floorDataList.Add(new FloorData
        {
            floorNumber = randomFloorNumberList[0],
            powerLevel = playerLevel == 1 ? 1 : playerLevel - 1
        });

        for (int i = 2; i <= floorAmount; i++)
        {
            int lastPowerLevel = floorDataList[i - 2].powerLevel;
            int randomPowerLevel = lastPowerLevel + Random.Range(1, lastPowerLevel + 1);
            floorDataList.Add(new FloorData { floorNumber = randomFloorNumberList[i - 1], powerLevel = randomPowerLevel });
        }

        return floorDataList.OrderBy(a => rng.Next()).ToList();
    }

    private static List<int> GetRandomFloorNumberList(int floorAmount)
    {
        List<int> floorNumberList = new List<int>();
        for (int i = 1; i <= floorAmount; i++)
        {
            floorNumberList.Add(i);
        }

        return floorNumberList.OrderBy(a => rng.Next()).ToList();
    }

    public override string ToString()
    {
        return $"FloorNumber = {floorNumber} - PowerLevel = {powerLevel}";
    }
}
