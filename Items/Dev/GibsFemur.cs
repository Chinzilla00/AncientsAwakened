using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GibsFemur : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angry Femur");
            Tooltip.SetDefault("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        public override void SetDefaults()
        {
			item.useTime = 25;
            item.CloneDefaults(ItemID.Bone);
            item.maxStack = 1;
            item.ranged = true;
            item.damage = 120;                            
            item.value = 600000;
            item.rare = ItemRarityID.Purple;
            item.knockBack = 5;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shoot = mod.ProjectileType("GibsFemur");
			item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.shootSpeed = 10f;
            item.consumable = false;
            item.autoReuse = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Skullshot");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
