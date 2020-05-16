using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosMask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Chaos Mask");
            Tooltip.SetDefault(@"Increases maximum mana by 80
Increases magic damage by 20%
Increases magic crit by 20%
Allows you to breath underwater");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Lime;
            item.defense = 18;
        }
		
		public override void UpdateEquip(Player player)
		{
			player.manaCost -= 0.3f;
            player.magicDamage += 0.20f;
            player.gills = true;
            player.magicCrit += 20;
			player.statManaMax2 += 80;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChaosMaskBonus");
			if (player.wet)
			{
				player.AddBuff(mod.BuffType("ChaosBuff"), 2);
            }
            player.accFlipper = true;
            player.ignoreWater = true;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AtlanteanHelm"));
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}