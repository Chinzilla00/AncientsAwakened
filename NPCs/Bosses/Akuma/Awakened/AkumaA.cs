using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;
using System.IO;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AkumaA : Akuma
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaA";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 3;
        }

		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.damage = 80;
            npc.defense = 270;
            npc.lifeMax = 700000;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2");
            bossBag = mod.ItemType("AkumaBag");
            if (AAWorld.downedAllAncients)
            {
                npc.damage = 90;
                npc.defense = 280;
                npc.lifeMax = 750000;
            }
            isAwakened = true;
        }
    }

    [AutoloadBossHead]
    public class AkumaAArms : AkumaA
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaAArms";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.damage = 80;
            npc.defense = 270;
            npc.lifeMax = 650000;
            npc.value = Item.sellPrice(2, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.behindTiles = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
            musicPriority = MusicPriority.BossHigh;
            npc.dontTakeDamage = true;
            npc.width = 60;
            npc.height = 60;
            npc.dontCountMe = true;
            if (AAWorld.downedAllAncients)
            {
                npc.damage = 61;
                npc.defense = 180;
                npc.lifeMax = 700000;
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

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100);
                    Main.dust[num935].scale = 2f;
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("AkumaA"))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.penetrate > 1)
            {
                damage = (int)(damage * .5f);
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                return false;
            }
            npc.active = false;
            return true;
        }
    }

    [AutoloadBossHead]
    public class AkumaABody : AkumaAArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaABody";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    [AutoloadBossHead]
    public class AkumaABody1 : AkumaAArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaABody1";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    [AutoloadBossHead]
    public class AkumaATail : AkumaAArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaATail";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}
