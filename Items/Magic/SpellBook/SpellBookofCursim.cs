using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofCursim : EffectSpellBook
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellBook of Cursim");
			Tooltip.SetDefault(@"Sprays a shower of ichor
Decreases target's defense
If this book is copied and consumed by the SpellBook of Sefer, you get the following effects:
In this shooting turn, your magic damage boosts and breaks the enemies' armor");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.mana = 12;
			item.useStyle = 5;
			item.shootSpeed = 10f;
			item.shoot = 280;
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useTime = 20;
            item.useAnimation = 20;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 2f;
			item.rare = 12;
			item.value = Item.sellPrice(0, 3, 34, 0);
			item.magic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
			recipe.AddIngredient(ItemID.GoldenShower, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 50);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void Instanteffect(Player player, Vector2 position, Vector2 velocity, int damage)
		{
			float speedX = velocity.X;
			float speedY = velocity.Y;
			float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .05d;
		    double deltaAngle = spread / 3f;
		    double offsetAngle;
			for (int i = 0; i < 3; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	int p = Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), 280, damage, item.knockBack, player.whoAmI);
				Main.projectile[p].magic = true;
		    }
		}

		public override void Sustainedeffect(Projectile projectile)
		{
			projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellBookofCursim = true;

			if(projectile.type == 280 && projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellbookprojCounter % 30 == 0)
			{
				for(int i = 0; i < 200; i++)
				{
					NPC npc = Main.npc[i];
					if(npc.Hitbox.Intersects(projectile.Hitbox))
					{
						bool crit = false;
						if (projectile.magic && Main.rand.Next(1, 101) <= Main.player[projectile.owner].magicCrit)
						{
							crit = true;
						}
						int damage = (int)(projectile.damage * 1.5f + npc.defense * (Main.expertMode? 0.75f:0.5f));
						damage = Main.DamageVar(damage);
						float KB = projectile.knockBack;
						int hitDirection = projectile.direction;

						int strike = (int)npc.StrikeNPC(damage, KB, hitDirection, crit, false, false);
						Main.player[projectile.owner].addDPS(strike);
					}
				}
			}
		}
	}
}
