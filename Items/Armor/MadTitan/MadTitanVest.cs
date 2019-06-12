using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.MadTitan
{
    [AutoloadEquip(EquipType.Body)]
	public class MadTitanVest : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mad Titan's Vest");
			Tooltip.SetDefault(@"40% increased damage
25% decreased ammo consumption");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 3000000;
			item.rare = 11;
			item.defense = 48;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.40f;
            player.rangedDamage *= 1.40f;
            player.magicDamage *= 1.40f;
            player.minionDamage *= 1.40f;
            player.thrownDamage *= 1.40f;
            player.ammoCost75 = true;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
            drawArms = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("MadTitanHelm") && legs.type == mod.ItemType("MadTitanBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"The infinity gauntlet is now at it's most powerful
'The power of a mad titan is now at your fingertips'";
            player.GetModPlayer<AAPlayer>(mod).TrueInfinityGauntlet = true;
            player.GetModPlayer<AAPlayer>(mod).InfinityGauntlet = false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarkmatterBreastplate", 1);
            recipe.AddIngredient(null, "RadiumPlatemail", 1);
            recipe.AddIngredient(null, "CrucibleScale", 20);
            recipe.AddTile(null, "AncientForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}