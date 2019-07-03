using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class IceCrystal : ModNPC
    {
        public override void SetDefaults()
        {
            npc.life = 200;
            npc.defense = 10;
            npc.width = 30;
            npc.height = 30;
            npc.aiStyle = -1;
            npc.alpha = 255;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ice Crystal");
		}

        public override void AI()
        {
            if (npc.ai[3] == 0)
            {
                Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                if (Main.netMode != 1)
                {
                    npc.ai[2]++;
                }
                Vector2 Point = new Vector2(npc.ai[0], npc.ai[1]);
                MoveToPoint(Point, 12);
                if (npc.ai[2] > 180 && Main.netMode != 1)
                {
                    npc.ai = new float[4];
                    npc.ai[3] = 1;
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (Main.netMode != 1)
                {
                    npc.ai[2]++;
                }
                if (npc.alpha > 0)
                {
                    npc.alpha -= 3;
                }
                else
                {
                    npc.alpha = 0;
                }
                float num395 = Main.mouseTextColor / 200f - 0.35f;
                num395 *= 0.2f;
                npc.scale = num395 + 0.95f;
                if (npc.ai[0] != 0f)
                {
                    npc.ai[0] -= 1f;
                    return;
                }
                float num396 = npc.position.X;
                float num397 = npc.position.Y;
                float num398 = 700f;
                bool flag11 = false;
                for (int num399 = 0; num399 < Main.maxPlayers; num399++)
                {
                    float num400 = Main.player[num399].position.X + Main.player[num399].width / 2;
                    float num401 = Main.player[num399].position.Y + Main.player[num399].height / 2;
                    float num402 = Math.Abs(npc.position.X + (float)(npc.width / 2) - num400) + Math.Abs(npc.position.Y + (float)(npc.height / 2) - num401);
                    if (num402 < num398 && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[num399].position, Main.player[num399].width, Main.player[num399].height))
                    {
                        num398 = num402;
                        num396 = num400;
                        num397 = num401;
                        flag11 = true;
                    }
                }
                if (flag11)
                {
                    float num403 = 12f;
                    Vector2 vector29 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num404 = num396 - vector29.X;
                    float num405 = num397 - vector29.Y;
                    float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
                    num406 = num403 / num406;
                    num404 *= num406;
                    num405 *= num406;
                    Projectile.NewProjectile(npc.Center.X - 4f, npc.Center.Y, num404, num405, 227, 20, mod.ProjectileType<IceSpike>(), Main.myPlayer, 0f, 0f);
                    npc.ai[0] = 50f;
                    return;
                }
            }
        }

        public void MoveToPoint(Vector2 point, float moveSpeed)
        {
            if (moveSpeed == 0f || npc.Center == point) return;
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public override void NPCLoot()
        {
            Main.PlaySound(2, npc.position, 50);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (m / pieCut) * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (m /pieCut) * 6.28f);
                Main.dust[dustID].noLight = false;
            }
        }
    }
}
