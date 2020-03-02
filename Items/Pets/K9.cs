using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class K9 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("K9 Unit");
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
            player.blackCat = false;
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.K9 = false;
            }
            if (modPlayer.K9)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}


