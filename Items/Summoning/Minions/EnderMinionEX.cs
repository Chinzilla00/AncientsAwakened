using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class EnderMinionEX : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.CloneDefaults(533); // ID for Deadly Sphere proj
            aiType = 533;
            projectile.width = 62;
            projectile.height = 62;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ender Minion EX");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

        }

        /*public override void AI()         // If you want dust to spawn from it
        {
            if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 1); //1 is where the dust id should go
            }
        }*/
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y;
            }
            return false;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.enderMinionEX = false;
            }
            if (modPlayer.enderMinionEX)
            {
                projectile.timeLeft = 2;
            }
            return true;
        }
    }
}