using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Draco.Dracokip
{
    [AutoloadEquip(EquipType.Legs)]
	public class DracoDiverBoots : ModItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Draconian Diver Flippers");
            Tooltip.SetDefault(@"16% increased movement speed
14% increased melee speed
25% decreased mana consumption
10% increased damage resistance");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 3000000;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.16f;
            player.meleeSpeed += 0.14f;
            player.manaCost *= 0.75f;
            player.endurance *= 1.1f;
            player.statManaMax2 += 100;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Akuma;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DracoLeggings", 1);
            recipe.AddIngredient(null, "FishDiverBoots", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}