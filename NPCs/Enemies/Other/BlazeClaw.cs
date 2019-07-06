using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.NPCs.Enemies.Other
{
    public class BlazeClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blaze Claw");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 24;
            npc.friendly = false;
            npc.damage = 90;
            npc.defense = 40;
            npc.lifeMax = 800;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = -1;
            npc.noGravity = true;
        }

        public override void AI()
        {
            AAAI.AIClaw(npc, ref npc.ai, false, true, 0.1f, 0.04f, 9f, 5f, 1f, 1f);
            if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 3.14f;
            }

            npc.frameCounter++;
            if (npc.frameCounter >= 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 26;
                if (npc.frame.Y > (26 * 4))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.downedSisters)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.04f;
            }
            return 0;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, mod.DustType<Dusts.AkumaDust>());
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 180);
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DaybreakIncineriteOre"));
        }
    }
}