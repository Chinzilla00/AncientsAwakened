using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.StripeMan
{
    [AutoloadEquip(EquipType.Legs)]
	public class StripeManPants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stripeman's Lucky Pants");
            Tooltip.SetDefault(@"Get all of the fisher skill effects
When fish swallowed the hook, you can get an extra fish.
Your fishing rod has chance to steal drops from the enemies and npcs
You can use your fishing rod to catch the items on the ground  
You have more chance to get a crate among the extra booty");
        }

		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 18;
			item.rare = -1;
			item.defense = 1;
            item.value = Item.sellPrice(0, 0, 0, 1);
        }

		public override bool DrawLegs()
        {
            return false;
        }

		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().StripeManFish = true;
			player.fishingSkill += 100;
			player.accFishingLine = true;
			player.accTackleBox = true;
			player.sonarPotion = true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AnglerHat, 1);
			recipe.AddIngredient(ItemID.AnglerVest, 1);
			recipe.AddIngredient(ItemID.AnglerPants, 1);
			recipe.AddIngredient(ItemID.AnglerTackleBag, 1);
			recipe.AddIngredient(null, "ShinyCharmFish", 1);
			recipe.AddIngredient(null, "LuckyCracker", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}