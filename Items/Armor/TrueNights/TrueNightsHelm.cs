using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.TrueNights
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueNightsHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Night's Helm");
			Tooltip.SetDefault("14% increased melee damage and speed");

		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.value = 90000;
			item.rare = 7;
			item.defense = 27;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.meleeSpeed += 0.14f;
            player.meleeDamage *= 1.14f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TrueNightsPlate") && legs.type == mod.ItemType("TrueNightsGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Lang.ArmorBonus("TrueNightsHelmBonus");
            player.moveSpeed += 0.44f;
            player.panic = true;
            player.GetModPlayer<AAPlayer>(mod).trueNights = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightsHelm", 1);
            recipe.AddIngredient(null, "CorruptionCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}