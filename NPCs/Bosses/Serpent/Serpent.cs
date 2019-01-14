using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using BaseMod;

namespace AAMod.NPCs.Bosses.Serpent
{
    [AutoloadBossHead]
    public class Serpent : ModNPC
	{
        public bool flies = false;
        public float speed = 30f;
        public float turnSpeed = 0.25f;
        bool TailSpawned = false;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Subzero Serpent");
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }

        public override void SetDefaults()
		{
            npc.damage = 30;
            npc.npcSlots = 7f;
            npc.width = 32;
            npc.height = 76;
            npc.defense = 20;
            npc.lifeMax = 6000;
            npc.aiStyle = 6;
            aiType = -1;
            animationType = 10;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 7, 0, 0);
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            bossBag = mod.ItemType("SerpentBag");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Boss6");
            npc.scale = 1.15f;
        }
        private bool fireAttack;
        private int attackTimer;
        private bool tongue;

        public override void AI()
        {
            int FrameHeight = 88;
            if (Main.rand.Next(10) == 0 && tongue == false)
            {
                tongue = true;
            }
            if (tongue == true)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += FrameHeight;
                    if (npc.frame.Y > (FrameHeight * 3))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                        tongue = false;
                    }
                }
            }
            Player player = Main.player[npc.target];
            float dist = npc.Distance(player.Center);
            if (dist > 300 & Main.rand.Next(20) == 1 && fireAttack == false)
            {
                fireAttack = true;
            }
            if (fireAttack == true)
            {
                attackTimer++;
                if (attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        if (Main.netMode != 1)
                        {
                            int num429 = 1;
                            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
                            {
                                num429 = -1;
                            }
                            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
                            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                            float num433 = 6f;
                            PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
                            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
                            PlayerPos = num433 / PlayerPos;
                            PlayerPosX *= PlayerPos;
                            PlayerPosY *= PlayerPos;
                            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
                            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
                            PlayerPosY += npc.velocity.Y * 0.5f;
                            PlayerPosX += npc.velocity.X * 0.5f;
                            PlayerDistance.X -= PlayerPosX * 1f;
                            PlayerDistance.Y -= PlayerPosY * 1f;
                            Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType("SnowBreath"), npc.damage / 2, 0, Main.myPlayer);
                        }
                    }
                }
                
                if (attackTimer >= 80)
                {
                    fireAttack = false;
                    attackTimer = 0;
                }
            }
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("SnowDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            BaseAI.AIWorm(npc, AAMod.SERPENTTYPES, 10, 6f, 12f, 0.09f, false, false);
        }

        public override void NPCLoot()
		{
            if (!Main.expertMode)
            {
                AAWorld.downedSerpent = true;
                npc.DropLoot(mod.ItemType("SnowMana"), 10, 15);
                string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
                int loot = Main.rand.Next(lootTable.Length);
                if (Main.rand.Next(9) == 0)
                {
                    npc.DropLoot(mod.ItemType("SnowflakeShuriken"), 90, 120); 
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.HealingPotion;   //boss drops
                AAWorld.downedSerpent = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {

                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SnowDust>();
                int dust2 = mod.DustType<Dusts.SnowDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }
    }

    public class SerpentBody : Serpent
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentBody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Subzero Serpent");
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 24; 
            npc.height = 30;
            npc.dontCountMe = true;
            npc.scale = 1.15f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {

            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 20;
                npc.height = 40;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SnowDustLight>();
                int dust2 = mod.DustType<Dusts.SnowDustLight>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }
    }

    public class SerpentTail : Serpent
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentTail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Subzero Serpent");
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 24;
            npc.height = 30;
            npc.dontCountMe = true;
            npc.scale = 1.15f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {

                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SnowDust>();
                int dust2 = mod.DustType<Dusts.SnowDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }
    }

}
