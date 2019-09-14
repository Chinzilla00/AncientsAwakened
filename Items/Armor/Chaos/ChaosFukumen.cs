using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			item.rare = 7;
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
            player.setBonus = Lang.ArmorBonus("ChaosFukumenBonus");
            player.rangedDamage += .25f;
            player.aggro -= 7;
            player.GetModPlayer<AAPlayer>(mod).ChaosRa = true;
            player.ammoCost75 = true;
            player.nightVision = true;
			player.detectCreature = true;
        }

        public override void AddRecipes()
		{
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("TrueAbyssalFukumen"));
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(null, "TruePaladinsSmeltery");
            recipe.SetResult(this);
			recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TrueDynaskull"));
            recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(null, "TruePaladinsSmeltery");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}