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
			Tooltip.SetDefault("14% increased melee speed");

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
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TrueNightsPlate") && legs.type == mod.ItemType("TrueNightsGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"Being hit by enemies Sends you into a speed frenzy, increasing movement speed for a short time
44% Increased Movement speed
Killing enemies has a 25% chance to cause them to erupt into a cursed explosion that damages enemies around it
Your melee attacks inflict cursed inferno";
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