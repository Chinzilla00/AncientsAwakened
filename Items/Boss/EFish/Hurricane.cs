using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class Hurricane : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hurricane");
			Tooltip.SetDefault(@"Shoots 2 waves of 6 arrows
You have a chance to shoot Oceanic Arrow
66% chance not to consume arrow
Tsunami EX");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 20;
            item.useTime = 10;
            item.width = 30;
            item.height = 62;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 100;
            item.shootSpeed = 13f;
            item.knockBack = 4f;
            item.rare = ItemRarityID.Yellow;
            item.noMelee = true;
            item.value = 200000;
            item.ranged = true;
			item.autoReuse = true;
        }
		
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .66;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num121 = 0.314159274f;
			int num122 = 5;
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
			Vector2 vector14 = new Vector2(speedX, speedY);
			vector14.Normalize();
			vector14 *= 40f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
			int arrowtype = type;
			for (int num123 = 0; num123 < num122; num123++)
			{
				float num124 = num123 - (num122 - 1f) / 2f;
				Vector2 vector15 = vector14.RotatedBy(num121 * num124, default);
				if (!flag11)
				{
					vector15 -= vector14;
				}
				if (Main.rand.NextBool(8))
				{
					type = mod.ProjectileType("OceanicArrow");
				}
				else
				{
					type = arrowtype;
				}
				int num125 = Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage, knockBack, player.whoAmI);
				Main.projectile[num125].noDropItem = true;
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Tsunami);
			recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}