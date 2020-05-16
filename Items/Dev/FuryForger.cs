using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace AAMod.Items.Dev
{
    public class FuryForger : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fury Forger");
			Tooltip.SetDefault(@"Striking enemies causes sparks to fly from them");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.melee = true;
			item.width = 48;
			item.height = 52;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 4;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(128, 56, 56);
                }
            }
        }
        
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Forge"));
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(player.velocity.X, player.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 4; i++)
                {
                    double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("SparkFury"), item.damage, 1.25f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), mod.ProjectileType("SparkFury"), item.damage, 1.25f, player.whoAmI, 0f, 0f);
                }
            }
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}
