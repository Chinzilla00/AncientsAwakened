using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class Solar : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
            DisplayName.SetDefault("Solar");
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 2;
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 60f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 1000f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 17f;
        }

        public override void AI()
        {
            Dust dust1;
            Dust dust2;
            Vector2 position = projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default, 1f)];
            dust2 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust1.noGravity = true;
            dust2.noGravity = true;

            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (Main.rand.Next(35) == 0)
                {
                    float num78 = Main.mouseX + Main.screenPosition.X - projectile.Center.X;
                    float num79 = Main.mouseY + Main.screenPosition.Y - projectile.Center.Y;
                    Vector2 value2 = new Vector2(num78, num79);
                    value2.Normalize();
                    Vector2 value3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    value3.Normalize();
                    value2 = value2 * 6f + value3;
                    value2.Normalize();
                    value2 *= 10f;
                    float num91 = Main.rand.Next(10, 50) * 0.001f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num91 *= -1f;
                    }
                    float num92 = Main.rand.Next(10, 50) * 0.001f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num92 *= -1f;
                    }
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value2.X, value2.Y, mod.ProjectileType("FireTentacle"), projectile.damage * (int)1.25f, projectile.knockBack, player.whoAmI, num92, num91);
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 300);
            Main.PlaySound(SoundID.Item14, projectile.position);
            Projectile.NewProjectile(projectile.position, projectile.velocity, ModContent.ProjectileType<AkumaExp>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
        }
    }
}
