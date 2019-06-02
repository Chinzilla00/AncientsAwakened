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
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
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
            projectile.extraUpdates = 0;
            projectile.width = 16;
            projectile.height = 16;    
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 30f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 400f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 17f;
        }

        public override void AI()
        {
            if (projectile.ai[1] == 0 && Main.netMode != 1)
            {
                int proj = Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType<SolarSun>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                Main.projectile[proj].netUpdate = true;
                projectile.ai[1]++;
                projectile.netUpdate = true;
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, 0, 0, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default(Color), 1f)];
                dust2 = Main.dust[Dust.NewDust(position, 0, 0, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 1f)];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }
        }



        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }


        //dust = Main.dust[Terraria.Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 15, 0f, 0f, 46, new Color(255, 75, 0), 1.381579f)];

    }
}
