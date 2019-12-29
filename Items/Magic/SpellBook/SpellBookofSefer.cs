using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofSefer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellBook of Sefer");
			Tooltip.SetDefault(@"Copy and consume the square magic weapon in your inventory one by one
Right click to put these magic weapon back in your inventory");
		}

		public override void SetDefaults()
		{
			item.mana = 45;
			item.damage = 130;
			item.useStyle = 5;
			item.shoot = 1;
			item.shootSpeed = 20f;
			item.width = 32;
			item.height = 34;
			item.UseSound = SoundID.Item20;
			item.useAnimation = 30;
			item.useTime = 30;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 6f;
			item.rare = 11;
			item.value = Item.sellPrice(0, 40, 0, 0);
			item.magic = true;
			item.reuseDelay = 15;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentNebula, 30);
            recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public int spellbookdamage = -1;
		public int spellbooktype = -1;
		public int projType = -1;
		public int usetimetotal = 0;
		private List<int> spellbookused = new List<int>();

		public override bool CanUseItem(Player player)
        {
			if(player.selectedItem == 58)
			{
				return false;
			}
			bool flag2 = false;
			if(player.altFunctionUse != 2)
			{
				if (player.itemAnimation == 0 && usetimetotal * item.mana * player.manaCost <= player.statManaMax2 && player.statMana >= item.mana * player.manaCost && player.altFunctionUse != 2)
				{
					bool flag = false;
					int spellbookindex = -1;
					for (int m = 0; m < 50; m++)
					{
						Item itemshoot = player.inventory[m];
						
						if (itemshoot != null && itemshoot.stack > 0 && itemshoot.magic && itemshoot.useStyle == 5 && !Item.staff[itemshoot.type] && itemshoot.shoot > 0 && itemshoot.type != mod.ItemType("SpellBookofSefer") && (itemshoot.width > itemshoot.height * 0.8f) && (itemshoot.width < itemshoot.height * 1.25)) 
						{
							spellbookindex = m;
							spellbookdamage = itemshoot.damage;
							spellbooktype = itemshoot.type;
							projType = itemshoot.shoot;
							spellbookused.Add(itemshoot.type);
							flag = true;
							break;
						}
					}
					if (flag)
					{
						player.inventory[spellbookindex].stack -= 1;
						if (player.inventory[spellbookindex].stack <= 0)
						{
							player.inventory[spellbookindex].TurnToAir();
						}
						usetimetotal ++;
						return true;
					}
				}
				
				if(player.GetModPlayer<AAPlayer>().SpellBookofRagnarok)
				{
					flag2 = true;
				}
			}
			
			if(player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectSpellBook || (player.itemAnimation == 0 && (player.altFunctionUse == 2 || (player.GetModPlayer<AAPlayer>().SpellBookofRagnarok && (usetimetotal - 1) * item.mana * player.manaCost > player.statManaMax2) || flag2)))
			{
				if(player.GetModPlayer<AAPlayer>().SpellBookofRagnarok) 
				{
					
					player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectRagnarok = true;
					player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().spellbooknum = usetimetotal;
					if(usetimetotal >= 5)
					{
						bool hasbuff = false;
						for (int j = 0; j < 22; j++)
						{
							if (player.buffType[j] == mod.BuffType("SpellBookofRagnarok"))
							{
								hasbuff = true;
								player.buffTime[j] = 600;
								break;
							}
						}
						if(!hasbuff)
						{
							player.AddBuff(mod.BuffType("SpellBookofRagnarok"), 600);
						}
					}
				}
				usetimetotal = 0;
				foreach(int spelltype in spellbookused)
				{
					Item itemclone = new Item();
					itemclone.SetDefaults(spelltype, false);
					Item CreatItem = player.GetItem(player.whoAmI, itemclone, false, false);
					if (CreatItem.stack > 0)
					{
						int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, itemclone.type, 1, false, 0, true, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
						}
					}
					else
					{
						itemclone.position.X = player.Center.X - itemclone.width / 2;
						itemclone.position.Y = player.Center.Y - itemclone.height / 2;
						itemclone.active = true;
						ItemText.NewText(itemclone, 0, true, false);
					}
				}
				player.ClearBuff(mod.BuffType("SpellbookofSefer"));
				player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectSpellBook = false;
				spellbookused.Clear();
				spellbookdamage = -1;
				spellbooktype = -1;
				projType = -1;
				return false;
			}
			
            return false;
        }

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override void UpdateInventory(Player player)
        {
			if(spellbooktype >= 0 && (Main.ingameOptionsWindow || player.inventory[player.selectedItem].type != item.type || Main.HoverItem.type == item.type))
			{
				usetimetotal = 0;
				foreach(int spelltype in spellbookused)
				{
					Item item = new Item();
					item.SetDefaults(spelltype, false);
					Item CreatItem = player.GetItem(player.whoAmI, item, false, false);
					if (CreatItem.stack > 0)
					{
						int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 0, true, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
						}
					}
					else
					{
						item.position.X = player.Center.X - item.width / 2;
						item.position.Y = player.Center.Y - item.height / 2;
						item.active = true;
						ItemText.NewText(item, 0, true, false);
					}
				}
				spellbookdamage = -1;
				spellbooktype = -1;
				projType = -1;
				spellbookused.Clear();
				player.ClearBuff(mod.BuffType("SpellbookofSefer"));
				player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectSpellBook = false;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			bool hasbuff = false;
			for (int j = 0; j < 22; j++)
			{
				if (player.buffType[j] == mod.BuffType("SpellbookofSefer"))
				{
					hasbuff = true;
					player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectSpellBook = true;
					player.buffTime[j] = 300;
					break;
				}
			}
			if(!hasbuff)
			{
				player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectSpellBook = true;
				player.AddBuff(mod.BuffType("SpellbookofSefer"), 300);
			}
			Item item = new Item();
			item.SetDefaults(spellbooktype, false);
			player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().ShootSustainedeffect.Add(item);
			if(ItemLoader.GetItem(spellbooktype) != null)
			{
				ModItem moditem = ItemLoader.GetItem(spellbooktype);
				MethodInfo[] methods = moditem.GetType().GetMethods();
				try
				{
					foreach(MethodInfo method in methods)
					{
						if(method.Name == "Instanteffect")
						{
							MethodInfo Instanteffect = moditem.GetType().GetMethod("Instanteffect", BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Any, new Type[]{typeof(Player), typeof(Vector2), typeof(Vector2), typeof(int)}, null);
							Instanteffect.Invoke(moditem, new object[]{player, position, new Vector2(speedX, speedY), damage + (int)(spellbookdamage * player.magicDamage)});
						}
					}
				}
				catch
				{}
				try
				{
					MethodInfo Shoot = moditem.GetType().GetMethod("Shoot", BindingFlags.Instance | BindingFlags.Public);
					if(!(bool)Shoot.Invoke(moditem, new object[]{player, position, speedX, speedY, projType, damage + (int)(spellbookdamage * player.magicDamage), knockBack}))
					{
						return false;
					}
				}
				catch
				{}
			}
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, projType, damage + (int)(spellbookdamage * player.magicDamage), knockBack, player.whoAmI);
			return false;
		}
	}
}
