using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class TrueHallowedPrism : HoverShooter
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Hallowed Prism");
            Main.projFrames[projectile.type] = 5;

        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 22;
            projectile.height = 50;
            Main.projFrames[projectile.type] = 6;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            Inertia = 20f;
            Shoot = mod.ProjectileType("TruePrismshot");
            ShootSpeed = 16f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Hallow;
        }

        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.dead) modPlayer.TrueHallowedPrism = false;
            if (modPlayer.TrueHallowedPrism) projectile.timeLeft = 2;
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), Main.DiscoColor.R * 0.3f, Main.DiscoColor.G * 0.3f, Main.DiscoColor.B * 0.3f);
        }

        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame > 5)
            {
                projectile.frame = 0;
            }
        }
    }
}