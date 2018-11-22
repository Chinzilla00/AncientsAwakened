using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
	public class MiniProbe : ModProjectile
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mini Probe"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 6;
			Main.projPet[projectile.type] = true;
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Pets/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);
            aiType = ProjectileID.SuspiciousTentacle;
            projectile.width = 14;
            projectile.height = 14;
            projectile.glowMask = customGlowMask;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.suspiciouslookingTentacle = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.dead)
			{
				modPlayer.MiniProbe = false;
			}
			if (modPlayer.MiniProbe)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}