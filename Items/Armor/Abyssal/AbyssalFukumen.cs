using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Abyssal
{
    [AutoloadEquip(EquipType.Head)]
	public class AbyssalFukumen : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Abyssal Fukumen");
            Tooltip.SetDefault(@"35% increased movement speed
15% increased ranged damage
Weightless as shadow itself");
        }

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.LightRed;
			item.defense = 6;
		}

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += .15f;
            player.moveSpeed += .35f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .35f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("AbyssalGi") && legs.type == mod.ItemType("AbyssalHakama");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.AbyssalBonus");
            player.GetModPlayer<AAPlayer>().depthSet = true;
            player.aggro -= 3;
            player.ammoCost80 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DepthFukumen", 1);
            recipe.AddIngredient(null, "RelicBar", 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}