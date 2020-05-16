using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class ThrowingCrescent : BaseAAItem
	{
		public override void SetDefaults()
		{

            item.damage = 300;            
            item.melee = true;
            item.width = 30;
            item.height = 30;
			item.useTime = 5;
			item.useAnimation = 8;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 1;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.shootSpeed = 15f;
			item.shoot = mod.ProjectileType ("TC");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Crescent");
            Tooltip.SetDefault("");
        }


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
                ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(null, "EventideAbyssium", 5);
                recipe.AddIngredient(null, "DreadScale", 5);
                recipe.AddIngredient(ItemID.LightDisc, 5);
				recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
		}
    }
}
