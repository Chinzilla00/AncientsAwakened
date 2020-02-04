using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class BHB : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Hole Blaster");
			Tooltip.SetDefault("You, beat, me... how...");
		}
		public override void SetDefaults()
		{
			item.damage = 200;
			item.ranged = true;
			item.width = 80;
			item.height = 34;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 2.5f;
			item.value = 4000000;
			item.rare = 2;
                        item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/BHB");
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("RedBullet"); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 18f;
			item.crit = 45;
			item.useAmmo = AmmoID.Bullet;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, -5);
		}

        public int cooldown;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
      cooldown++;
      	double rotationA = -0.15;
      for(int i = 0;  i < Main.rand.Next(2,4); i++)
      {
        Vector2 vector = new Vector2(speedX, speedY).RotatedBy(rotationA, default(Vector2));
			Projectile.NewProjectile(position.X  + (vector.X*4.8f) - 0.2f * vector.Y, position.Y  + (vector.Y* 4.8f)+0.2f*vector.X, vector.X, vector.Y, mod.ProjectileType("RedBullet"), damage, knockBack, player.whoAmI, 0f, 0f);
      rotationA +=  Main.rand.NextFloat(0.02f,0.1f);
    }
      if (cooldown == 10)
      {
      Projectile.NewProjectile(position.X, position.Y, speedX*0.5f, speedY/2, mod.ProjectileType("Rocket"), damage, knockBack, player.whoAmI, 0f, 0f);
        cooldown = 0;
      }
      if (Main.rand.Next(1,25)==1)
      Projectile.NewProjectile(position.X, position.Y, speedX*0.5f, speedY/2, mod.ProjectileType("Black"), damage, knockBack, player.whoAmI, 0f, 0f);

			return false;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.VortexBeater, 1);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
