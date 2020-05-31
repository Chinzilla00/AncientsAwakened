﻿using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;

namespace AAMod
{
    public class DownedBools : ModWorld
    {
        public static bool downedGobSummoner = false;
        public static bool downedOgre = false;
        public static bool downedBetsy = false;
        public static bool downedMoth = false;

        public override void Initialize()
        {
            downedGobSummoner = false;
            downedOgre = false;
            downedBetsy = false;
            downedMoth = false;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedGobSummoner) downed.Add("GS");
            if (downedOgre) downed.Add("O");
            if (downedBetsy) downed.Add("B");
            if (downedMoth) downed.Add("M");

            return new TagCompound
            {
                {"downed", downed}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");

            downedGobSummoner = downed.Contains("GS");
            downedOgre = downed.Contains("O");
            downedBetsy = downed.Contains("B");
            downedMoth = downed.Contains("M");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte killed = new BitsByte();

            killed[0] = downedGobSummoner;
            killed[1] = downedOgre;
            killed[2] = downedBetsy;
            killed[3] = downedMoth;

            writer.Write(killed);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte killed = reader.ReadByte();

            downedGobSummoner = killed[0];
            downedOgre = killed[1];
            downedBetsy = killed[2];
            downedMoth = killed[3];
        }
    }
}
