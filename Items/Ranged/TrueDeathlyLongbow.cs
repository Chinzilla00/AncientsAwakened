using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Ranged
{
    public class TrueDeathlyLongbow : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Ghastbow");
            Tooltip.SetDefault(@"Replaces Arrows with Reaper Arrows
Fires an explosive ghast skull every other shot");
        }

        public override void SetDefaults()
		{
			item.damage = 75;
			item.ranged = true;
			item.width = 46;
			item.height = 86;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Arrow;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.statLife += damage / 8;
            player.HealEffect(damage / 8);
        }

        int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ReaperArrow"), damage, knockBack, player.whoAmI);
            }
            shoot++;

            if (shoot % 2 != 0) return false;

            if (shoot >= 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("GhastSkull"), (int)(damage * 1.5), knockBack, player.whoAmI);
                shoot = 0;
            }
            shoot = 0;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DeathlyLongbow", 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(null, "DungeonCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
