using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Minibosses
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Exterminator : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Exterminator");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.SolarCorite];
		}

		public override void SetDefaults()
		{
            npc.width = 38;
            npc.height = 38;
            npc.aiStyle = -1;
            npc.damage = 20;
            npc.defense = 12;
            npc.lifeMax = 200;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0;
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
            npc.spriteDirection = Math.Sign(npc.velocity.X);
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
            num997 = 0.3f;
            num998 = 8f;
            scaleFactor3 = 300f;
            num999 = 800f;
            num1000 = 60f;
            num1001 = 5f;
            scaleFactor4 = 0.8f;
            num1002 = 0;
            scaleFactor5 = 10f;
            num1003 = 30f;
            num1004 = 150f;
            num1005 = 60f;
            num1006 = 0.333333343f;
            num1007 = 8f;
            flag63 = false;
            num1006 *= num1005;
            int num1009 = (npc.ai[0] == 2f) ? 2 : 1;
            int num1010 = (npc.ai[0] == 2f) ? 30 : 20;
            for (int num1011 = 0; num1011 < 2; num1011++)
            {
                if (Main.rand.Next(3) < num1009)
                {
                    int num1012 = Dust.NewDust(npc.Center - new Vector2(num1010), num1010 * 2, num1010 * 2, mod.DustType<Dusts.VoidDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 90, default(Color), 1.5f);
                    Main.dust[num1012].noGravity = true;
                    Main.dust[num1012].velocity *= 0.2f;
                    Main.dust[num1012].fadeIn = 1f;
                }
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
                    Vector2 velocity = new Vector2(npc.ai[2], npc.ai[3]) + new Vector2(Main.rand.Next(-num1002, num1002 + 1), Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
                    velocity.Normalize();
                    velocity *= scaleFactor5;
                    npc.velocity = velocity;
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
                    npc.ai[1] = 45f;
                    npc.ai[0] = 4f;
                }
                else
                {
                    Vector2 center6 = npc.Center;
                    Vector2 center7 = Main.player[npc.target].Center;
                    Vector2 vec2 = center7 - center6;
                    vec2.Normalize();
                    if (vec2.HasNaNs())
                    {
                        vec2 = new Vector2(npc.direction, 0f);
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
                npc.position.X = npc.position.X - npc.width / 2;
                npc.position.Y = npc.position.Y - npc.height / 2;
                npc.velocity = Vector2.Zero;
                npc.damage = (int)(80f * Main.damageMultiplier);
                npc.alpha = 255;
                Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 1.1f, 0.3f, 0.3f);
                for (int num1017 = 0; num1017 < 10; num1017++)
                {
                    int num1018 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num1018].velocity *= 1.4f;
                    Main.dust[num1018].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                }
                for (int num1019 = 0; num1019 < 40; num1019++)
                {
                    int num1020 = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num1020].noGravity = true;
                    Main.dust[num1020].velocity *= 2f;
                    Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                    Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                    if (Main.rand.Next(2) == 0)
                    {
                        num1020 = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 0.9f);
                        Main.dust[num1020].noGravity = true;
                        Main.dust[num1020].velocity *= 1.2f;
                        Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                        Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        num1020 = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 0.7f);
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

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneVoid ? .01f : 0f;
        }

        public override void NPCLoot()
        {
            string[] lootTable = { "DoomStaff", "Voidsaber", "ProbeControlUnit", "DoomGun" };
            int loot = Main.rand.Next(lootTable.Length);
            npc.DropLoot(mod.ItemType(lootTable[loot]));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float num69 = 0 * Main.npc[npc.type].scale;
            Vector2 vector10 = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Color color9 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            if ((npc.ai[0] == 2f || npc.ai[0] == 4f))
            {
                Texture2D texture2D32 = mod.GetTexture("NPCs/Minibosses/ExterminatorCharge");
                Vector2 origin16 = new Vector2(texture2D32.Width / 2, texture2D32.Height / 8 + 14);
                int num210 = (int)npc.ai[1] / 2;
                float num211 = -1.57079637f * npc.spriteDirection;
                float num212 = npc.ai[1] / 45f;
                if (num212 > 1f)
                {
                    num212 = 1f;
                }
                num210 %= 4;
                for (int num213 = 6; num213 >= 0; num213--)
                {
                    Vector2 arg_B3E4_0 = npc.oldPos[num213];
                    Color color47 = Color.Lerp(Color.Red, Color.DarkRed, num212);
                    color47 = Color.Lerp(color47, Color.Blue, num213 / 12f);
                    color47.A = (byte)(64f * num212);
                    color47.R = (byte)(color47.R * (10 - num213) / 20);
                    color47.G = (byte)(color47.G * (10 - num213) / 20);
                    color47.B = (byte)(color47.B * (10 - num213) / 20);
                    color47.A = (byte)(color47.A * (10 - num213) / 20);
                    color47 *= num212;
                    int num214 = (num210 - num213) % 4;
                    if (num214 < 0)
                    {
                        num214 += 4;
                    }
                    Rectangle value36 = texture2D32.Frame(1, 4, 0, num214);
                    Main.spriteBatch.Draw(texture2D32, new Vector2(npc.oldPos[num213].X - Main.screenPosition.X + npc.width / 2 - Main.npcTexture[npc.type].Width * npc.scale / 2f + vector10.X * npc.scale, npc.oldPos[num213].Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + vector10.Y * npc.scale + num69), new Rectangle?(value36), color47, npc.oldRot[num213] + num211, origin16, MathHelper.Lerp(0.1f, 1.2f, (10f - num213) / 10f), SpriteEffects.None, 0f);
                }
            }
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/Exterminator_Glow"), npc.Bottom - Main.screenPosition + new Vector2(-Main.npcTexture[npc.type].Width * npc.scale / 2f + vector10.X * npc.scale, -Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + vector10.Y * npc.scale + num69 + npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(npc.frame), new Microsoft.Xna.Framework.Color(255 - npc.alpha, 255 - npc.alpha, 255 - npc.alpha, 255 - npc.alpha), npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
            float scaleFactor14 = 0.25f + (npc.GetAlpha(color9).ToVector3() - new Vector3(0.5f)).Length() * 0.25f;
            for (int num247 = 0; num247 < 4; num247++)
            {
                Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/Exterminator_Glow"), npc.Bottom - Main.screenPosition + new Vector2(-Main.npcTexture[npc.type].Width * npc.scale / 2f + vector10.X * npc.scale, -Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + vector10.Y * npc.scale + num69 + npc.gfxOffY) + npc.velocity.RotatedBy(num247 * 1.57079637f, default(Vector2)) * scaleFactor14, new Microsoft.Xna.Framework.Rectangle?(npc.frame), new Microsoft.Xna.Framework.Color(64, 64, 64, 0), npc.rotation, vector10, npc.scale, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}
