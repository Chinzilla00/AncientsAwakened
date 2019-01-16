using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.TownNPCs

{
    public class EyeShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovecraftian Eye");
		}

		public override void SetDefaults()
		{
            projectile.CloneDefaults(452);
            projectile.hostile = false;
            projectile.friendly = true;
            
		}

        public override void AI()
        {
            if (NPC.downedMoonlord)
            {
                projectile.damage = 200;
                return;
            }
            if (Main.hardMode)
            {
                projectile.damage = 90;
                return;
            }
            if (!Main.hardMode)
            {
                projectile.damage = 20;
                return;
            }
        }
    }
}
