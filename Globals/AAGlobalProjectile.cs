using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;
using AAMod.NPCs.Bosses.Broodmother;
using AAMod.NPCs.Bosses.Hydra;
using AAMod.NPCs.Bosses.Equinox;
using AAMod.NPCs.Bosses.Orthrus;
using AAMod.NPCs.Bosses.Raider;
using AAMod.NPCs.Bosses.Retriever;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using AAMod.NPCs.Bosses.Zero;
using AAMod.NPCs.Bosses.Zero.Protocol;
using AAMod.NPCs.Bosses.MushroomMonarch;
using AAMod.NPCs.Bosses.Djinn;
using AAMod.NPCs.Bosses.Serpent;
using AAMod.NPCs.Bosses.AH.Ashe;
using AAMod.NPCs.Bosses.AH.Haruka;
using AAMod.NPCs.Bosses.Shen;
using System;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata;

namespace AAMod
{
    public class AAGlobalProjectile : GlobalProjectile
    {
        public static int CountProjectiles(int Type)
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
                {
                    num++;
                }
            }
            return num;
        }

        public static bool AnyProjectiless(int Type)
        {
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
