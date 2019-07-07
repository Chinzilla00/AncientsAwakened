using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Dev
{
    public class CursedSickleEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Tartarus Reaper");
            Tooltip.SetDefault(@"Spins a cursed scythe around you that shreds through enemies
Right click to swing the scythe");			
		}

		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.rare = 9;
            item.value = BaseUtility.CalcValue(0, 5, 0, 0);
            item.UseSound = SoundID.Item71;
            item.useStyle = 1;
            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 280;
            item.knockBack = 4;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("CursedSickleEX");
            item.shootSpeed = 0.1f;
            item.melee = true;
            item.expert = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.shoot = mod.ProjectileType("CursedSickleEXProj");
                item.shootSpeed = 7f;
            }
            else
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.shoot = mod.ProjectileType("CursedSickleEX");
                item.shootSpeed = 0.1f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("CursedSickleEffect"), damage, knockBack, player.whoAmI, k, 0f);
			}
			return true;
		}

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "CursedSickle");
                recipe.AddIngredient(null, "EXSoul");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}