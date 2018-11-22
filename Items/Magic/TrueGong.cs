using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Magic
{
    public class TrueGong : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 50;
            item.height = 64;
            item.maxStack = 1;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 6;
			item.damage = 100;                        
            item.magic = true;
			item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 4;
			item.mana = 13;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = 122;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The True Gong");
            Tooltip.SetDefault("MORE GONG");
        }

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 45f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
		    double startAngle = Math.Atan2(speedX, speedY)- (spread/2);
		    double deltaAngle = spread/5f;
		    double offsetAngle;
		    int i;
		    for (i = 0; i < 5;i++ )
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Terraria.Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
		    }
		    return false;
		}

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Gong");
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
