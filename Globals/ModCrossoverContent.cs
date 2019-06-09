using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod
{
    public class ModSupport
    {
        public static Mod thorium = null, calamity = null, redemption = null;

        public static FieldInfo CRevengence = null, CDeath = null, CDefiled = null;

        public static bool Revengence(bool? value = null)
        {
            if (value != null && CRevengence != null)
            {
                CRevengence.SetValue(null, (bool)value);
                return (bool)value;

            }
            return (CRevengence == null ? false : (bool)CRevengence.GetValue(null));
        }

        public static bool Death(bool? value = null)
        {
            if (value != null && CDeath != null)
            {
                CDeath.SetValue(null, (bool)value);
                return (bool)value;
            }
            return (CDeath == null ? false : (bool)CDeath.GetValue(null));
        }

        public static bool Defiled(bool? value = null)
        {
            if (value != null && CDefiled != null)
            {
                CDefiled.SetValue(null, (bool)value);
                return (bool)value;
            }
            return (CDefiled == null ? false : (bool)CDefiled.GetValue(null));
        }

        public static bool ModInstalled(string name)
        {
            switch (name)
            {
                case "Calamity": return calamity != null;
                case "Thorium": return thorium != null;
                case "Redemption": return redemption != null;
                default: return false;
            }
        }

        public static bool forceBlackMapBG = false;
        public static Texture2D forceBlackMapTexture = null;

        public static Texture2D GetMapBackgroundImage()
        {
            return (forceBlackMapBG ? Main.mapTexture : null);
        }

        public static void SetupSupport()
        {
            thorium = ModLoader.GetMod("ThoriumMod");
            calamity = ModLoader.GetMod("CalamityMod");
            redemption = ModLoader.GetMod("Redemption");

            #region Calamity
            if (calamity != null)
            {
                FieldInfo worldList = calamity.GetType().GetField("worlds", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                Dictionary<string, ModWorld> worldDict = (Dictionary<string, ModWorld>)worldList.GetValue(calamity);
                FieldInfo[] finfo = worldDict["CalamityWorld"].GetType().GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                for (int m = 0; m < finfo.Length; m++)
                {
                    FieldInfo info = finfo[m];
                    string fname = info.Name.ToLower();
                    switch (fname)
                    {
                        default: break;
                        case "revenge": CRevengence = info; break;
                        case "death": CDeath = info; break;
                        case "defiled": CDefiled = info; break;
                    }
                }
            }
            #endregion
        }
    }

    public abstract class CrossoverItem : ModItem
    {
        public string crossoverModName = "(N/A)";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (!ModSupport.ModInstalled(crossoverModName)) //this is to give a warning if they have the item and the mod is not enabled
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
                if (ModSupport.thorium != null)
                {
                    float? boost = (float?)ModSupport.thorium.Call("GetRadiantBoost", player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.thorium != null)
                {
                    ModSupport.thorium.Call("SetRadiantBoost", player.whoAmI, value);
                }
            }
        }
        public int Thorium_radiantCrit
        {
            get
            {
                if (ModSupport.thorium != null)
                {
                    int? boost = (int?)ModSupport.thorium.Call("GetRadiantCrit", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.thorium != null)
                {
                    ModSupport.thorium.Call("SetRadiantCrit", player.whoAmI, value);
                }
            }
        }
        public int Thorium_healBonus
        {
            get
            {
                if (ModSupport.thorium != null)
                {
                    int? boost = (int?)ModSupport.thorium.Call("GetHealBonus", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.thorium != null)
                {
                    ModSupport.thorium.Call("SetHealBonus", player.whoAmI, value);
                }
            }
        }
        #endregion

        #region Redemption

        public float Redemption_druidicBoost
        {
            get
            {
                if (ModSupport.redemption != null)
                {
                    float? boost = (float?)ModSupport.redemption.Call("GetDruidicBoost", player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.redemption != null)
                {
                    ModSupport.redemption.Call("SetDruidicBoost", player.whoAmI, value);
                }
            }
        }
        public int Redemption_druidicCrit
        {
            get
            {
                if (ModSupport.redemption != null)
                {
                    int? boost = (int?)ModSupport.redemption.Call("GetDruidicCrit", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.redemption != null)
                {
                    ModSupport.redemption.Call("SetDruidicCrit", player.whoAmI, value);
                }
            }
        }

        #endregion
    }
}