using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Head)]
	public class DepthFukumen : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Depth Fukumen");
            Tooltip.SetDefault(@"25% increased movement speed
10% increased ranged damage
Weightless as shadow itself");
        }

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 7500;
			item.rare = 2;
			item.defense = 3;
		}

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.1f;
            player.moveSpeed *= 1.25f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DepthGi") && legs.type == mod.ItemType("DepthHakama");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = @"Your minions are imbued with the poisonous properties of hydra venom
+1 Minion Slot
Nightvision";
            player.GetModPlayer<AAPlayer>(mod).depthSet = true;
            player.maxMinions += 1;
            player.nightVision = true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 15);
            recipe.AddIngredient(null, "HydraHide", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}