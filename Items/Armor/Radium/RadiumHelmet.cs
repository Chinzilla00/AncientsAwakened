using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radium Helmet");
			Tooltip.SetDefault(@"15% increased melee damage
Shines with the light of a starry night sky");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 300000;
			item.rare = 11;
			item.defense = 30;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.15f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Lang.ArmorBonus("RadiumHelmetBonus");
            if (Main.dayTime)
            {
                player.moveSpeed += .3f;
            }
            player.GetModPlayer<AAPlayer>().Radium = true;
            player.GetModPlayer<AAPlayer>().radiumMe = true;
            player.meleeSpeed += 0.15f;
            player.meleeCrit += 15;
            player.panic = true;
            player.starCloak = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}