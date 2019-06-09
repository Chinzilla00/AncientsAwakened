using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class BunnyBattler : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rabbid Rabbit");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 40;
            npc.aiStyle = -1;
            npc.damage = 90;
            npc.defense = 40;
            npc.lifeMax = 300;
            npc.knockBackResist = 0f;
            npc.npcSlots = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.aiStyle = -1;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool isDead = npc.life <= 0;
            if (isDead)          //this make so when the npc has 0 life(dead) he will spawn this
            {

            }
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), (isDead ? 2f : 1.5f));
            }
        }

        public override void AI()
        {
            BaseAI.AISlime(npc, ref npc.ai, true, 25, 6f, -8f, 6f, -10f);
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y < 0)
            {
                npc.frame.Y = frameHeight * 4;
            }
            else if (npc.velocity.Y > 0)
            {
                npc.frame.Y = frameHeight * 5;
            }
            else if (npc.ai[0] < -15f)
            {
                npc.frame.Y = 0;
            }
            else if (npc.ai[0] > -15f)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.ai[0] > -10f)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.ai[0] > -5f)
            {
                npc.frame.Y = frameHeight * 3;
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void PostAI()
        {
            if (NPC.AnyNPCs(mod.NPCType<Rajah>()))
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 5;
                }
                else
                {
                    npc.alpha = 0;
                }
            }
            else
            {
                npc.dontTakeDamage = true;
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.active = false;
                }
            }
        }
    }
}