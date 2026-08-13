using System;
using FortRise;
using HarmonyLib;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  /// <summary>
  /// La variante accelere l'ARCHER, exactement comme les bottes de vitesse du jeu.
  ///
  /// Avant, elle accelerait le JEU : <c>Engine.TimeRate = 1.3</c>. Tout allait plus
  /// vite - les fleches, les ennemis, les plateformes, les animations, la musique
  /// meme - ce qui ne rend pas un archer plus rapide, seulement la partie plus
  /// courte. Et cela cassait tout ce qui compte en images : minuteurs de manche,
  /// enregistrements, cadence des variantes des autres mods.
  ///
  /// TowerFall, lui, a une seule ligne pour ses bottes, dans <c>MaxRunSpeed</c> :
  /// <c>if (HasSpeedBoots) num *= 1.4f;</c>. On fait la meme chose, avec un facteur
  /// reglable. Rien d'autre ne bouge : ni le saut, ni l'esquive, ni l'inertie -
  /// c'est le plafond de la course au sol et en l'air qui change, et il se sent
  /// tout de suite sans deregler quoi que ce soit d'autre.
  /// </summary>
  public class MyPlayer : IHookable
  {
    public static void Load(IHarmony harmony)
    {
      // MaxRunSpeed est une propriete PRIVEE : nom en dur, pas de nameof possible.
      //
      // Assez grosse pour ne pas etre incrustee par le JIT - elle traverse
      // Level.Session.MatchSettings.Variants pour l'encombrement, teste la boue et
      // les bottes - donc un postfix la voit reellement passer.
      harmony.Patch(
          AccessTools.DeclaredMethod(typeof(Player), "get_MaxRunSpeed"),
          postfix: new HarmonyMethod(MaxRunSpeed_patch)
      );
    }

    /// <summary>
    /// Multiplie le plafond de course de cet archer.
    ///
    /// Se cumule avec les vraies bottes : un archer qui les ramasse par-dessus la
    /// variante va encore plus vite, comme deux bonus de vitesse devraient le faire.
    /// </summary>
    public static void MaxRunSpeed_patch(Player __instance, ref float __result)
    {
      try
      {
        // Les variantes du NIVEAU en cours, et non celles du menu principal :
        // IVariantEntry.IsActive() sans argument lit MainMenu.CurrentMatchSettings,
        // qui n'est pas forcement le match en train de se jouer.
        MatchVariants variants = __instance.Level?.Session?.MatchSettings?.Variants;
        if (variants == null || !Variants.Speed.IsActive(variants, __instance.PlayerIndex))
        {
          return;
        }

        __result *= TFModFortRiseVariantSpeedModule.Settings.Factor;
      }
      catch (Exception)
      {
        // Une vitesse est un confort ; une exception ici tomberait a CHAQUE image
        // d'update de chaque archer. On laisse la vitesse normale.
      }
    }
  }
}
