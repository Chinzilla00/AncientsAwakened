using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class MudkipS : ModProjectile
    {

        public static int frameWidth = 60, frameHeight = 46;
        public Rectangle frame;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shiny Mudkip"); // Automatic from .lang files
            Main.projFrames[projectile.type] = 11;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BlackCat);
            aiType = ProjectileID.BlackCat;
            projectile.width = 36;
            projectile.height = 38;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.blackCat = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.dead)
            {
                modPlayer.MudkipS = false;
            }
            if (modPlayer.MudkipS)
            {
                projectile.timeLeft = 2;
            }
        }

        public Color GetGlowAlpha(Player player)
        {

            if (Main.bloodMoon)
            {
                return new Color(177, 54, 79);
            }
            else if (BaseExtensions.InZone(player, "Desert") || BaseExtensions.InZone(player, "UGDesert") || BaseExtensions.InZone(player, "Sandstorm"))
            {
                return new Color(168, 106, 32);
            }
            else if (BaseExtensions.InZone(player, "Jungle"))
            {
                return new Color(7, 145, 142);
            }
            else if (BaseExtensions.InZone(player, "Snow"))
            {
                return new Color(9, 137, 191);
            }
            else if (BaseExtensions.InZone(player, "Corrupt"))
            {
                return new Color(59, 29, 131);
            }
            else if (BaseExtensions.InZone(player, "Crimson"))
            {
                return new Color(200, 0, 0);
            }
            else if (BaseExtensions.InZone(player, "Hallow"))
            {
                return new Color(171, 11, 209);
            }
            else if (BaseExtensions.InZone(player, "Dungeon"))
            {
                return new Color(232, 32, 3);
            }
            else
            {
                return new Color(9, 61, 191);
            }
        }

        public static Texture2D glowTex = null;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Player player = Main.player[projectile.owner];
            Color lightColor = BaseMod.BaseDrawing.GetLightColor(projectile.Center);
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Mudkip_Glow");
            }
            frame = BaseDrawing.GetFrame(Main.projFrames[projectile.type], frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spritebatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 11, frame, GetGlowAlpha(player));
            return false;
        }
    }
}


