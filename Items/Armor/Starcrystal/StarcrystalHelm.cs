using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Starcrystal
{
    [AutoloadEquip(EquipType.Head)]
    public class StarcrystalHelm : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 20;
            item.value = 10;
            item.rare = 3;
            item.defense = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starcrystal Headgear");
            Tooltip.SetDefault(@"+20 Mana");
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("StarcrystalPlate") && legs.type == mod.ItemType("StarcrystalBoots");  //put your Breastplate name and Leggings name
        }
		public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Doubles damage when mana is below 20% maximum capacity"; 
			if (player.statMana < player.statManaMax * .2f) 
            {
                player.meleeDamage *= 2;
				player.rangedDamage *= 2;
				player.magicDamage *= 2;
                player.minionDamage *= 2;
                player.thrownDamage *= 2;
            }
        }
        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddRecipeGroup("AAMod:Gold", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
