using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Magic
{
    public class TrueGong : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 50;
            item.height = 64;
            item.maxStack = 1;

            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 6;
			item.damage = 150;                        
            item.magic = true;
			item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 4;
			item.mana = 13;             //mana use
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/MOARGONG"); 
            item.autoReuse = true;
            item.shoot = 122;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The True Gong");
            Tooltip.SetDefault("MORE GONG");
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 25f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
		    double startAngle = Math.Atan2(speedX, speedY)- (spread/2);
		    double deltaAngle = spread/5f;
		    double offsetAngle;
		    int i;
		    for (i = 0; i < 5;i++ )
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
		    }
		    return false;
		}

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Gong");
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
