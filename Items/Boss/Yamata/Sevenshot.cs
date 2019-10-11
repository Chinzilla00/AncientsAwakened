using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Projectiles.Yamata;

namespace AAMod.Items.Boss.Yamata
{
    public class Sevenshot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydra Sevenshot");
			Tooltip.SetDefault(@"Fires 6 bullets and a destructive moon blast
50% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        item.damage = 120;
	        item.ranged = true;
	        item.width = 60;
	        item.height = 26;
	        item.useTime = 16;
	        item.useAnimation = 16;
	        item.useStyle = 5;
	        item.noMelee = true;
	        item.knockBack = 4.5f;
	        item.value = Item.sellPrice(0, 30, 0, 0);
	        item.UseSound = SoundID.Item36;
	        item.autoReuse = true;
	        item.shoot = 10;
	        item.shootSpeed = 20f;
	        item.useAmmo = 97;
            item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
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
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float SpeedX = speedX + Main.rand.Next(-25, 26) * 0.05f;
		    float SpeedY = speedY + Main.rand.Next(-25, 26) * 0.05f;
		    Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<Moonblow>(), (int)(damage * 1.5f), knockBack, player.whoAmI, 0.0f, 0.0f);
		    for (int i = 0; i <= 6; i++)
		    {
		    	float SpeedNewX = speedX + Main.rand.Next(-45, 46) * 0.05f;
		    	float SpeedNewY = speedY + Main.rand.Next(-45, 46) * 0.05f;
		    	Projectile.NewProjectile(position.X, position.Y, SpeedNewX, SpeedNewY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
	    
	    public override bool ConsumeAmmo(Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "EventideAbyssium", 5);
	        recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "HydraTrishot");
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
	    }
	}
}