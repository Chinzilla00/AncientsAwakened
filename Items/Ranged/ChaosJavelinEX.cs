using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class ChaosJavelinEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Javelin");
            Tooltip.SetDefault(@"Explodes on contact
Chaos Javelin EX");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("ChaosJavelinEX");
            item.shootSpeed = 17f;
            item.damage = 400;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.width = 30;
            item.height = 30;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = ItemRarityID.Purple;
            item.expert = true; item.expertOnly = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosJavelin");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
