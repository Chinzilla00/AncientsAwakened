using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class SoulSiphon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Siphon");
			Tooltip.SetDefault(@"I swear if you ask me for a song one more time...
-Charlie");
		}
		public override void SetDefaults()
		{
			item.damage = 220;
            item.useStyle = 1;
			item.melee = true;
            item.useAnimation = 25;
            item.useTime = 25;
            item.knockBack = 7f;
            item.width = 60;
            item.height = 56;
            item.scale = 1.15f;
            item.UseSound = SoundID.Item71;
            item.rare = 11;
            item.shootSpeed = 9f;
            item.value = 500000;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AmphibiousProjectile");
            item.shootSpeed = 9f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(30, 6, 49);
                }
            }
        }
        
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            //target.AddBuff(BuffID.SoulDrain, 1000);
        }
	}
}
