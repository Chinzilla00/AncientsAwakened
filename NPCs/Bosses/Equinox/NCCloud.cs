using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class NCCloud : ModNPC
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightclawer Cloud");
            Main.npcFrameCount[npc.type] = 4;
		}

        public override void SetDefaults()
        {
            npc.width = 45;
            npc.height = 45;
            npc.scale = 1f;
            npc.noTileCollide = true;
            npc.defense = 50;
            npc.life = 1500;
            npc.damage = 100;
            npc.aiStyle = -1;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public int body = -1;
        public float rotValue = -1f;


        public override void AI()
        {
            Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), .37f, .8f, .89f);
            
            if(npc.ai[0] ++ == 5)
            {
                SpawnDust();
            }

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("NightcrawlerHead"), 120f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC NightcrawlerHead = Main.npc[body];
            if (NightcrawlerHead == null || NightcrawlerHead.life <= 0 || !NightcrawlerHead.active || NightcrawlerHead.type != mod.NPCType("NightcrawlerHead")) { npc.active = false; return; }

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            if (rotValue == -1f) rotValue = npc.ai[3];
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            int aiTimerFire = 0;

            npc.ai[1]++;

            if (npc.ai[3] == 1 || npc.ai[3] == 4 || npc.ai[3] == 7 || npc.ai[3] == 10)
            {
                aiTimerFire = 50;
            }
            if (npc.ai[3] == 2 || npc.ai[3] == 5 || npc.ai[3] == 8 || npc.ai[3] == 11)
            {
                aiTimerFire = 100;
            }
            if (npc.ai[3] == 3 || npc.ai[3] == 6 || npc.ai[3] == 9 || npc.ai[3] == 12)
            {
                aiTimerFire = 150;
            }

            if (npc.ai[1] >= 150)
            {
                npc.ai[1] = 0;
            }

            npc.Center = BaseUtility.RotateVector(NightcrawlerHead.Center, NightcrawlerHead.Center + new Vector2(140, 0f), rotValue);

            if (npc.ai[1] == aiTimerFire)
            {
                Vector2 speed = new Vector2(1f, 0f).RotatedBy((float)(Main.rand.NextDouble() * 3.1415f)) * 6f;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, mod.ProjectileType("NightcrawlerNothing"), npc.damage, 0, Main.myPlayer);
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawAfterimage(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.oldPos, npc.scale, npc.rotation, npc.direction, 9, npc.frame, 1f, 1f, 7, true, 0, 0, Color.White);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE));
            return false;
        }

        public override void NPCLoot()
        {
            SpawnDust();
            npc.active = false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(163, 60);
        }

        public void SpawnDust()
        {
            Vector2 position = npc.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }
    }
}