using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TruffleBookIt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TruffleBookIt");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.damage = 24;
            projectile.width = 66;
            projectile.height = 104;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(projectile.position), BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position));

            Lighting.AddLight((int)(projectile.Center.X + (projectile.width / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16, color.R / 255, color.G / 255, color.B / 255);
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y -= .1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TruffleBookIt_Glow1");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/TruffleBookIt_Glow2");
            
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(projectile.position), BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position));
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 4, 0, 2);

            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, color, true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex1, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}