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
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));

            int num147 = 8;
            int num148 = -18;

            float num149 = (Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + projectile.width * 0.5f;

            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            int num312 = Main.projectileTexture[projectile.type].Height / 11;
            int y15 = num312 * projectile.frame;

            Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + num149 + num148, projectile.position.Y - Main.screenPosition.Y + projectile.height / 2 + projectile.gfxOffY), new Rectangle?(new Rectangle(0, y15, Main.projectileTexture[projectile.type].Width, num312 - 1)), projectile.GetAlpha(color25), projectile.rotation, new Vector2(num149, projectile.height / 2 + num147), projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}


