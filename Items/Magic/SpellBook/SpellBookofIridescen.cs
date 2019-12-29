using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofIridescen : EffectSpellBook
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellBook of Iridescen");
			Tooltip.SetDefault(@"Shoot a rainbow and a bunch of holy arrow
If this book is copied and consumed by the SpellBook of Sefer, you get the following effects:
In this shooting turn, when you get the damage exceeding your life, you will avoid this damage.");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.mana = 20;
			item.useStyle = 5;
			item.shootSpeed = 10f;
			item.shoot = 91;
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useTime = 20;
            item.useAnimation = 20;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 2f;
			item.rare = 8;
			item.value = Item.sellPrice(0, 12, 13, 0);
			item.magic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PixieDust, 100);
			recipe.AddIngredient(ItemID.CrystalShard, 100);
			recipe.AddIngredient(ItemID.UnicornHorn, 5);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ExtraShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			bool flag = false;
			for(int i = 0; i < 1000; i++)
			{
				if(Main.projectile[i].type == 251 && Main.projectile[i].timeLeft == 0)
				{
					Main.projectile[i].Kill();
				}
				if(Main.projectile[i].type == 251 && Main.projectile[i].active)
				{
					flag = true;
					break;
				}
			}
			if(!flag)
			{
				int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 250, damage, knockBack, player.whoAmI);
				Main.projectile[p].magic = true;
			}
			float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .05d;
		    double deltaAngle = spread / 3f;
		    double offsetAngle;
		    for (int i = 0; i < 3; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	int p = Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), 91, damage, knockBack, player.whoAmI);
				Main.projectile[p].magic = true;
				Main.projectile[p].noDropItem = true;
		    }
			return true;
		}

		public override void Instanteffect(Player player, Vector2 position, Vector2 velocity, int damage)
		{
			float speedX = velocity.X;
			float speedY = velocity.Y;
			float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
			for (int i = 0; i < 5; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	int p = Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), 91, item.damage, item.knockBack, player.whoAmI);
				Main.projectile[p].magic = true;
				Main.projectile[p].noDropItem = true;
		    }
			player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().SpellBookofIridescen = true;
		}
	}
}
