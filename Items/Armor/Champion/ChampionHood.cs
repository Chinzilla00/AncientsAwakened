using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Champion
{
    [AutoloadEquip(EquipType.Head)]
    public class ChampionHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Slayer Hood");
            Tooltip.SetDefault(@"32% increased Magic damage
10% increased non-magic damage
25% increased Magic critical strike chance
25% reduced Mana consumption
150 increased maximum mana
The armor of a champion feared across the land");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 9;
            AARarity = 14;
            item.defense = 30;
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
            player.setBonus = @"";
        }


        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.22f;
            player.allDamage += .1f;
            player.magicCrit += 25;
            player.manaCost *= .75f;
            player.statManaMax2 += 150;
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