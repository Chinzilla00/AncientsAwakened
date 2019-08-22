using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDeathly
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueDeathlyRibguard : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Ghast Ribguard");
            Tooltip.SetDefault("20% Increased ranged crit");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.value = 90000;
            item.rare = 8;
            item.defense = 17;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.2f;
            player.rangedCrit += 20;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Ectoplasm, 25);
                recipe.AddIngredient(ItemID.Bone, 55);
                recipe.AddIngredient(null, "DeathlyRibguard", 1);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}