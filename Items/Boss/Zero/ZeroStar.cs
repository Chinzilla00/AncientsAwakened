using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
	public class ZeroStar : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Zero Star");
            Tooltip.SetDefault("A spinning blade of doom");
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Boss/Zero/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }

		public override void SetDefaults()
		{
			item.damage = 170;
			item.width = 46;
			item.height = 46;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = Item.buyPrice(1, 0, 0, 0);
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.thrown = true;
            item.shoot = mod.ProjectileType("ZeroStarP");
            item.shootSpeed = 20f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.glowMask = customGlowMask;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(120, 0, 30);
                }
            }
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ApocalyptitePlate", 5);
                recipe.AddIngredient(null, "UnstableSingularity", 5);
                recipe.AddIngredient(ItemID.LightDisc, 5);
                recipe.AddTile(null, "BinaryReassembler");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
	}
}