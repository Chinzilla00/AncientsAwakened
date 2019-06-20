using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fulgurite
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Helmet");
			Tooltip.SetDefault(@"12% increased melee damage, critical strike chance, and melee speed");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 50000;
			item.rare = 5;
			item.defense = 22;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.meleeDamage *= 1.12f;
            player.meleeCrit += 12;
            player.meleeSpeed *= 1.12f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FulguriteBreastplate") && legs.type == mod.ItemType("FulguritePants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
+22% increased melee and movement speed";

            player.GetModPlayer<AAPlayer>(mod).fulgurite = true;
            player.meleeSpeed *= 1.22f;
            player.moveSpeed *= 1.22f;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}