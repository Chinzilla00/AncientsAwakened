using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod
{
    public class ModSupport
    {
        public static Mod thorium = null, calamity = null;

        public static FieldInfo Revengance = null, Death = null, Defiled = null;

        public static bool Calamity_Revengence(bool? value = null)
        {
            if (value != null && Revengance != null)
            {
                Revengance.SetValue(null, (bool)value);
                return (bool)value;

            }
            return (Revengance == null ? false : (bool)Revengance.GetValue(null));
        }

        public static bool Calamity_Death(bool? value = null)
        {
            if (value != null && Death != null)
            {
                Death.SetValue(null, (bool)value);
                return (bool)value;
            }
            return (Death == null ? false : (bool)Death.GetValue(null));
        }

        public static bool Calamity_Defiled(bool? value = null)
        {
            if (value != null && Defiled != null)
            {
                Defiled.SetValue(null, (bool)value);
                return (bool)value;
            }
            return (Defiled == null ? false : (bool)Defiled.GetValue(null));
        }

        public static bool ModInstalled(string name)
        {
            switch (name)
            {
                case "Calamity": return calamity != null;
                case "Thorium": return thorium != null;
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
            Mod mod = AAMod.instance;
            thorium = ModLoader.GetMod("ThoriumMod");
            calamity = ModLoader.GetMod("CalamityMod");

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
                        case "revenge": Revengance = info; break;
                        case "death": Death = info; break;
                        case "defiled": Defiled = info; break;
                    }
                }
            }
            #endregion
        }
    }

    public abstract class CrossoverItem : ModItem
    {
        public string crossoverModName = "THORIUM";

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
    }


}