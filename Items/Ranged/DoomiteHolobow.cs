using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DoomiteHolobow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Holobow");
        }

        public override void SetDefaults()
        {
            item.damage = 28;
            item.noMelee = true;
            item.ranged = true;
            item.width = 20;
            item.height = 64;
            item.useTime = 23;
            item.useAnimation = 23;
            item.useStyle = 5;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.shootSpeed = 40f;
            item.crit = 0;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.HoloArrow>(), item.damage, knockBack, player.whoAmI);
            return false;
        }
    }
}