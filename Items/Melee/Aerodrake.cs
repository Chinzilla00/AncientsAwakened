using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Aerodrake : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aerodrake");
			Tooltip.SetDefault("Flying Dragon EX");
		}

		public override void SetDefaults()
		{
            item.rare = 9;
            item.UseSound = SoundID.DD2_SonicBoomBladeSlash;
            item.useStyle = 1;
            item.damage = 1250;
            item.useAnimation = 15;
            item.useTime = 15;
            item.width = 82;
            item.height = 102;
            item.knockBack = 5.5f;
            item.melee = true;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.autoReuse = true;
            item.useTurn = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Aerodrake>();
            item.shootSpeed = 17f;
            item.expert = true; item.expertOnly = true;

            glowmaskDrawType = GLOWMASKTYPE_SWORD;
            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
            glowmaskDrawColor = AAColor.COLOR_WHITEFADE1;
        }

		public override void UseStyle(Player player)
        {
            player.itemLocation +=
                new Vector2(-4 * player.direction, 16 * player.gravDir).RotatedBy(player.itemRotation);
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DD2SquireBetsySword, 1);
			recipe.AddIngredient(mod, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 46)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 400);
        }
	}
}
