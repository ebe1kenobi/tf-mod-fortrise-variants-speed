using System;
using FortRise;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  /// <summary>
  /// Ouvre la fenetre de vitesse depuis la case SPEED de l'ecran des variantes.
  ///
  /// La variante est par joueur, donc le jeu a deja pris <c>Alt</c> pour sa fenetre
  /// des joueurs : la vitesse s'ouvre a <c>Alt2</c>. C'est la touche que le jeu
  /// reserve a EXPLAIN, mais seulement pour une variante qui a une description -
  /// la notre n'en a pas, la touche est donc libre, et le guide des boutons le dit.
  /// </summary>
  public class MyVariantToggle : IHookable
  {
    public static void Load(IHarmony harmony)
    {
      harmony.Patch(
          AccessTools.DeclaredMethod(typeof(VariantToggle), nameof(VariantToggle.Update)),
          postfix: new HarmonyMethod(Update_patch)
      );

      // OnSelect est protegee : nom en dur, pas de nameof possible.
      harmony.Patch(
          AccessTools.DeclaredMethod(typeof(VariantToggle), "OnSelect"),
          postfix: new HarmonyMethod(OnSelect_patch)
      );
    }

    /// <summary>Est-ce NOTRE case ? Le libelle est le seul repere fiable ici.</summary>
    private static bool IsOurs(VariantToggle toggle)
    {
      return toggle != null
          && toggle.Variant != null
          && string.Equals(toggle.Variant.Title, Variants.TITLE, StringComparison.Ordinal);
    }

    public static void Update_patch(VariantToggle __instance)
    {
      try
      {
        // Selected retombe des que le jeu ouvre sa propre fenetre des joueurs :
        // les deux ne peuvent donc pas s'ouvrir l'une sur l'autre.
        if (!__instance.Selected || !IsOurs(__instance) || !MenuInput.Alt2)
        {
          return;
        }

        Sounds.ui_click.Play(160f, 1f);
        __instance.Selected = false;

        SpeedPopup popup = new SpeedPopup(__instance, new Vector2(160f, __instance.Y));
        popup.TweenIn();
        __instance.MainMenu.Add<SpeedPopup>(popup);
      }
      catch (Exception e)
      {
        Logger.Info("[Variants] fenetre de vitesse impossible : " + e.Message);
      }
    }

    /// <summary>
    /// Annonce la touche dans le guide du bas.
    ///
    /// En postfix : le OnSelect du jeu vient de poser Alt/PLAYERS en ligne C et de
    /// VIDER la ligne D (notre variante n'a pas de description). On ecrit donc en D,
    /// ce qui laisse les deux touches visibles a la fois.
    /// </summary>
    public static void OnSelect_patch(VariantToggle __instance)
    {
      try
      {
        if (!IsOurs(__instance))
        {
          return;
        }

        __instance.MainMenu.ButtonGuideD.SetDetails(MenuButtonGuide.ButtonModes.Alt2, "SPEED");
      }
      catch (Exception e)
      {
        Logger.Info("[Variants] guide des boutons : " + e.Message);
      }
    }
  }
}
