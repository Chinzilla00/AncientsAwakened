using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.Items.Ranged
{
    public class Criotgun : BaseAAItem
    { 

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Criot Gun");
            Tooltip.SetDefault("“You have to question the stability of the place that calls this a riot gun…”");
        }       

        public override void SetDefaults()
		{
			item.damage = 25;
			item.ranged = true;
			item.width = 54;
			item.height = 26;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 0;
                        item.value = Item.sellPrice(0, 7, 0, 0);
                        item.rare = 8;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/CriotG");
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 10;
                        item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (shoot++ > 5) shoot = 0;

            for (int i = 0; i < 1; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)) * .5f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Criotp"), damage, knockBack, player.whoAmI);
            }

            if (Main.rand.Next(1) == 0)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)) * .5f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Criotp"), damage, knockBack, player.whoAmI);
                shoot = 0;
                for (int i = 0; i < Main.rand.Next(5); i++)
                {
                Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)) * .5f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, mod.ProjectileType("Criotp"), damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 20;
                item.useTime = 5;
                item.reuseDelay = 250;
                item.useAmmo = AmmoID.Bullet;
                item.damage = 40;
	        item.shootSpeed = 30;
            }
            else
            {
		item.shootSpeed = 10;
		item.useTime = 20;
		item.useAnimation = 20;
                item.reuseDelay = 0;
                item.useAmmo = AmmoID.Bullet;
                item.damage = 25;
            }
            return base.CanUseItem(player);
        }
    }
}
