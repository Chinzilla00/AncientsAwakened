using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class Crescent : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Crescent");
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 3;
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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }
        int ProjTimer = 0;

        public override void PostAI()
        {
            if (Main.netMode != 1)
            {
                ProjTimer++;
                if (ProjTimer >= 50)
                {
                    ProjTimer = 0;
                    Projectile.NewProjectile(projectile.position, Vector2.Zero, Terraria.ModLoader.ModContent.ProjectileType<FlairdraCyclone>(), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }
    }
}
