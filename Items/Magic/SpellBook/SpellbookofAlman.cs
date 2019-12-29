using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellbookofAlman : EffectSpellBook
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spellbook of Alman");
			Tooltip.SetDefault(@"Copy a non-magical weapon in your inventory
If this book is copied and consumed by the SpellBook of Sefer, you get the following effects:
Copy and shoot a projectile in the world");
		}

		public override void SetDefaults()
		{
			item.damage = 415;
			item.mana = 12;
			item.useStyle = 5;
			item.shootSpeed = 12f;
			item.shoot = 1;
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useTime = 20;
            item.useAnimation = 20;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 2f;
			item.rare = 7;
			item.value = Item.sellPrice(0, 25, 26, 0);
			item.magic = true;
			item.reuseDelay = 20;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(null, "BinaryReassembler");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ExtraShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int m = 0; m < 50; m++)
			{
				Item itemshoot = player.inventory[m];
				
				if (itemshoot != null && !itemshoot.summon && itemshoot.stack > 0 && !itemshoot.magic && itemshoot.shoot > 0 && itemshoot.type != mod.ItemType("SpellBookofSefer") && itemshoot.type != mod.ItemType("SpellbookofAlman")) 
				{
					ModItem moditem = ItemLoader.GetItem(itemshoot.type);
					int shoottype = itemshoot.shoot;
					if (itemshoot.useAmmo > 0)
					{
						float speed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
						bool canshoot = true;
						player.PickAmmo(itemshoot, ref shoottype, ref speed, ref canshoot, ref damage, ref knockBack, false);
						if(shoottype == itemshoot.shoot)
						{
							continue;
						}
					}
					if(moditem != null)
					{
						try
						{
							MethodInfo Shoot = moditem.GetType().GetMethod("Shoot", BindingFlags.Instance | BindingFlags.Public);
							if((bool)Shoot.Invoke(moditem, new object[]{player, position, speedX, speedY, shoottype, (int)(damage * player.magicDamage), knockBack}))
							{
								int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, shoottype, (int)(damage * player.magicDamage), knockBack, player.whoAmI);
								Main.projectile[p].magic = true;
								Main.projectile[p].noDropItem = true;
								Main.projectile[p].friendly = true;
								Main.projectile[p].hostile = false;
							}
						}
						catch
						{}
					}
					else
					{
						int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, shoottype, (int)(damage * player.magicDamage), knockBack, player.whoAmI);
						Main.projectile[p].magic = true;
						Main.projectile[p].noDropItem = true;
						Main.projectile[p].friendly = true;
						Main.projectile[p].hostile = false;
					}
					break;
				}
			}
			return false;
		}

		public override void Instanteffect(Player player, Vector2 position, Vector2 velocity, int damage)
		{
			for(int i = 0; i < 1000; i++)
			{
				if(Main.projectile[i].active && Main.projectile[i].hostile && !Main.projectile[i].friendly && Main.projectile[i].velocity.Length() > .1f)
				{
					for(int j = 0; j < 3; j++)
					{
						Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
						int k = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Main.projectile[i].type, Main.projectile[i].damage, Main.projectile[i].knockBack, Main.projectile[i].owner);
						Main.projectile[k].GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellbookofAlman = true;
						Main.projectile[k].magic = true;
						Main.projectile[k].noDropItem = true;
						Main.projectile[k].friendly = true;
						Main.projectile[k].hostile = false;
					}
					return;
				}
			}
		}

		public override void Sustainedeffect(Projectile projectile)
		{
			if(projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellbookofAlman)
			{
				projectile.velocity = projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().vanillaoldvelocity;
			}
		}
	}
}
