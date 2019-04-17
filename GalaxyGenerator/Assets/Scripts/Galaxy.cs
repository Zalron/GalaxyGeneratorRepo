﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
namespace GalaxyGenerator
{
    public class Galaxy : MonoBehaviour
    {
        
        //public Sector sector;
        public GameObject player;
        public GameObject starSystemsObject;
        public static int SectorSize = 100;
        public static ConcurrentDictionary<string, Sector> sectors;
        public static List<string> toRemove = new List<string>(); // a list to remove the chunks that are not needed from the dictionary
        public static int galaxyHeight = 120; // the height of the world
        public static int galaxyLength = 1000; // the height of the world
        public static int galaxyWidth = 1000; // the height of the world
        // Start is called before the first frame update
        void Start()
        {
            GenerateGalaxy((int)(player.transform.position.x / SectorSize), (int)(player.transform.position.y / SectorSize), (int)(player.transform.position.z / SectorSize), 1, 1);
        }
        public static string BuildSectorName(Vector3 v) // assigning a name to a Sector
        {
            return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
        }
        void GenerateSectorAt(int x, int y, int z)// builds Sectors
        {
            Vector3 SectorPosition = new Vector3(x * SectorSize, y * SectorSize, z * SectorSize);
            string sn = BuildSectorName(SectorPosition);
            Sector s = new Sector(SectorType.Core, SectorPosition, starSystemsObject, sn, this);
        }
        void GenerateGalaxy(int x, int y, int z, int startradius, int radius)// builds Sectors around the player
        {

            //builds chunk center
            GenerateSectorAt(x, y, z);
            //builds chunk forward
            GenerateSectorAt(x, y, z + 1);
            //builds chunk back
            GenerateSectorAt(x, y, z - 1);
            //builds chunk left
            GenerateSectorAt(x - 1, y, z);
            //builds chunk right
            GenerateSectorAt(x + 1, y, z);
            //builds chunk up
            GenerateSectorAt(x, y + 1, z);
            //builds chunk down
            GenerateSectorAt(x, y - 1, z);
        }
        public void GenerateSectorStars(Vector3 StarPosition, GameObject sector)
        {
            Instantiate(starSystemsObject, StarPosition, Quaternion.identity, sector.transform);
        }
    }
}
