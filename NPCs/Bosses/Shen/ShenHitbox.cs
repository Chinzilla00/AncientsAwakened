using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenHitbox : ModNPC
    {
        public override string Texture => "AAMod/BlankTex";

        public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1);
            npc.damage = (int)(npc.damage * .8f);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon; Discordian Doomsayer");
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.damage = 110;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.value = 0f;
            npc.knockBackResist = 0.0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;
        }
        public override bool PreAI()
        {
            npc.TargetClosest(true);
            int boss = (int)npc.ai[0];
            if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != mod.NPCType("Shen"))
            {
                npc.active = false;
                return false;
            }
            npc.netUpdate = true;
            npc.position.X = Main.npc[boss].Center.X - 50;
            npc.position.Y = Main.npc[boss].position.Y;
            return false;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}