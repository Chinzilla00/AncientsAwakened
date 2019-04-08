using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.TrueAbyssal
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueAbyssalFukumen : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("True Abyssal Fukumen");
            Tooltip.SetDefault(@"35% increased movement speed
20% increased minion damage
Weightless as shadow itself");
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
            player.minionDamage += .2f;
            player.moveSpeed += .35f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TrueAbyssalGi") && legs.type == mod.ItemType("TrueAbyssalHakama");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = @"Your minions are imbued with the poisonous properties of hydra venom
Enemies are less likely to target you
+4 Minion Slot
Nightvision and Hunter potions effects";
            player.aggro -= 5;
            player.GetModPlayer<AAPlayer>(mod).depthSet = true;
            player.maxMinions += 4;
            player.nightVision = true;
			player.detectCreature = true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AbyssalFukumen"));
			recipe.AddIngredient(null, "MireCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}