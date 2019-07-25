using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Leviathan
{
    [AutoloadEquip(EquipType.Head)]
    public class LeviathanHelm : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leviathan Mask");
            Tooltip.SetDefault(@"16% increased ranged & magic Damage
Enemies are less likely to target you
It smells like fish.");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;
            item.defense = 13;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("LeviathanPlate") && legs.type == mod.ItemType("LeviathanGreaves");
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Lang.ArmorBonus("LeviathanHelmBonus");
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                player.moveSpeed += .5f;
                player.rangedDamage += .5f;
                player.magicDamage += .5f;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .14f;
            player.magicDamage += .14f;
            player.aggro -= 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FishronScale", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}