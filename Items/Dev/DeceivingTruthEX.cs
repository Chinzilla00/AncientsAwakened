using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class DeceivingTruthEX : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Deceiving Nightmare");
            Tooltip.SetDefault(@"Summons 1 of 10 possible piercing streaks
Deceiving Truth EX");
            Item.staff[item.type] = true;
        }

		public override void SetDefaults()
		{
            item.mana = 10;
            item.damage = 130;
            item.useStyle = 5;
            item.shootSpeed = 32f;
            item.shoot = mod.ProjectileType<Projectiles.Darkpuppey.A>();
            item.width = 26;
            item.height = 28;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Vine");
            item.useAnimation = 23;
            item.useTime = 23;
            item.autoReuse = true;
            item.rare = 11;
            item.noMelee = true;
            item.knockBack = 1f;
            item.value = 3000000;
            item.magic = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int ShootMe = Main.rand.Next(10);
            switch (ShootMe)
            {
                case 0:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.A>();
                    break;
                case 1:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.B>();
                    break;
                case 2:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.C>();
                    break;
                case 3:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.D>();
                    break;
                case 4:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.E>();
                    break;
                case 5:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.F>();
                    break;
                case 6:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.G>();
                    break;
                case 7:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.H>();
                    break;
                case 8:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.I>();
                    break;
                case 9:
                    type = mod.ProjectileType<Projectiles.Darkpuppey.J>();
                    break;

            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DeceivingTruth");
                recipe.AddIngredient(null, "EXSoul");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}