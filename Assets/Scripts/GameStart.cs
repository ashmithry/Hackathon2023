using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class GameStart : MonoBehaviour
{
    
    public GameObject AI;
    public GameObject Player;

    void Awake()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().playerCount = 32;
        //Spawn AI around the map
        List<string> names = new List<string>(usernames);

        Transform[] spawnPoints = GetComponentsInChildren<Transform>();

        Transform playerSpawnPoint;
        do
        {
            playerSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        } while (playerSpawnPoint == transform);

        //Spawn the player
        int index = 0;
        GameObject p = GameObject.Find("Player");
        p.transform.position = playerSpawnPoint.position;
        GameObject minimapCam = GameObject.Find("MinimapCam");
        minimapCam.transform.parent = p.transform;
        minimapCam.transform.localPosition = new Vector3(0, 85, 0);
        minimapCam.transform.Rotate(0, 0, 135);
        RotateTowardsCenter(p);

        foreach (Transform sp in spawnPoints)
        {
            if(sp == transform || Mathf.Abs(sp.position.magnitude) < 10f || sp == playerSpawnPoint)
            {
                continue;
            }

            GameObject o = Instantiate(AI, sp.position, Quaternion.identity);

            // Assign a random name

            int i = Random.Range(0, names.Count);
            o.GetComponent<ShipData>().username.GetChild(0).GetComponent<TextMeshPro>().text = names[i];
            names.Remove(names[i]);
        }
    }

    void RotateTowardsCenter(GameObject o)
    {
        Vector3 lookPos = Vector3.zero - o.transform.position;
        lookPos.y = 0;
        o.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    string[] usernames = {
    // Pirate-related usernames (25)
    "CaptainHook123",
    "Blackbeard89",
    "ScurvyDog456",
    "JollyRoger777",
    "SeaWolf12",
    "CannonballKate",
    "DavyJones555",
    "BuccaneerX",
    "TreasureHunter22",
    "PegLegPete",
    "Rumlover123",
    "ShiverMeTimbers",
    "SaltySailor789",
    "GoldToothedJack",
    "SwordMaster99",
    "MaroonedMark",
    "SquidBeard12",
    "KrakenSlayer123",
    "CorsairQueen",
    "BootyHunter42",
    "AdmiralAnnie",
    "SwashbucklerX",
    "PlunderPirate",
    "CrowsNest24",
    "RustyCutlass",
    "SailAway777",

    // Xbox Gamertag-style usernames (75)
    "ShadowNinja123",
    "DragonSlayerX",
    "SniperWolf89",
    "RapidFire666",
    "StarGazer23",
    "LoneWolf32",
    "TheIronFist",
    "MidnightRiderX",
    "CyberPunk007",
    "EpicGamerX",
    "JediMaster99",
    "NeonSpectre123",
    "FirestormX",
    "ThunderBolt42",
    "MysticBladeX",
    "DarkKnight777",
    "StormChaserX",
    "PsychoKiller23",
    "BlazeFire89",
    "NightmareX",
    "SpeedDemon123",
    "SteelHeartedX",
    "BioHazard42",
    "DoomBringerX",
    "PhantomStriker",
    "NovaStrike777",
    "DeathWishX",
    "SavageHunter23",
    "IronLion32",
    "PixelWarriorX",
    "ShadowHunter007",
    "EagleEyeX",
    "VenomousViper123",
    "GhostRiderX",
    "MegaBlast42",
    "XtremeGamerX",
    "BladeMaster99",
    "CrimsonFang123",
    "SpartanWarriorX",
    "WickedWitch89",
    "RavenDarkness666",
    "StealthyNinja23",
    "SilverBullet32",
    "RampageX",
    "CyborgX",
    "SonicBoom007",
    "BlitzkriegX",
    "Stormbringer123",
    "LaserSlingerX",
    "NebulaSorcerer",
    "GrimReaper777",
    "RecklessRider23",
    "ViperFang89",
    "ShatteredDreamX",
    "PhantomBlade123",
    "DreadnoughtX",
    "ShadowWraith42",
    "SoulStealerX",
    "VenomStrike777",
    "BloodLustX",
    "NightStalker23",
    "IronFist32",
    "MadHatterX",
    "CyberShadow007",
    "EternalPhoenixX",
    "Frostbite123",
    "DeathBringerX",
    "SilentAssassin42",
    "VortexX",
    "NovaBlitz777",
    "ToxicShockX",
    "Warlord99",
    "Nightshade123",
    "VoidWalkerX",
    "LoneRanger89",
    "Hellfire666",
    "ShadowLurker23",
    "DeathMachineX",
    "Ragnarok007",
    "RapidStrikeX",
    "StormSorcerer123",
    "DoomSlayerX",
    "BlackWidow42",
    "NemesisX",
    "PhantomWolf777",
    "SavageAngel23",
    "IronLioness32",
    "MysticDragonX"
    };
}
