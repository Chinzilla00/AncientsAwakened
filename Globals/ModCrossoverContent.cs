using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using AAMod.Items;
using System;
using System.Reflection;

namespace AAMod
{
    public class ModSupport
    {

        public static FieldInfo CRevengence = null, CDeath = null, CDefiled = null;

        /*
        public static bool Revengence
        {
            get
            {
                if (calamity != null)
                {
                    if (calamity.Version >= new Version(1, 4, 2, 201))
                    {
                        return CalamityMod.world.CalamityWorld.revenge;
                    }
                    else
                    {
                        return CalamityMod.CalamityWorld.revenge;
                    }
                    
                }
                return false;
            }
        }

        public static bool Death
        {
            get
            {
                if (calamity != null)
                {
                    if (calamity.Version >= new Version(1, 4, 2, 201))
                    {
                        return CalamityMod.world.CalamityWorld.death;
                    }
                    else
                    {
                        return CalamityMod.CalamityWorld.death;
                    }
                    
                }
                return false;
            }
        }

        public static bool Defiled
        {
            get
            {
                if (calamity != null)
                {
                    if (calamity.Version >= new Version(1, 4, 2, 201))
                    {
                        return CalamityMod.world.CalamityWorld.defiled;
                    }
                    else
                    {
                        return CalamityMod.CalamityWorld.defiled;
                    }
                }
                return false;
            }
        }*/

        public static bool forceBlackMapBG = false;
        public static Texture2D forceBlackMapTexture = null;

        public static Texture2D GetMapBackgroundImage()
        {
            return forceBlackMapBG ? Main.mapTexture : null;
        }

        public static Mod GetMod(string modname)
		{
            if(ModLoader.GetMod(modname) != null)
			{
                return ModLoader.GetMod(modname);
            }
            return null;
        }

        public static ModItem GetModItem(string modname, string itemname)
		{
			ModItem item = new ModItem();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					item = mod.GetItem(itemname);
				}
				catch(Exception)
				{
					item = null;
					throw new Exception("Can't find this item" + itemname);
				}
			}

			return item;
		}

        public static ModNPC GetModNPC(string modname, string npcname)
		{
			ModNPC npc = new ModNPC();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					npc = mod.GetNPC(npcname);
				}
				catch(Exception)
				{
					npc = null;
					throw new Exception("Can't find this npc" + npcname);
				}
			}

			return npc;
		}

        public static ModProjectile GetModProjectile(string modname, string projname)
		{
			ModProjectile projectile = new ModProjectile();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					projectile = mod.GetProjectile(projname);
				}
				catch(Exception)
				{
					projectile = null;
					throw new Exception("Can't find this projectile" + projname);
				}
			}

			return projectile;
		}

        public static ModDust GetModDust(string modname, string dustname)
		{
            ModDust dust = new ModDust();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					dust = mod.GetDust(dustname);
				}
				catch(Exception)
				{
					dust = null;
					throw new Exception("Can't find this projectile" + dustname);
				}
			}

			return dust;
        }

        public static ModBuff GetModBuff(string modname, string buffname)
		{
            ModBuff buff = new ModBuff();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					buff = mod.GetBuff(buffname);
				}
				catch(Exception)
				{
					buff = null;
					throw new Exception("Can't find this buff" + buffname);
				}
			}

			return buff;
        }

        public static object GetModWorldConditions(string modname, string worldname, string ConditionName, bool nopub = false, bool sta = false)
		{
			object condition = null;
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					ModWorld world = mod.GetModWorld(worldname);
					if(world != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						condition = world.GetType().GetField(ConditionName, binding).GetValue(world);
					}
				}
				catch(Exception)
				{
					condition = null;
					throw new Exception("Error in reading world data.");
				}
            }
			return condition;
        }

        public static object GetModPlayerConditions(string modname, string playername, string ConditionName, bool nopub = false, bool sta = false)
		{
            object condition = false;
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					ModPlayer player = Main.player[Main.myPlayer].GetModPlayer(mod, playername);
					if(player != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						condition = (bool)player.GetType().GetField(ConditionName, binding).GetValue(player);
					}
				}
				catch(Exception)
				{
					condition = false;
					throw new Exception("Error in reading modplayer data.");
				}
            }
			return condition;
        }
    }

    public abstract class CrossoverItem : BaseAAItem
    {
        public string crossoverModName = "(N/A)";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (ModSupport.GetMod(crossoverModName) != null)
            {
                TooltipLine error = new TooltipLine(mod, "Error", "WARNING: ITEM WILL NOT FUNCTION WITHOUT " + crossoverModName.ToUpper() + " ENABLED!")
                {
                    overrideColor = new Color(255, 50, 50)
                };
                list.Add(error);
            }
        }
    }

    public class ModSupportPlayer : ModPlayer
    {
        #region Thorium
        public float Thorium_radiantBoost
        {
            get
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    float? boost = (float?)ModSupport.GetMod("ThoriumMod").Call("GetRadiantBoost", player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    ModSupport.GetMod("ThoriumMod").Call("SetRadiantBoost", player.whoAmI, value);
                }
            }
        }
        public int Thorium_radiantCrit
        {
            get
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    int? boost = (int?)ModSupport.GetMod("ThoriumMod").Call("GetRadiantCrit", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    ModSupport.GetMod("ThoriumMod").Call("SetRadiantCrit", player.whoAmI, value);
                }
            }
        }
        public int Thorium_healBonus
        {
            get
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    int? boost = (int?)ModSupport.GetMod("ThoriumMod").Call("GetHealBonus", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    ModSupport.GetMod("ThoriumMod").Call("SetHealBonus", player.whoAmI, value);
                }
            }
        }
        #endregion

        #region Redemption

        public float Redemption_druidicBoost
        {
            get
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    float? boost = (float?)ModSupport.GetMod("Redemption").Call("GetDruidicBoost", player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    ModSupport.GetMod("Redemption").Call("SetDruidicBoost", player.whoAmI, value);
                }
            }
        }
        public int Redemption_druidicCrit
        {
            get
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    int? boost = (int?)ModSupport.GetMod("Redemption").Call("GetDruidicCrit", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    ModSupport.GetMod("Redemption").Call("SetDruidicCrit", player.whoAmI, value);
                }
            }
        }

        #endregion
    }
}