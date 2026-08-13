using FortRise;

namespace TFModFortRiseVariantSpeed
{
  public class TFModFortRiseVariantSpeedSettings : ModuleSettings
  {
    public override void Create(ISettingsCreate settings)
    {
      settings.CreateNumber("Speed (% of normal run speed)", speedPercent,
          (x) => speedPercent = x, MinPercent, MaxPercent);
    }

    /// <summary>Bornes du reglage, en pourcentage de la vitesse de course normale.</summary>
    public const int MinPercent = 105;
    public const int MaxPercent = 250;

    /// <summary>Pas d'un cran de la fenetre : 5 %, assez fin pour se doser.</summary>
    public const int Step = 5;

    /// <summary>
    /// La vitesse de course de la variante, en pourcentage de la normale.
    ///
    /// 140 par defaut, la valeur des bottes de vitesse du jeu : la variante fait
    /// exactement ce que font les bottes, elle le fait seulement reglable.
    ///
    /// Un entier et non un flottant : il se lit dans le fichier de reglages, il
    /// s'affiche sans arrondi, et le menu de FortRise sait deja editer un nombre.
    /// </summary>
    public int speedPercent { get; set; } = 140;

    /// <summary>Le multiplicateur applique a la course, borne.</summary>
    public float Factor
    {
      get
      {
        int percent = speedPercent;
        if (percent < MinPercent) percent = MinPercent;
        if (percent > MaxPercent) percent = MaxPercent;
        return percent / 100f;
      }
    }
  }
}
