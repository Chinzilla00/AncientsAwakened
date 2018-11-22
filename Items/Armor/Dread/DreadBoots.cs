using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dread
{
    [AutoloadEquip(EquipType.Legs)]
	public class DreadBoots : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dread Moon Hakama");
			Tooltip.SetDefault(@"50% increased movement speed
100% increased throwing velocity
25% decreased ammo consumption
5% increased damage resistance
The abyssal wrath of the Mire rests in this armor");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 3000000;
			item.defense = 29;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.5f;
            player.thrownVelocity *= 2;
			player.ammoCost75 = true;
			player.endurance *= 1.05f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(45, 46, 70);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 18);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DepthHakama", 1);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}