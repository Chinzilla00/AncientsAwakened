using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.MadTitan
{
    [AutoloadEquip(EquipType.Body)]
	public class MadTitanChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mad Titan's Chestplate");
			Tooltip.SetDefault(@"40% increased damage
25% decreased ammo consumption");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 3000000;
            item.rare = 9;
            AARarity = 14;
            item.defense = 48;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.allDamage *= 1.40f;
            player.ammoCost75 = true;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("MadTitanHelm") && legs.type == mod.ItemType("MadTitanBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"The infinity gauntlet is now at it's most powerful
'The power of a mad titan is now at your fingertips'";
            player.GetModPlayer<AAPlayer>(mod).TrueInfinityGauntlet = true;
            player.GetModPlayer<AAPlayer>(mod).InfinityGauntlet = false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarkmatterBreastplate", 1);
            recipe.AddIngredient(null, "RadiumPlatemail", 1);
            recipe.AddIngredient(null, "CrucibleScale", 20);
            recipe.AddTile(null, "AncientForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}