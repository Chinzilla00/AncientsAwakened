using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class MadnessBow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Bow");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 28;
            item.useTime = 28;
            item.width = 12;
            item.height = 28;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 11;
            item.shootSpeed = 5f;
            item.knockBack = 1f;
            item.rare = ItemRarityID.Green;
            item.noMelee = true;
            item.value = 3000;
            item.ranged = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MadnessFragment", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}