using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Time : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 12;
        }
        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.penetrate = -1;  
            projectile.width = 20;
            projectile.height = 22;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 150;
        }
        private Color Gold = Color.Goldenrod;
        public bool AM;
        public bool PM;

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.ai[0] != 0)
            {
                return TimeColor();
            }
            else
            {
                return Gold;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] != 0)
            {
                int Buff;
                if (Main.dayTime)
                {
                    Buff = BuffID.Daybreak;
                }
                else
                {
                    Buff = mod.BuffType<Buffs.Moonraze>();
                }

                target.AddBuff(Buff, 180);
            }
        }

        public override void AI()
        {
            FindFrame();
            if (projectile.ai[0] != 0)
            {
                if (Main.dayTime)
                {
                    Lighting.AddLight(projectile.Center, new Vector3(Color.OrangeRed.R / 255f, Color.OrangeRed.G / 255, Color.OrangeRed.B / 255f));
                }
                else
                {
                    Lighting.AddLight(projectile.Center, new Vector3(Color.Indigo.R / 255f, Color.Indigo.G / 255, Color.Indigo.B / 255f));
                }
            }
            else
            {
                Lighting.AddLight(projectile.Center, new Vector3(Gold.R / 255f, Gold.G / 255f, Gold.B / 255f));
            }
            projectile.velocity *= 0.985f;
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 30f)
            {
                projectile.alpha += 10;
                if (projectile.alpha >= 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                    return;
                }
            }
        }

        public void FindFrame()
        {
            double num4 = (float)Main.time;
            if (!Main.dayTime)
            {
                num4 += 54000.0;
            }
            num4 = num4 / 86400.0 * 24.0;
            double num5 = 7.5;
            num4 = num4 - num5 - 12.0;
            if (num4 < 0.0)
            {
                num4 += 24.0;
            }
            if (num4 > 1)
            {
                projectile.frame = 0;
            }
            else if (num4 > 2)
            {
                projectile.frame = 1;
            }
            else if (num4 > 3)
            {
                projectile.frame = 2;
            }
            else if (num4 > 4)
            {
                projectile.frame = 3;
            }
            else if (num4 > 5)
            {
                projectile.frame = 4;
            }
            else if (num4 > 6)
            {
                projectile.frame = 5;
            }
            else if (num4 > 7)
            {
                projectile.frame = 6;
            }
            else if (num4 > 8)
            {
                projectile.frame = 7;
            }
            else if (num4 > 9)
            {
                projectile.frame = 8;
            }
            else if (num4 > 10)
            {
                projectile.frame = 9;
            }
            else if (num4 > 11)
            {
                projectile.frame = 10;
            }
            else if (num4 > 12)
            {
                projectile.frame = 11;
            }
            else if (num4 > 13)
            {
                projectile.frame = 0;
            }
            else if (num4 > 14)
            {
                projectile.frame = 1;
            }
            else if (num4 > 15)
            {
                projectile.frame = 2;
            }
            else if (num4 > 16)
            {
                projectile.frame = 3;
            }
            else if (num4 > 17)
            {
                projectile.frame = 4;
            }
            else if (num4 > 18)
            {
                projectile.frame = 5;
            }
            else if (num4 > 19)
            {
                projectile.frame = 6;
            }
            else if (num4 > 20)
            {
                projectile.frame = 7;
            }
            else if (num4 > 21)
            {
                projectile.frame = 8;
            }
            else if (num4 > 22)
            {
                projectile.frame = 9;
            }
            else if (num4 > 23)
            {
                projectile.frame = 10;
            }
            else
            {
                projectile.frame = 11;
            }
        }

        public static Color TimeColor()
        {
            double num4 = (float)Main.time;
            if (!Main.dayTime)
            {
                num4 += 54000.0;
            }
            num4 = num4 / 86400.0 * 24.0;
            double num5 = 7.5;
            num4 = num4 - num5 - 12.0;
            if (num4< 0.0)
            {
                num4 += 24.0;
            }
            if (num4 >= 12.0)
            {
                return Color.Indigo;
            }
            else
            {
                return Color.OrangeRed;
            }
        }

    }
}
