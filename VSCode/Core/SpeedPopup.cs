using System;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  /// <summary>
  /// La fenetre qui regle la vitesse, ouverte sur la case SPEED de l'ecran des
  /// variantes.
  ///
  /// Elle est calquee sur <c>VariantPerPlayer</c>, la fenetre que le jeu ouvre lui
  /// meme sur ses variantes par joueur : meme panneau, meme entree en glissant
  /// depuis la droite, meme sortie au bouton retour, memes sons. C'est ce qui la
  /// fait passer pour une fenetre du jeu et non pour un menu greffe dessus - et
  /// c'est aussi pourquoi elle herite de MenuItem : le MainMenu ne sait faire
  /// glisser, selectionner et retirer que ca.
  ///
  /// Elle remplace cinq cases de variante (Speedx1 a Speedx5) qui disaient la meme
  /// chose a cinq vitesses fixes.
  /// </summary>
  public class SpeedPopup : MenuItem
  {
    private const float PanelWidth = 160f;
    private const float PanelHeight = 46f;

    private readonly VariantToggle toggle;
    private readonly Vector2 tweenFrom;
    private readonly Vector2 tweenTo;

    private readonly Wiggler wiggler;
    private readonly SineWave arrowSine;

    public SpeedPopup(VariantToggle toggle, Vector2 position) : base(position)
    {
      // Devant les cases de variantes, qui vivent a une profondeur ordinaire.
      Depth = -100;

      this.toggle = toggle;
      tweenTo = Position;
      tweenFrom = Position + new Vector2(320f, 0f);

      wiggler = Wiggler.Create(20, 5f, null, null, false, false);
      Add(wiggler);

      arrowSine = new SineWave(60);
      Add(arrowSine);
    }

    private static TFModFortRiseVariantSpeedSettings Settings
        => TFModFortRiseVariantSpeedModule.Settings;

    public override void Added()
    {
      base.Added();

      // Le bouton retour doit refermer la FENETRE et non quitter l'ecran des
      // variantes : c'est ce que fait le jeu pour sa fenetre des joueurs.
      MainMenu.BackState = MainMenu.MenuState.Variants;
    }

    public override void Removed()
    {
      MainMenu.BackState = MainMenu.MenuState.VersusOptions;
      base.Removed();
    }

    public override void Update()
    {
      base.Update();

      if (!Selected)
      {
        return;
      }

      if (MenuInput.Left)
      {
        Change(-TFModFortRiseVariantSpeedSettings.Step);
      }
      else if (MenuInput.Right)
      {
        Change(TFModFortRiseVariantSpeedSettings.Step);
      }

      // Confirmer ferme aussi : une fois la vitesse choisie, c'est le geste
      // naturel, et il n'y a rien d'autre a valider ici.
      if (MenuInput.Back || MenuInput.Alt2 || MenuInput.Confirm)
      {
        TweenOut();
        Sounds.ui_clickBack.Play(160f, 1f);
      }
    }

    /// <summary>
    /// Bouge la vitesse d'un cran, en butant aux bornes. Le son n'est joue que si
    /// la valeur a REELLEMENT change : sinon maintenir la direction contre une
    /// borne fait crepiter le menu.
    /// </summary>
    private void Change(int delta)
    {
      var settings = Settings;
      int before = settings.speedPercent;

      int after = before + delta;
      if (after < TFModFortRiseVariantSpeedSettings.MinPercent)
      {
        after = TFModFortRiseVariantSpeedSettings.MinPercent;
      }
      if (after > TFModFortRiseVariantSpeedSettings.MaxPercent)
      {
        after = TFModFortRiseVariantSpeedSettings.MaxPercent;
      }

      if (after == before)
      {
        return;
      }

      settings.speedPercent = after;
      wiggler.Start();
      Sounds.ui_move1.Play(160f, 1f);
    }

    public override void Render()
    {
      base.Render();

      MenuPanel.DrawPanel(X - PanelWidth / 2f, Y - PanelHeight / 2f, PanelWidth, PanelHeight);
      Draw.TextCentered(TFGame.Font, "SPEED", Position + new Vector2(0f, -13f), Color.White);

      // La valeur au centre, grosse : c'est la seule chose qu'on vient regler.
      float scale = 2f + wiggler.Value * 0.3f;
      Draw.TextCentered(TFGame.Font, Label(), Position + new Vector2(0f, 6f),
          VariantItem.ActiveSelection, Vector2.One * scale, 0f);

      DrawArrow(-62f, true);
      DrawArrow(62f, false);
    }

    /// <summary>"x1.40", avec le point decimal quelle que soit la langue du systeme.</summary>
    private static string Label()
    {
      int percent = Settings.speedPercent;
      return "x" + (percent / 100).ToString() + "." + (percent % 100).ToString("D2");
    }

    /// <summary>
    /// Les chevrons de part et d'autre. Ils s'eteignent contre une borne : c'est
    /// ce qui dit qu'on est au bout sans avoir a essayer.
    /// </summary>
    private void DrawArrow(float offsetX, bool left)
    {
      bool enabled = left
          ? Settings.speedPercent > TFModFortRiseVariantSpeedSettings.MinPercent
          : Settings.speedPercent < TFModFortRiseVariantSpeedSettings.MaxPercent;

      Color color = enabled ? Color.White : Color.Gray * 0.5f;
      float breathe = enabled ? 1f + 0.15f * arrowSine.Value : 1f;

      Draw.TextCentered(TFGame.Font, left ? "<" : ">",
          Position + new Vector2(offsetX, 4f), color, Vector2.One * breathe, 0f);
    }

    public override void TweenIn()
    {
      Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 20, true);
      tween.OnUpdate = delegate (Tween t)
      {
        Position = Vector2.Lerp(tweenFrom, tweenTo, t.Eased);
      };
      tween.OnComplete = delegate (Tween t)
      {
        Selected = true;
      };
      Add(tween);
    }

    public override void TweenOut()
    {
      Selected = false;

      // Le reglage part sur le disque en refermant, et pas seulement en quittant le
      // jeu : voir TFModFortRiseVariantSpeedModule.SaveSettingsNow.
      TFModFortRiseVariantSpeedModule.SaveSettingsNow();

      Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 12, true);
      tween.OnUpdate = delegate (Tween t)
      {
        Position = Vector2.Lerp(tweenTo, tweenFrom, t.Eased);
      };
      tween.OnComplete = delegate (Tween t)
      {
        // Rendre la main a la case d'ou l'on vient, sinon l'ecran des variantes
        // n'a plus rien de selectionne et ne repond plus.
        toggle.Selected = true;
        RemoveSelf();
      };
      Add(tween);
    }

    protected override void OnSelect() { }

    protected override void OnDeselect() { }

    protected override void OnConfirm() { }
  }
}
