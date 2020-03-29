using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Champion
{
    [AutoloadEquip(EquipType.Head)]
    public class ChampionHeaddress : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Champion Headdress");
            Tooltip.SetDefault(@"70% increased minion damage
10% increased non-minion damage
+7 maximum Minions
+2 maximum sentries 
The armor of a champion feared across the land");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 9;
            AARarity = 14;
            item.defense = 27;
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

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("ChampionPlate") && legs.type == mod.ItemType("ChampionGreaves");
		}


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChampionHeaddressBonus");
        }


        public override void UpdateEquip(Player player)
        {
            player.minionDamage += .6f;
            player.allDamage += .1f;
            player.maxMinions += 7;
            player.maxTurrets += 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HoodlumHood", 1);
            recipe.AddIngredient(null, "ChampionPlate", 10);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}