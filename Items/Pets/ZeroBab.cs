using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class ZeroBab : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.LightPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BabySkeletronHead);
            aiType = ProjectileID.BabySkeletronHead;
            projectile.width = 62;
            projectile.height = 62;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.skeletron = false;
            return true;
        }

        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, 1f, 0.2f, 0.1f);
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.ZeroBab = false;
            }
            if (!modPlayer.ZeroBab)
            {
                projectile.active = false;
            }

            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 5, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 5, frame, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Glowmasks/ZeroBab_Glow"), 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 5, frame, Globals.AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}


