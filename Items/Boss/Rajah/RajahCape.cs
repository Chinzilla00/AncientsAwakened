using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Rajah
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class RajahCape : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit's Cloak of Supremecy");
            Tooltip.SetDefault(@"Every 10% of health lost gives you:
1. 12% extra attack power to your highest damage type boost
2. 5% increased movement speed
3. 4% damage resistance
All effects of the Sash of Vengeance
'You have been deemed a worthy successor by the Champion of the Innocent'");
        }

        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
            item.defense = 10;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            Color damageColor = Color.Firebrick;
            string DamageType = "";

            if (modPlayer.MeleeHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipMelee");
                damageColor = Color.Firebrick;
            }
            else if (modPlayer.RangedHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipRanged");
                damageColor = Color.SeaGreen;
            }
            else if (modPlayer.MagicHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipMagic");
                damageColor = Color.Violet;
            }
            else if (modPlayer.SummonHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipSummoning");
                damageColor = Color.Cyan;
            }
            else if (modPlayer.ThrownHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipThrowing");
                damageColor = Color.DarkOrange;
            }

            string DamageAmount = (10 * DamageBoost(player)) + "% ";
            TooltipLine DamageToltip = new TooltipLine(mod, "Damage Type", Language.GetTextValue("Mods.AAMod.Common.RajahSPDamageBoost") + DamageAmount + DamageType + Language.GetTextValue("Mods.AAMod.Common.RajahSPDamageInfo"))
            {
                overrideColor = damageColor
            };
            tooltips.Add(DamageToltip);

            string SpeedAmount = (10 * Speed(player)) + "% ";
            TooltipLine SpeedTooltip = new TooltipLine(mod, "Damage Type", Language.GetTextValue("Mods.AAMod.Common.RajahSPSpeedBoost") + SpeedAmount);
            tooltips.Add(SpeedTooltip);

            string ResAmount = (10 * DamageRes(player)) + "% ";
            TooltipLine ResTooltip = new TooltipLine(mod, "Damage Type", Language.GetTextValue("Mods.AAMod.Common.RajahSPDamageResistance") + ResAmount);
            tooltips.Add(ResTooltip);

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            player.autoJump = true;
            Player.jumpHeight = 40;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            player.moveSpeed += Speed(player);
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += Speed(player);
            player.endurance += DamageRes(player);

            if (modPlayer.MeleeHighest(player))
            {
                player.meleeDamage += DamageBoost(player);
            }
            else if (modPlayer.RangedHighest(player))
            {
                player.rangedDamage += DamageBoost(player);
            }
            else if (modPlayer.MagicHighest(player))
            {
                player.magicDamage += DamageBoost(player);
            }
            else if (modPlayer.SummonHighest(player))
            {
                player.minionDamage += DamageBoost(player);
            }
            else if (modPlayer.ThrownHighest(player))
            {
                player.thrownDamage += DamageBoost(player);
            }
        }

        public float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return 1.08f;
            }
            else if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .96f;
            }
            else if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .84f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .72f;
            }
            else if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .60f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .48f;
            }
            else if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .36f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .24f;
            }
            else if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .12f;
            }

            return 0f;
        }

        public float DamageRes(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return .18f;
            }
            else if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .16f;
            }
            else if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .14f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .12f;
            }
            else if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .1f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .08f;
            }
            else if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .06f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .04f;
            }
            else if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .02f;
            }

            return 0f;
        }

        public float Speed(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return .45f;
            }
            else if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .4f;
            }
            else if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .35f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .3f;
            }
            else if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .25f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .2f;
            }
            else if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .15f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .1f;
            }
            else if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .05f;
            }

            return 0f;
        }
    }
    
}