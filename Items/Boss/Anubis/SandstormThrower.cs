using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis
{
    public class SandstormThrower : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandthrower");
			Tooltip.SetDefault("30% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			item.damage = 30;
			item.ranged = true;
			item.width = 80;
			item.height = 38;
			item.useTime = 3;
			item.useAnimation = 5;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 6;
            item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<Projectiles.Anubis.SandstormFlame>();
			item.shootSpeed = 12f;
			item.useAmmo = 23;
		}

	    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
		}

	    public override bool ConsumeAmmo(Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 30)
	    		return false;
	    	return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Sandgun, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
