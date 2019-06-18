using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class FossilBoneslinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fossil Boneslinger");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.width = 12;
            item.height = 28;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 25;
            item.shootSpeed = 8f;
            item.knockBack = 1f;
            item.rare = 3;
            item.noMelee = true;
            item.value = 9000;
            item.ranged = true;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType<Projectiles.AmberArrow>();
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DesertFossil, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}