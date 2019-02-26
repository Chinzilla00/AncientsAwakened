using BaseMod;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Djinn
{
    [AutoloadBossHead]
    public class DjinnCo : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Djinn");
			Main.npcFrameCount[npc.type] = 15;
		}

		public override void SetDefaults()
		{
            npc.width = 70;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 25;
            npc.lifeMax = 6000;
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.knockBackResist = 0f;
            npc.value = (float)Item.buyPrice(0, 8, 0, 0);
            npc.buffImmune[20] = true;
            npc.buffImmune[44] = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Djinn");
            bossBag = mod.ItemType<Items.Boss.Djinn.DjinnBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }
        
        public int runonce = 0;
        public int FrameHeight = 130;


        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (runonce == 0)
            {
                StartSandstorm();
                runonce += 1;
            }

            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }
            
            npc.ai[3]++;
            if (npc.ai[3] <= 300)
            {
                npc.frameCounter++;
                if (npc.frameCounter > 9)
                {
                    npc.frame.Y += 130;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y > FrameHeight * 5)
                {
                    npc.frame.Y = 0;
                }
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.1f, 0.04f, 4f, 1.5f, false, 300);
            }
            else
            {
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                if (npc.ai[3] > 300)
                {
                    npc.frame.Y = FrameHeight * 6;
                }
                if (npc.ai[3] > 309)
                {
                    npc.frame.Y = FrameHeight * 7;
                }
                if (npc.ai[3] == 309)
                {
                    fireProjectile();
                }
                if (npc.ai[3] > 318)
                {
                    npc.frame.Y = FrameHeight * 8;
                }
                if (npc.ai[3] > 327)
                {
                    npc.frame.Y = FrameHeight * 9;
                }
                if (npc.ai[3] > 336)
                {
                    npc.frame.Y = FrameHeight * 10;
                }
                if (npc.ai[3] == 336)
                {
                    fireProjectile();
                }
                if (npc.ai[3] > 345)
                {
                    npc.frame.Y = FrameHeight * 11;
                }
                if (npc.ai[3] > 354)
                {
                    npc.frame.Y = FrameHeight * 12;
                }
                if (npc.ai[3] > 363)
                {
                    npc.frame.Y = FrameHeight * 13;
                }
                if (npc.ai[3] == 372)
                {
                    fireProjectile();
                }
                if (npc.ai[3] > 372)
                {
                    npc.frame.Y = FrameHeight * 14;
                }
                if (npc.ai[3] > 381)
                {
                    npc.ai[3] = 0;
                }
            }

            if (!player.InZone("Desert") || player.dead || !Main.dayTime)
            {
                npc.TargetClosest(true);
                if (!player.InZone("Desert") || player.dead || !Main.dayTime)
                {
                    npc.alpha += 2;
                }
            }
            else
            {
                Sandstorm.TimeLeft = 10;
                npc.alpha -= 2;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
            }

            if (npc.alpha >= 255)
            {
                npc.active = false;
            }
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
        }

        public void fireProjectile()
        {
            List<Point> list4 = new List<Point>();
            Vector2 vec5 = Main.player[npc.target].Center + new Vector2(Main.player[npc.target].velocity.X * 30f, 0f);
            Point point14 = vec5.ToTileCoordinates();
            int num1468 = 0;
            while (num1468 < 1000 && list4.Count < 1)
            {
                bool flag118 = false;
                int num1469 = Main.rand.Next(point14.X - 30, point14.X + 30 + 1);
                foreach (Point current in list4)
                {
                    if (Math.Abs(current.X - num1469) < 10)
                    {
                        flag118 = true;
                        break;
                    }
                }
                if (!flag118)
                {
                    int startY = point14.Y - 20;
                    int num1470;
                    int num1471;
                    Collision.ExpandVertically(num1469, startY, out num1470, out num1471, 1, 51);
                    if (StrayMethods.CanSpawnSandstormHostile(new Vector2((float)num1469, (float)(num1471 - 15)) * 16f, 15, 15))
                    {
                        list4.Add(new Point(num1469, num1471 - 15));
                    }
                }
                num1468++;
            }
            foreach (Point current2 in list4)
            {
                Projectile.NewProjectile((float)(current2.X * 16), (float)(current2.Y * 16), 0f, 0f, 658, 0, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            npc.position.X = npc.position.X + (float)(npc.width / 2);
            npc.position.Y = npc.position.Y + (float)(npc.height / 2);
            npc.position.X = npc.position.X - (float)(npc.width / 2);
            npc.position.Y = npc.position.Y - (float)(npc.height / 2);
            int dust = mod.DustType<Dusts.SandDust>();
            for (int Loop = 0; Loop < 5; Loop++)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].velocity.Y = hitDirection * 0.1F;
                Main.dust[dust].noGravity = false;
            }
            if (npc.life <= 0)
            {
                for (int Loop = 0; Loop < 5; Loop++)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust].velocity.X *= 0f;
                    Main.dust[dust].noGravity = false;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
            AAWorld.downedDjinn = true;
        }

        public override void NPCLoot()
        {
            Sandstorm.TimeLeft = 0;
            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("DesertMana"), 10, 15);
                string[] lootTable = { "Djinnerang", "SandLamp", "SandScepter", "SandstormCrossbow", "SultanScimitar" };
                int loot = Main.rand.Next(lootTable.Length);
                if (Main.rand.Next(6) == 0)
                {
                    npc.DropLoot(mod.ItemType("Sandagger"), 90, 120);
                }
                else
                {
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
        }

        private static void StartSandstorm()
        {
            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
            ChangeSeverityIntentions();
        }

        private static void ChangeSeverityIntentions()
        {
            if (Sandstorm.Happening)
            {
                Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
            }
            else if (Main.rand.Next(3) == 0)
            {
                Sandstorm.IntendedSeverity = 0f;
            }
            else
            {
                Sandstorm.IntendedSeverity = Main.rand.NextFloat() * 0.3f;
            }
            if (Main.netMode != 1)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }

    }

    
}
