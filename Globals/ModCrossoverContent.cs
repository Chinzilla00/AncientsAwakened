using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using AAMod.Items;

namespace AAMod
{
    public class ModSupport
    {
        public static Mod thorium = null, calamity = null, redemption = null;

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

        public static bool ModInstalled(string name)
        {
            switch (name)
            {
                case "CalamityMod": return calamity != null;
                case "ThoriumMod": return thorium != null;
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
        }
    }

    public abstract class CrossoverItem : BaseAAItem
    {
        public string crossoverModName = "(N/A)";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (!ModSupport.ModInstalled(crossoverModName))
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