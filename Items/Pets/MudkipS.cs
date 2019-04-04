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
            if (!modPlayer.MudkipS)
            {
                projectile.active = false;
            }
        }
    }
}


