using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class ElectricitySpell : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 90;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 32;
            item.height = 32;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 6;
            item.mana = 4;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("ElectricitySpellP");  //this make the item shoot your projectile
            item.shootSpeed = 11f;     
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Electricity Shard");
      Tooltip.SetDefault("It shoots sparks in an even spread.");
    }

		public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 4);   //you need 10 Wood
			recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
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
	}
}
