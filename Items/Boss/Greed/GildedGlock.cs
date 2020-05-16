using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class GildedGlock : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilded Glock");
            Tooltip.SetDefault("Uses Coins as Ammo");
        }
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 30;
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 28;
            item.useTime = 28;
            item.UseSound = SoundID.Item41;
            item.damage = 70;
            item.knockBack = 7;
            item.ranged = true;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = ProjectileID.CopperCoin;
            item.shootSpeed = 12;
            item.useAmmo = AmmoID.Coin;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol, 1);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}