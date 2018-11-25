/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;


namespace AAMod.Items.Armor.Valkyrie
{
    [AutoloadEquip(EquipType.Head)]
	public class ValkyrieHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Valkyrie Helm");
			Tooltip.SetDefault(@"15% increased melee critical chance
25% increased throwing critical chance
Enemies are more likely to target you
Hard as ice, light as snow");

		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 3000000;
			item.rare = 11;
			item.defense = 30;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(102, 204, 255);
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 15;
			player.thrownCrit += 25;
			player.aggro += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("ValkyrieChestplate") && legs.type == mod.ItemType("ValkyrieBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"Champion's Valor
Your Melee and thrown weapons chill enemies
Your Melee and thrown weapons Frostburn enemies";
			
			player.GetModPlayer<AAPlayer>(mod).valkyrieSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 18);
			recipe.AddIngredient(ItemID.FrostCore, 2);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}*/