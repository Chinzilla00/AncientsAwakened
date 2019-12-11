using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class Cerberus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 11;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Puppy);
            aiType = ProjectileID.Puppy;
            projectile.width = 28;
            projectile.height = 28;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.puppy = false;
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.Cerberus = false;
            }
            if (!modPlayer.Cerberus)
            {
                projectile.active = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            float num149 = (Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + projectile.width * 0.5f;

            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            int y15 = 40 * projectile.frame;

            Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + num149 + -18, projectile.position.Y - Main.screenPosition.Y + projectile.height / 2 + projectile.gfxOffY), new Rectangle?(new Rectangle(0, y15, Main.projectileTexture[projectile.type].Width, 40 - 1)), projectile.GetAlpha(color25), projectile.rotation, new Vector2(num149, projectile.height / 2 + 8), projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}


