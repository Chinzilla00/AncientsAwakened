using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace AAMod.Items.Armor.Stone
{
    [AutoloadEquip(EquipType.Head)]
	public class StoneSoldierMask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Stone Soldier Helmet");
			Tooltip.SetDefault(@"Increases mining speed by 10%
Provides light & spelunker effect when worn");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.findTreasure = true;
			player.pickSpeed -= 0.15f;

			Lighting.AddLight((int)player.Center.X, (int)player.Center.Y, 1f, 0.95f, .8f);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("StoneSoldierPlate") && legs.type == mod.ItemType("StoneSoldierGreaves");
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.StoneSoldierMaskBonus");

			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			modPlayer.StoneSoldier = true;

			player.discount = true;
			player.coins = true;
			player.goldRing = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddIngredient(null, "StoneShell", 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}