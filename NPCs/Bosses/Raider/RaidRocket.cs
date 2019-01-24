using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Raider
{
    public class RaidRocket : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public bool PlayerHit = false;
        public bool TileHit = false;

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 30;
            npc.damage = 60;
            npc.defense = 16;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.value = 0f;
            npc.noTileCollide = false;
            npc.noGravity = true;
            npc.npcSlots = 1.5f;
            npc.canGhostHeal = false;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            npc.TargetClosest(false);
            npc.rotation = npc.velocity.ToRotation();
            if (Math.Sign(npc.velocity.X) != 0)
            {
                npc.spriteDirection = -Math.Sign(npc.velocity.X);
            }
            if (npc.rotation < -1.57079637f)
            {
                npc.rotation += 3.14159274f;
            }
            if (npc.rotation > 1.57079637f)
            {
                npc.rotation -= 3.14159274f;
            }
            float num997 = 0.4f;
            float num998 = 10f;
            float scaleFactor3 = 200f;
            float num999 = 750f;
            float num1000 = 30f;
            float num1001 = 30f;
            float scaleFactor4 = 0.95f;
            int num1002 = 50;
            float scaleFactor5 = 14f;
            float num1003 = 30f;
            float num1004 = 100f;
            float num1005 = 20f;
            float num1006 = 0f;
            float num1007 = 7f;
            bool flag63 = true;
            num1006 *= num1005;
            if (Main.expertMode)
            {
                num997 *= Main.expertKnockBack;
            }
            if (npc.collideX || npc.collideY)
            {
                TileHit = true;
                npc.life = 0;
            }
            if (npc.ai[0] != 3f)
            {
                int num1008 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num1008].noGravity = true;
                Main.dust[num1008].velocity = npc.velocity / 5f;
                Vector2 vector125 = new Vector2(-10f, 10f);
                if (npc.spriteDirection == 1)
                {
                    vector125.X *= -1f;
                }
                vector125 = vector125.RotatedBy((double)npc.rotation, default(Vector2));
                Main.dust[num1008].position = npc.Center + vector125;
            }
            if (npc.ai[0] == 0f)
            {
                npc.knockBackResist = num997;
                float scaleFactor6 = num998;
                Vector2 center4 = npc.Center;
                Vector2 center5 = Main.player[npc.target].Center;
                Vector2 vector126 = center5 - center4;
                Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
                float num1013 = vector126.Length();
                vector126 = Vector2.Normalize(vector126) * scaleFactor6;
                vector127 = Vector2.Normalize(vector127) * scaleFactor6;
                bool flag64 = Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1);
                if (npc.ai[3] >= 120f)
                {
                    flag64 = true;
                }
                float num1014 = 8f;
                flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
                if (num1013 > num999 || !flag64)
                {
                    npc.velocity.X = (npc.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
                    npc.velocity.Y = (npc.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
                    if (!flag64)
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] == 120f)
                        {
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.ai[3] = 0f;
                    }
                }
                else
                {
                    npc.ai[0] = 1f;
                    npc.ai[2] = vector126.X;
                    npc.ai[3] = vector126.Y;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.knockBackResist = 0f;
                npc.velocity *= scaleFactor4;
                npc.ai[1] += 1f;
                if (npc.ai[1] >= num1001)
                {
                    npc.ai[0] = 2f;
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                    Vector2 velocity = new Vector2(npc.ai[2], npc.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
                    velocity.Normalize();
                    velocity *= scaleFactor5;
                    npc.velocity = velocity;
                }
                if (Main.rand.Next(4) == 0)
                {
                    int num1015 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num1015].noGravity = true;
                    Main.dust[num1015].velocity *= 2f;
                    Main.dust[num1015].velocity = Main.dust[num1015].velocity / 2f + Vector2.Normalize(Main.dust[num1015].position - npc.Center);
                }
            }
            else if (npc.ai[0] == 2f)
            {
                npc.knockBackResist = 0f;
                float num1016 = num1003;
                npc.ai[1] += 1f;
                bool flag65 = Vector2.Distance(npc.Center, Main.player[npc.target].Center) > num1004 && npc.Center.Y > Main.player[npc.target].Center.Y;
                if ((npc.ai[1] >= num1016 && flag65) || npc.velocity.Length() < num1007)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.velocity /= 2f;
                    npc.netUpdate = true;
                }
                else
                {
                    Vector2 center6 = npc.Center;
                    Vector2 center7 = Main.player[npc.target].Center;
                    Vector2 vec2 = center7 - center6;
                    vec2.Normalize();
                    if (vec2.HasNaNs())
                    {
                        vec2 = new Vector2((float)npc.direction, 0f);
                    }
                    npc.velocity = (npc.velocity * (num1005 - 1f) + vec2 * (npc.velocity.Length() + num1006)) / num1005;
                }
                if (flag63 && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[0] = 3f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 4f)
            {
                npc.ai[1] -= 3f;
                if (npc.ai[1] <= 0f)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                }
                npc.velocity *= 0.95f;
            }
            if (flag63 && npc.ai[0] != 3f && Vector2.Distance(npc.Center, Main.player[npc.target].Center) < 64f)
            {
                npc.ai[0] = 3f;
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
                npc.ai[3] = 0f;
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 3f)
            {
                npc.position = npc.Center;
                npc.width = (npc.height = 192);
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                npc.velocity = Vector2.Zero;
                npc.damage = (int)(80f * Main.damageMultiplier);
                npc.alpha = 255;
                Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.2f, 0.7f, 1.1f);
                for (int num1017 = 0; num1017 < 10; num1017++)
                {
                    int num1018 = Dust.NewDust(npc.position, npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num1018].velocity *= 1.4f;
                    Main.dust[num1018].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                }
                for (int num1019 = 0; num1019 < 40; num1019++)
                {
                    int num1020 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num1020].noGravity = true;
                    Main.dust[num1020].velocity *= 2f;
                    Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                    Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                    if (Main.rand.Next(2) == 0)
                    {
                        num1020 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.9f);
                        Main.dust[num1020].noGravity = true;
                        Main.dust[num1020].velocity *= 1.2f;
                        Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                        Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        num1020 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.7f);
                        Main.dust[num1020].velocity *= 1.2f;
                        Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                        Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                    }
                }
                npc.ai[1] += 1f;
                if (npc.ai[1] >= 3f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    return;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num236 = 0;
            while ((double)num236 < damage / (double)(npc.lifeMax * 50))
            {
                int num237 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), (float)(-1 * hitDirection), -1f, 0, default(Color), 1f);
                Main.dust[num237].position = Vector2.Lerp(Main.dust[num237].position, npc.Center, 0.25f);
                Main.dust[num237].scale = 0.5f;
                num236++;
            }
            PlayerHit = true;
            npc.life = 0;
        }

        public override void NPCLoot()
        {
            if (TileHit)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 20, npc.velocity.X, npc.velocity.Y, mod.ProjectileType("RaidStrike"), npc.damage, 1, 255);
            }
            if (PlayerHit)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 20, npc.velocity.X, npc.velocity.Y, mod.ProjectileType("RaidBoom"), npc.damage, 1, 255);
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 28;
                if (npc.frame.Y > (28 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
            return true;
        }
    }
}