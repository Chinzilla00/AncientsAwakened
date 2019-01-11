using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.GripsShen
{
    public class DiscordianBurst : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Discordian Burst");
            Main.projFrames[projectile.type] = 3;
        }
    	
        public override void SetDefaults()
        {
            projectile.width = 160;
            projectile.height = 160;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.timeLeft = 3;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.hide = true;
        }

        public override void AI()
        {
            projectile.ai[1] += 0.01f;
            projectile.scale = projectile.ai[1];
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= (float)(3 * Main.projFrames[projectile.type]))
            {
                projectile.Kill();
                return;
            }
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.hide = true;
                }
            }
            projectile.alpha -= 63;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            bool flag56 = projectile.type == 612;
            bool flag57 = projectile.type == 624;
            if (flag56)
            {
                Lighting.AddLight(projectile.Center, 0.9f, 0.8f, 0.6f);
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.position = projectile.Center;
                projectile.width = (projectile.height = (int)(52f * projectile.scale));
                projectile.Center = projectile.position;
                projectile.Damage();
                if (flag56)
                {
                    Main.PlaySound(SoundID.Item14, projectile.position);
                    for (int num980 = 0; num980 < 4; num980++)
                    {
                        int num981 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[num981].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                    }
                    for (int num982 = 0; num982 < 10; num982++)
                    {
                        int num983 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 200, default(Color), 2.7f);
                        Main.dust[num983].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                        Main.dust[num983].noGravity = true;
                        Main.dust[num983].velocity *= 3f;
                        num983 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[num983].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                        Main.dust[num983].velocity *= 2f;
                        Main.dust[num983].noGravity = true;
                        Main.dust[num983].fadeIn = 2.5f;
                    }
                    for (int num984 = 0; num984 < 5; num984++)
                    {
                        int num985 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 2.7f);
                        Main.dust[num985].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2)) * (float)projectile.width / 2f;
                        Main.dust[num985].noGravity = true;
                        Main.dust[num985].velocity *= 3f;
                    }
                    for (int num986 = 0; num986 < 10; num986++)
                    {
                        int num987 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num987].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2)) * (float)projectile.width / 2f;
                        Main.dust[num987].noGravity = true;
                        Main.dust[num987].velocity *= 3f;
                    }
                }
                if (flag57)
                {
                    Main.PlaySound(SoundID.Item14, projectile.position);
                    for (int num988 = 0; num988 < 20; num988++)
                    {
                        int num989 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[num989].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                        Main.dust[num989].velocity *= 2f;
                        Main.dust[num989].noGravity = true;
                        Main.dust[num989].fadeIn = 2.5f;
                        Main.dust[num989].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].cPet, Main.player[projectile.owner]);
                    }
                    for (int num990 = 0; num990 < 15; num990++)
                    {
                        int num991 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 2.7f);
                        Main.dust[num991].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2)) * (float)projectile.width / 2f;
                        Main.dust[num991].noGravity = true;
                        Main.dust[num991].velocity *= 3f;
                        Main.dust[num991].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].cPet, Main.player[projectile.owner]);
                    }
                    float num992 = (float)Main.rand.NextDouble() * 6.28318548f;
                    float num993 = (float)Main.rand.NextDouble() * 6.28318548f;
                    float num994 = (float)Main.rand.NextDouble() * 6.28318548f;
                    float num995 = 7f + (float)Main.rand.NextDouble() * 7f;
                    float num996 = 7f + (float)Main.rand.NextDouble() * 7f;
                    float num997 = 7f + (float)Main.rand.NextDouble() * 7f;
                    float num998 = num995;
                    if (num996 > num998)
                    {
                        num998 = num996;
                    }
                    if (num997 > num998)
                    {
                        num998 = num997;
                    }
                    for (int num999 = 0; num999 < 200; num999++)
                    {
                        int num1000 = mod.DustType<Dusts.Discord>();
                        float scaleFactor14 = num998;
                        if (num999 > 50)
                        {
                            scaleFactor14 = num996;
                        }
                        if (num999 > 100)
                        {
                            scaleFactor14 = num995;
                        }
                        if (num999 > 150)
                        {
                            scaleFactor14 = num997;
                        }
                        int num1001 = Dust.NewDust(projectile.position, 6, 6, num1000, 0f, 0f, 100, default(Color), 1f);
                        Vector2 vector123 = Main.dust[num1001].velocity;
                        Main.dust[num1001].position = projectile.Center;
                        vector123.Normalize();
                        vector123 *= scaleFactor14;
                        if (num999 > 150)
                        {
                            vector123.Y *= 0.5f;
                            vector123 = vector123.RotatedBy((double)num994, default(Vector2));
                        }
                        else if (num999 > 100)
                        {
                            vector123.X *= 0.5f;
                            vector123 = vector123.RotatedBy((double)num992, default(Vector2));
                        }
                        else if (num999 > 50)
                        {
                            vector123.Y *= 0.5f;
                            vector123 = vector123.RotatedBy((double)num993, default(Vector2));
                        }
                        Main.dust[num1001].velocity *= 0.2f;
                        Main.dust[num1001].velocity += vector123;
                        Main.dust[num1001].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].cPet, Main.player[projectile.owner]);
                        if (num999 <= 200)
                        {
                            Main.dust[num1001].scale = 2f;
                            Main.dust[num1001].noGravity = true;
                            Main.dust[num1001].fadeIn = Main.rand.NextFloat() * 2f;
                            if (Main.rand.Next(4) == 0)
                            {
                                Main.dust[num1001].fadeIn = 2.5f;
                            }
                            Main.dust[num1001].noLight = true;
                            if (num999 < 100)
                            {
                                Main.dust[num1001].position += Main.dust[num1001].velocity * 20f;
                                Main.dust[num1001].velocity *= -1f;
                            }
                        }
                    }
                    return;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.Damage();
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int num74 = 0; num74 < 4; num74++)
            {
                int num75 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num75].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
            }
            for (int num76 = 0; num76 < 30; num76++)
            {
                int num77 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 200, default(Color), 3.7f);
                Main.dust[num77].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                Main.dust[num77].noGravity = true;
                Main.dust[num77].velocity *= 3f;
                Main.dust[num77].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].ArmorSetDye(), Main.player[projectile.owner]);
                num77 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num77].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                Main.dust[num77].velocity *= 2f;
                Main.dust[num77].noGravity = true;
                Main.dust[num77].fadeIn = 2.5f;
                Main.dust[num77].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].ArmorSetDye(), Main.player[projectile.owner]);
            }
            for (int num78 = 0; num78 < 10; num78++)
            {
                int num79 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 2.7f);
                Main.dust[num79].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2)) * (float)projectile.width / 2f;
                Main.dust[num79].noGravity = true;
                Main.dust[num79].velocity *= 3f;
                Main.dust[num79].shader = GameShaders.Armor.GetSecondaryShader(Main.player[projectile.owner].ArmorSetDye(), Main.player[projectile.owner]);
            }
            for (int num80 = 0; num80 < 10; num80++)
            {
                int num81 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num81].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2)) * (float)projectile.width / 2f;
                Main.dust[num81].noGravity = true;
                Main.dust[num81].velocity *= 3f;
            }
            for (int num82 = 0; num82 < 2; num82++)
            {
                int num83 = Gore.NewGore(projectile.position + new Vector2((float)(projectile.width * Main.rand.Next(100)) / 100f, (float)(projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num83].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)projectile.width / 2f;
                Main.gore[num83].velocity *= 0.3f;
                Gore expr_3121_cp_0 = Main.gore[num83];
                expr_3121_cp_0.velocity.X = expr_3121_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
                Gore expr_314F_cp_0 = Main.gore[num83];
                expr_314F_cp_0.velocity.Y = expr_314F_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DiscordInferno>(), 300);
        }
    }
}