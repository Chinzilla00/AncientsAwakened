using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod
{
	public class DreadHydraHandler : ParentNPC
	{

        public override string Texture { get { return "AAMod/BlankTex"; } }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return G_CanSpawn(spawnInfo.spawnTileX, spawnInfo.spawnTileY, npc.type, spawnInfo.player, spawnInfo) ? 1f : 0f;
        }



        public virtual void G_HitEffect(int hitDirection, double damage, bool isDead)
        {
        }
        public virtual bool G_CanSpawn(int x, int y, int type, Player player, NPCSpawnInfo info)
        {
            return G_CanSpawn(x, y, type, player);
        }
        public virtual bool G_CanSpawn(int x, int y, int type, Player player)
        {
            return false;
        }
    }
}