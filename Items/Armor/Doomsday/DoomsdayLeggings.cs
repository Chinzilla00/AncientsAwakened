using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Legs)]
	public class DoomsdayLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doomsday Assault Greaves");
			Tooltip.SetDefault(@"18% increased movement speed
16% increased melee speed
25% decreased ammo consumption
7% increased damage
The power to destroy entire planets rests in this armor");

		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 18;
			item.value = 3000000;
			item.defense = 26;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.18f;
			player.meleeSpeed += 0.16f;
			player.ammoCost75 = true;
            player.meleeDamage *= 1.07f;
            player.rangedDamage *= 1.07f;
            player.magicDamage *= 1.07f;
            player.minionDamage *= 1.07f;
            player.thrownDamage *= 1.07f;
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 18);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}