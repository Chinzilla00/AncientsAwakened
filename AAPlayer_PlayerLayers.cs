using AAMod.Items;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod
{
    public partial class AAPlayer : ModPlayer
    {
        public override void ModifyDrawLayers(List<PlayerLayer> list)
        {
            AddPlayerLayer(list, glAfterHead, PlayerLayer.Head);
            AddPlayerLayer(list, glAfterBody, PlayerLayer.Body);
            AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms);
            AddPlayerLayer(list, glAfterHandOn, PlayerLayer.HandOnAcc);
            AddPlayerLayer(list, glAfterHandOff, PlayerLayer.HandOffAcc);
            AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms);
            AddPlayerLayer(list, glAfterWep, PlayerLayer.HeldItem);
            AddPlayerLayer(list, glAfterLegs, PlayerLayer.Legs);
            AddPlayerLayer(list, glAfterShield, PlayerLayer.ShieldAcc);
            AddPlayerLayer(list, glAfterNeck, PlayerLayer.NeckAcc);
            AddPlayerLayer(list, glAfterFace, PlayerLayer.FaceAcc);

            if (!player.merman && !player.wereWolf && groviteGlow[player.whoAmI])
            {
                BaseDrawing.AddPlayerLayer(list, glGroviteHead, PlayerLayer.Head, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteBody, PlayerLayer.Body, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteLegs, PlayerLayer.Legs, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteArm, PlayerLayer.Arms, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteWings, PlayerLayer.Wings, false);
            }

            AddPlayerLayer(list, glAfterAll, list[list.Count - 1]);
        }

        public static void AddPlayerLayer(List<PlayerLayer> layers, PlayerLayer layer, PlayerLayer parent)
        {
            int index = layers.IndexOf(parent);
            if (index != -1)
            {
                layers.Insert(index + 1, layer);
            }
        }

        public PlayerLayer glAfterWep = new PlayerLayer("AAMod", "glAfterWep", PlayerLayer.HeldItem, delegate (PlayerDrawInfo edi)
        {
            if (edi.shadow != 0)
            {
                return;
            }

            Player drawPlayer = edi.drawPlayer;
            Item heldItem = drawPlayer.inventory[drawPlayer.selectedItem];
            BaseAAItem baseAAItem = null;

            if (heldItem.modItem != null && heldItem.modItem is BaseAAItem)
            {
                baseAAItem = (BaseAAItem)heldItem.modItem;
            }

            if (baseAAItem != null && baseAAItem.glowmaskTexture != null && baseAAItem.glowmaskDrawType != BaseAAItem.GLOWMASKTYPE_NONE)
            {
                Vector2? offsetNull = baseAAItem.HoldoutOffset();
                Vector2 offset = (offsetNull != null) ? (Vector2)offsetNull : Vector2.Zero;

                if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_SWORD)
                {
                    BaseDrawing.DrawHeldSword(Main.playerDrawData, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, null, 1, AAMod.instance.GetTexture(baseAAItem.glowmaskTexture));
                }
                else if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_GUN)
                {
                    BaseDrawing.DrawHeldGun(Main.playerDrawData, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, false, false, 0f, 0f, null, 1, AAMod.instance.GetTexture(baseAAItem.glowmaskTexture));
                }
            }
        });

        public PlayerLayer glAfterHead = new PlayerLayer("AAMod", "glAfterHead", PlayerLayer.Head, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);

            Vector2 position = edi.position;
            int dyeHead = edi.headArmorShader;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumVisor")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumHeadgear")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumHeadgear_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumHood")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsHelm_Glow_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueFleshrendHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueFleshrendHelm_Glow_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayHelmet_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterVisor")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterHelmet_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHeaddress")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterHeaddress_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHat")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHat_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHelm_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHelmet_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHeadgear")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHeadgear_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumMask_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("GripMaskRed")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/GripMaskRed_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DaybringerMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DaybringerMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("NightcrawlerMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/NightcrawlerMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("RetrieverMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/RetrieverMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ZeroMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ZeroMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TiedMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TiedMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.FlashGlow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("LizEars")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/LizEars_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystalHelm")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/StarcrystalHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneHelmet")) && drawPlayer.statLife > 40)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HeartstoneHelmet_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomHat")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomHat_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DJDuckHead")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DJDuckHead_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteVisor")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosKabuto")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosKabutoBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosKabuto_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosMask")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosMaskBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosHood")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosHoodBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosVisor")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosVisorBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueBlazingKabuto")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueBlazingKabuto_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenHelmet_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("GibsSkull")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/GibsSkull_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedHood")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("LeviathanHelm")) && drawPlayer.statLife < (drawPlayer.statLifeMax2 / 2))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/LeviathanHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Ichor, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HoodlumHood")) && drawPlayer.statLife < (drawPlayer.statLifeMax2 / 2))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HoodlumHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterShield = new PlayerLayer("AAMod", "glAfterShield", PlayerLayer.ShieldAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("TaiyangBaolei")))
            {
                string texturePath = Main.dayTime ? "Glowmasks/TaiyangBaoleiA_Shield_Glow" : "Glowmasks/TaiyangBaolei_Shield_Glow";
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture(texturePath), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType<Items.Accessories.CapShield>()) && !AAGlobalProjectile.AnyProjectiless(mod.ProjectileType<Projectiles.CapShield>()))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Accessories/CapShield_ShieldTex"), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterFace = new PlayerLayer("AAMod", "glAfterFace", PlayerLayer.FaceAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("SoulStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/SoulStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("SpaceStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/SpaceStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("RealityStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/RealityStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("TimeStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/TimeStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("PowerStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/PowerStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("MindStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/MindStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
        });

        public PlayerLayer glAfterNeck = new PlayerLayer("AAMod", "glAfterNeck", PlayerLayer.NeckAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("Naitokurosu")))
            {
                string texturePath = Main.dayTime ? "Glowmasks/Naitokurosu_Neck_Glow" : "Glowmasks/NaitokurosuA_Neck_Glow";
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture(texturePath), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterHandOn = new PlayerLayer("AAMod", "glAfterHandOn", PlayerLayer.HandOnAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DemonGauntlet")))
            {
                Texture2D Glow = mod.GetTexture("Glowmasks/DemonGauntlet_HandsOn_Glow");
                Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, Glow, edi.handOnShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterHandOff = new PlayerLayer("AAMod", "glAfterHandOff", PlayerLayer.HandOffAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DemonGauntlet")))
            {
                Texture2D Glow = mod.GetTexture("Glowmasks/DemonGauntlet_HandsOff_Glow");
                Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, Glow, edi.handOffShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterBody = new PlayerLayer("AAMod", "glAfterBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumChestplate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsPlate_Glow_" + (drawPlayer.Male ? "Body" : "Female")), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueFleshrendPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueFleshrendPlate_Glow_" + (drawPlayer.Male ? "Body" : "Female")), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayChestplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumPlatemail")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystaPlate")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/StarcrystaPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneChestguard")) && drawPlayer.statLife > 40)
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/HeartstoneChestguard_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomShirt_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DJDuckShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DJDuckShirt_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteBreastplate")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteBreastplate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueBlazingDou")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueBlazingDou_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosPlate")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_" + (drawPlayer.Male ? "Body" : "Female")), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenPlate_" + (drawPlayer.Male ? "Body" : "Female")), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedRobe")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedRobe_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterArm = new PlayerLayer("AAMod", "glAfterArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumChestplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsPlate_Glow_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayChestplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumPlatemail")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystaPlate")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/StarcrystalPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneChestguard")) && drawPlayer.statLife > 40)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HeartstoneChestguard_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomShirt_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteBreastplate")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueBlazingDou")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueBlazingDou_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosPlate")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenPlate_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedRobe")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedRobe_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterLegs = new PlayerLayer("AAMod", "glAfterLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoLeggings_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumBoots")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumBoots_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsBoots")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsBoots_Glow_Legs"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Uranium, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayLeggings_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterGreaves")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterGreaves_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumCuisses")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumCuisses_Legs"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystalBoots")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/StarcrystalBoots_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneLeggings")) && drawPlayer.statLife < 40)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HeartstoneLeggings_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedGreaves")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedGreaves_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomPants")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomPants_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteGreaves")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteGreaves_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperLeggings_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueBlazingSuneate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueBlazingSuneate_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosGreaves")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosGreavesBlue_Legs"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.legFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosGreaves_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenBoots")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenBoots_Legs"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedPants")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedPants_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.legFrame);
            }
        });

        #region Grovite Layers
        public PlayerLayer glGroviteHead = new PlayerLayer("AAMod", "glGroviteHead", PlayerLayer.Head, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateHood")))
            {
                Texture2D tex = mod.GetTexture("Glowmasks/AngryPirateHood_Head_Glow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.headArmorShader, drawPlayer, drawPlayer.bodyFrame, drawPlayer.headRotation, drawPlayer.headPosition, edi.headOrigin);
            }
        });

        public PlayerLayer glGroviteBody = new PlayerLayer("AAMod", "glGroviteBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateCofferplate")))
            {
                Texture2D tex = mod.GetTexture("GroviteCofferplateBodyGlow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.bodyArmorShader, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, edi.bodyOrigin);
            }
        });

        public PlayerLayer glGroviteLegs = new PlayerLayer("AAMod", "glGroviteLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && (!drawPlayer.mount.Active || drawPlayer.mount.Type != 6) && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateBoots")))
            {
                Texture2D tex = mod.GetTexture("Glowmasks/AngryPirateBoots_Legs_Glow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.legArmorShader, drawPlayer, drawPlayer.legFrame, drawPlayer.legRotation, drawPlayer.legPosition, edi.legOrigin);
            }
        });

        public PlayerLayer glGroviteArm = new PlayerLayer("AAMod", "glGroviteArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateCofferplate")))
            {
                Texture2D tex = mod.GetTexture("Glowmasks/AngryPirateCofferplate_Arms_Glow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.bodyArmorShader, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, edi.bodyOrigin);
            }
        });

        public PlayerLayer glGroviteWings = new PlayerLayer("AAMod", "glGroviteWings", PlayerLayer.Wings, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            int accSlot = 0;
            bool social = false;

            if (edi.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateSails"), ref social, ref accSlot))
            {
                int dye = BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                if (dye == -1)
                {
                    dye = 0;
                }

                DrawFlickerTexture(1, Main.playerDrawData, edi, mod.GetTexture("Glowmasks/AngryPirateSails_Wings/Glow"), dye, drawPlayer);
            }
        });
        #endregion

        public PlayerLayer glAfterAll = new PlayerLayer("AAMod", "glAfterAll", delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (drawPlayer.mount.Active)
            {
                return;
            }

            if (drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale > 0)
            {
                Texture2D Shield = mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusShield");
                BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);

                Texture2D Ring = mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusFreeRing");
                BaseDrawing.DrawTexture(Main.spriteBatch, Ring, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale, drawPlayer.GetModPlayer<AAPlayer>(mod).RingRotation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);

                Texture2D RingGlow = mod.GetTexture("Glowmasks/SagittariusFreeRing_Glow");
                BaseDrawing.DrawTexture(Main.spriteBatch, RingGlow, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale, drawPlayer.GetModPlayer<AAPlayer>(mod).RingRotation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), ColorUtils.COLOR_GLOWPULSE, true);
            }

            if (drawPlayer.GetModPlayer<AAPlayer>(mod).TimeScale > 0)
            {
                Texture2D Ring = mod.GetTexture("Items/Accessories/TimeRing");
                BaseDrawing.DrawTexture(Main.spriteBatch, Ring, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).TimeScale, drawPlayer.GetModPlayer<AAPlayer>(mod).RingRotation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), AAColor.COLOR_WHITEFADE1, true);
            }
        });

        public static bool HasAndCanDraw(Player player, int type)
        {
            int dum = 0;
            bool dummy = false;

            return HasAndCanDraw(player, type, ref dummy, ref dum);
        }

        public static bool HasAndCanDraw(Player player, int type, ref bool social, ref int slot)
        {
            if (player.wereWolf || player.merman)
            {
                return false;
            }

            ModItem mitem = ItemLoader.GetItem(type);
            if (mitem != null)
            {
                Item item = mitem.item;
                if (item.headSlot > 0)
                {
                    return BasePlayer.HasHelmet(player, type) && BaseDrawing.ShouldDrawHelmet(player, type);
                }
                else if (item.bodySlot > 0)
                {
                    return BasePlayer.HasChestplate(player, type) && BaseDrawing.ShouldDrawChestplate(player, type);
                }
                else if (item.legSlot > 0)
                {
                    return BasePlayer.HasLeggings(player, type) && BaseDrawing.ShouldDrawLeggings(player, type);
                }
                else if (item.accessory)
                {
                    return BasePlayer.HasAccessory(player, type, true, true, ref social, ref slot) && BaseDrawing.ShouldDrawAccessory(player, type);
                }
            }

            return false;
        }

        public static void DrawFlickerTexture(int drawType, object sb, PlayerDrawInfo edi, Texture2D tex, int shader, Player drawPlayer, Rectangle frame = default, float rotation = 0, Vector2 drawPos = default, Vector2 framePos = default)
        {
            if (drawPlayer == null || !drawPlayer.active || drawPlayer.dead)
            {
                return;
            }

            for (int j = 0; j < 7; j++)
            {
                Color color = new Color(110 - j * 10, 110 - j * 10, 110 - j * 10, 110 - j * 10);
                Vector2 vector = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)) * 0.4f;

                if (drawType == 2)
                {
                    BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : BaseDrawing.GetYOffset(drawPlayer)) + vector.Y, color, frame);
                }
                else
                {
                    bool wings = drawType == 1;
                    if (wings)
                    {
                        rotation = drawPlayer.bodyRotation;
                        frame = new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4);
                        framePos = new Vector2(Main.wingsTexture[drawPlayer.wings].Width / 2, Main.wingsTexture[drawPlayer.wings].Height / 8);
                    }

                    Vector2 pos = Vector2.Zero;
                    int x = 0;
                    int y = 0;

                    if (wings)
                    {
                        x = (int)(edi.position.X - Main.screenPosition.X + drawPlayer.width / 2 - 9 * drawPlayer.direction);
                        y = (int)(edi.position.Y - Main.screenPosition.Y + drawPlayer.height / 2 + 2f * drawPlayer.gravDir);
                        pos = new Vector2(x, y);
                    }
                    else
                    {
                        x = (int)(edi.position.X - Main.screenPosition.X - frame.Width / 2 + drawPlayer.width / 2);
                        y = (int)(edi.position.Y - Main.screenPosition.Y + drawPlayer.height - frame.Height + 4f);
                        pos = new Vector2(x, y);
                    }

                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0)
                        {
                            shader = shader
                        };
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else if (sb is SpriteBatch)
                    {
                        ((SpriteBatch)sb).Draw(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0);
                    }
                }
            }
        }
    }
}
