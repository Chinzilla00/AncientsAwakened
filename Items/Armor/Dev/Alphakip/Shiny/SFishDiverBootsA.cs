using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Dev.Alphakip.Shiny
{
    [AutoloadEquip(EquipType.Legs)]
	public class SFishDiverBootsA : BaseAAItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Shiny Fish Diver's Flippers");
            Tooltip.SetDefault(@"Actually flippers now!
20% increased Melee/Ranged damage & critical strike chance
13% increased damage resistance and melee speed
35% increased movement speed
20% reduced ammo consumption");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(39, 115, 189);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = 9;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.2f;
            player.rangedDamage *= 1.2f;
            player.meleeCrit += 20;
            player.rangedCrit += 20;
            player.endurance *= 1.13f;
            player.meleeSpeed *= 1.13f;
            player.ammoCost80 = true;
            player.moveSpeed *= 1.35f;
            player.accFlipper = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ShinyFishDiverBoots", 1);
                recipe.AddIngredient(null, "EXArmor", 1);
                recipe.AddTile(null, "AncientForge");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "FishDiverBootsA", 1);
                recipe.AddIngredient(null, "ShinyCharm", 1);
                recipe.AddTile(null, "AncientForge");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}