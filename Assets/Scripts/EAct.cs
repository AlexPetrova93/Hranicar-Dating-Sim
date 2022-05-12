using Yarn.Unity;

public enum EAct
{
    Seznameni,
    SexAVztahy,
    NeurotickaGalerie,
    NeurotickaHospoda,
    Zaver,
    None
}

public static class EActExtensions
{
    public static EAct ToNextEAct(this EAct previousAct, ESetting nextSetting)
    {
        switch (previousAct)
        {
            case EAct.Seznameni:
                return EAct.SexAVztahy;
            case EAct.SexAVztahy:
                return nextSetting == ESetting.Pub ? EAct.NeurotickaHospoda : EAct.NeurotickaGalerie;
            case EAct.NeurotickaGalerie:
                return EAct.Zaver;
            case EAct.NeurotickaHospoda:
                return EAct.Zaver;
            default:
                return EAct.None;
        }
    }
}