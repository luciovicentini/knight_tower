using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class FloorData {
    public FloorData(int powerLevel, int floorNumber, string levelSymbol) {
        this.powerLevel = powerLevel;
        this.floorNumber = floorNumber;
        this.levelSymbol = levelSymbol;
    }

    public FloorData(int powerLevel) {
        this.powerLevel = powerLevel;
        floorNumber = 1;
        levelSymbol = "";
    }

    public FloorData(int powerLevel, string levelSymbol) {
        this.powerLevel = powerLevel;
        floorNumber = 1;
        this.levelSymbol = levelSymbol;
    }

    public int powerLevel { get; set; }
    public int floorNumber { get; set; }
    public string levelSymbol { get; set; }

    public static FloorData One() {
        return new(1, 1, "");
    }

    public override string ToString() {
        return $"FloorNumber = {floorNumber} - PowerLevel = {powerLevel} {levelSymbol}";
    }

    internal string GetFullPowerLevelString() {
        if (levelSymbol != "") {
            return $"{powerLevel} {levelSymbol}";
        }
        return powerLevel.ToString();
    }
}

public static class FloorDataUtil {
    private static readonly Random Rng = new();

    private static readonly Dictionary<string, string> PowerLevelSymbol = new() {
        { "", "A" }, { "A", "B" }, { "B", "C" }, { "C", "D" }, { "D", "E" }, { "E", "F" }, { "F", "G" },
        { "G", "H" }, { "H", "I" }, { "I", "J" }, { "J", "K" }, { "K", "L" }, { "L", "M" }, { "M", "N" },
        { "N", "O" }, { "O", "P" }, { "P", "Q" }, { "Q", "R" }, { "R", "S" }, { "S", "T" }, { "T", "U" },
        { "U", "V" }, { "V", "W" }, { "W", "X" }, { "X", "Y" }, { "Y", "Z" }, { "Z", "A" }
    };

    public static List<FloorData> GetFloorPowerLevels(int floorAmount, FloorData playerLevel) {
        var floorDataList = new List<FloorData>();
        var randomFloorNumberList = GetRandomFloorNumberList(floorAmount);

        var firstFloor = new FloorData(floorNumber: randomFloorNumberList[0],
            powerLevel: UnityEngine.Random.Range(1, playerLevel.powerLevel),
            levelSymbol: playerLevel.levelSymbol);

        floorDataList.Add(firstFloor);

        var calculatedPlayerLevel = playerLevel.powerLevel + firstFloor.powerLevel;
        for (var i = 2; i <= floorAmount; i++) {
            var lastPowerLevel = floorDataList[i - 2].powerLevel;
            var randomPowerLevel = UnityEngine.Random.Range(lastPowerLevel, calculatedPlayerLevel + 1);
            calculatedPlayerLevel += randomPowerLevel;
            floorDataList.Add(new FloorData(floorNumber: randomFloorNumberList[i - 1], powerLevel: randomPowerLevel,
                levelSymbol: playerLevel.levelSymbol));
        }

        return floorDataList.OrderBy(a => Rng.Next()).ToList();
    }

    private static List<int> GetRandomFloorNumberList(int floorAmount) {
        var floorNumberList = new List<int>();
        for (var i = 1; i <= floorAmount; i++) {
            floorNumberList.Add(i);
        }

        return floorNumberList.OrderBy(a => Rng.Next()).ToList();
    }

    public static FloorData GetBossLevel(List<FloorData> floorDataList, FloorData playerLevel) {
        int bossLevel = GetPlayerLevelWhenFacingBoss(floorDataList, playerLevel);


        if (ShouldPlayerLoseBossBattle()) {
            bossLevel = Rng.Next(bossLevel + 1, bossLevel + 100);
        } else {
            int minBossLevel = Mathf.Clamp((bossLevel - 100), 1, bossLevel);
            bossLevel = Rng.Next(minBossLevel, bossLevel);
        }

        return new FloorData(bossLevel, -1, playerLevel.levelSymbol);
    }

    private static bool ShouldPlayerLoseBossBattle() {
        int randomNumber = Rng.Next(1, 500);
        return randomNumber < GameManager.Instance.nextTowerLevel;
    }

    public static int GetPlayerLevelWhenFacingBoss(List<FloorData> floorDataList, FloorData playerLevel) {
        int playerLevelWhenFacingBoss = playerLevel.powerLevel;
        
        foreach (var floorData in floorDataList) {
            playerLevelWhenFacingBoss += floorData.powerLevel;
        }

        return playerLevelWhenFacingBoss;
    }

    public static FloorData UpdatePlayerLevel(FloorData data) {
        if (data.powerLevel < 1000) return data;

        var timesNextSymbol = 0;
        while (data.powerLevel >= 1000) {
            data.powerLevel = data.powerLevel / 1000;
            timesNextSymbol++;
        }

        for (var i = 0; i < timesNextSymbol; i++) data.levelSymbol = GetNextLevelSymbol(data.levelSymbol);
        return data;
    }

    private static string GetNextLevelSymbol(string levelSymbol) {
        if (levelSymbol.Length == 0 || levelSymbol.Length == 1) {
            return PowerLevelSymbol[levelSymbol];
        }

        var result = "";
        foreach (var c in levelSymbol) result += PowerLevelSymbol[c.ToString()];
        return result;
    }
}