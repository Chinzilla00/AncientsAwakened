using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosFukumen : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Chaos Fukumen");
            Tooltip.SetDefault(@"24% increased ranged critical strike chance");
        }

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 50000;
			item.rare = ItemRarityID.Lime;
			item.defense = 15;
		}

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        public override void UpdateArmorSet(Player player){
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChaosFukumenBonus");
            player.rangedDamage += .25f;
            player.aggro -= 7;
            player.GetModPlayer<AAPlayer>().ChaosRa = true;
            player.ammoCost75 = true;
            player.nightVision = true;
			player.detectCreature = true;
        }

        public override void AddRecipes()
		{
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AbyssalFukumen"));
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Dynaskull"));
            recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}