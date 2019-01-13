using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Hallow
{
    public class FatPixie : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fat Pixie");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 500;   //boss life
            npc.damage = 30;  //boss damage
            npc.defense = 30;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 0, 75, 45);
            npc.aiStyle = -1;
            npc.width = 60;
            npc.height = 36;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneHoly && Main.hardMode ? .7f : 0f;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (npc.velocity.Y == 0)
            {
                npc.frame.Y = 0;
            }
            else
            {
                if (npc.velocity.Y < 0)
                {
                    npc.frame.Y = 36;
                }
                else
                if (npc.velocity.Y > 0)
                {
                    npc.frame.Y = 36 * 2;
                }
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }else
            {
                npc.spriteDirection = 1;
            }
            BaseAI.AISlime(npc, ref npc.ai, false, 200, 2f, 6f, 3f, 8f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.PixieDust, Main.rand.Next(5, 7));
        }
    }
}


