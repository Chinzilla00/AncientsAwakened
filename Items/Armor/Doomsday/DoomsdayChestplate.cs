using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace AAMod.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Body)]
	public class DoomsdayChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Doomsday Assault Armor");
			Tooltip.SetDefault(@"25% increased melee and ranged damage
7% increased damage
The power to destroy entire planets rests in this armor");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 3000000;
			item.defense = 46;
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

        public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.25f;
			player.rangedDamage *= 1.25f;
            player.meleeDamage *= 1.07f;
            player.rangedDamage *= 1.07f;
            player.magicDamage *= 1.07f;
            player.minionDamage *= 1.07f;
            player.thrownDamage *= 1.07f;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 20);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
			recipe.AddTile(null, "BinaryReassembler");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}