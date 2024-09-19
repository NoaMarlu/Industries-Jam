using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // 
    //  イージング関数     unity.Ver
    // 
    //------------------------------------------------------------------------------

    public enum EaseType
    {
        SINE_IN,
        SINE_OUT,
        SINE_INOUT,

        QUAD_IN,
        QUAD_OUT,
        QUAD_INOUT,

        CUBIC_IN,
        CUBIC_OUT,
        CUBIC_INOUT,

        QUART_IN,
        QUART_OUT,
        QUART_INOUT,

        QUINT_IN,
        QUINT_OUT,
        QUINT_INOUT,

        EXPO_IN,
        EXPO_OUT,
        EXPO_INOUT,

        CIRC_IN,
        CIRC_OUT,
        CIRC_INOUT,

        BACK_IN,
        BACK_OUT,
        BACK_INOUT,

        ELASTIC_IN,
        ELASTIC_OUT,
        ELASTIC_INOUT,

        BOUNCE_IN,
        BOUNCE_OUT,
        BOUNCE_INOUT,
    }


    public static float Enable(EaseType easeType, float t, float d, float b = 0, float c = 1)
    {
        float easePos = 0;

        switch(easeType)
        {
            case EaseType.SINE_IN: easePos = SineIn(t, d, b, c); break;
            case EaseType.SINE_OUT: easePos = SineOut(t, d, b, c); break;
            case EaseType.SINE_INOUT: easePos = SineInOut(t, d, b, c); break;

            case EaseType.QUAD_IN: easePos = QuadIn(t, d, b, c); break;
            case EaseType.QUAD_OUT: easePos = QuadOut(t, d, b, c); break;
            case EaseType.QUAD_INOUT: easePos = QuadInOut(t, d, b, c); break;

            case EaseType.CUBIC_IN: easePos = CubicIn(t, d, b, c); break;
            case EaseType.CUBIC_OUT: easePos = CubicOut(t, d, b, c); break;
            case EaseType.CUBIC_INOUT: easePos = CubicInOut(t, d, b, c); break;

            case EaseType.QUART_IN: easePos = QuartIn(t, d, b, c); break;
            case EaseType.QUART_OUT: easePos = QuartOut(t, d, b, c); break;
            case EaseType.QUART_INOUT: easePos = QuartInOut(t, d, b, c); break;

            case EaseType.QUINT_IN: easePos = QuintIn(t, d, b, c); break;
            case EaseType.QUINT_OUT: easePos = QuintOut(t, d, b, c); break;
            case EaseType.QUINT_INOUT: easePos = QuintInOut(t, d, b, c); break;

            case EaseType.EXPO_IN: easePos = ExpoIn(t, d, b, c); break;
            case EaseType.EXPO_OUT: easePos = ExpoOut(t, d, b, c); break;
            case EaseType.EXPO_INOUT: easePos = ExpoInOut(t, d, b, c); break;

            case EaseType.CIRC_IN: easePos = CircIn(t, d, b, c); break;
            case EaseType.CIRC_OUT: easePos = CircOut(t, d, b, c); break;
            case EaseType.CIRC_INOUT: easePos = CircInOut(t, d, b, c); break;

            case EaseType.BACK_IN: easePos = BackIn(t, d, b, c); break;
            case EaseType.BACK_OUT: easePos = BackOut(t, d, b, c); break;
            case EaseType.BACK_INOUT: easePos = BackInOut(t, d, b, c); break;

            case EaseType.ELASTIC_IN: easePos = ElasticIn(t, d, b, c); break;
            case EaseType.ELASTIC_OUT: easePos = ElasticOut(t, d, b, c); break;
            case EaseType.ELASTIC_INOUT: easePos = ElasticInOut(t, d, b, c); break;

            case EaseType.BOUNCE_IN: easePos = BounceIn(t, d, b, c); break;
            case EaseType.BOUNCE_OUT: easePos = BounceOut(t, d, b, c); break;
            case EaseType.BOUNCE_INOUT: easePos = BounceInOut(t, d, b, c); break;
        }

        return easePos;
    }



    // Sine 
    public static float SineIn(float t, float d, float b = 0, float c = 1)
    {
        return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
    }

    public static float SineOut(float t, float d, float b = 0, float c = 1)
    {
        return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
    }

    public static float SineInOut(float t, float d, float b = 0, float c = 1)
    {
        return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
    }

    // Quad
    public static float QuadIn(float t, float d, float b = 0, float c = 1)
    {
        return c * (t /= d) * t + b;
    }
    public static float QuadOut(float t, float d, float b = 0, float c = 1)
    {
        return -c * (t /= d) * (t - 2) + b;
    }

    public static float QuadInOut(float t, float d, float b = 0, float c = 1)
    {
        if ((t /= d / 2) < 1) return ((c / 2) * (t * t)) + b;
        return -c / 2 * (((t - 2) * (--t)) - 1) + b;
    }

    // Cubic
    public static float CubicIn(float t, float d, float b = 0, float c = 1)
    {
        return c * (t /= d) * t * t + b;
    }
    public static float CubicOut(float t, float d, float b = 0, float c = 1)
    {
        return c * ((t = t / d - 1) * t * t + 1) + b;
    }

    public static float CubicInOut(float t, float d, float b = 0, float c = 1)
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t + 2) + b;
    }

    // Quart
    public static float QuartIn(float t, float d, float b = 0, float c = 1)
    {
        return c * (t /= d) * t * t * t + b;
    }
    public static float QuartOut(float t, float d, float b = 0, float c = 1)
    {
        return -c * ((t = t / d - 1) * t * t * t - 1) + b;
    }

    public static float QuartInOut(float t, float d, float b = 0, float c = 1)
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
        return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
    }

    // Quint
    public static float QuintIn(float t, float d, float b = 0, float c = 1)
    {
        return c * (t /= d) * t * t * t * t + b;
    }
    public static float QuintOut(float t, float d, float b = 0, float c = 1)
    {
        return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
    }

    public static float QuintInOut(float t, float d, float b = 0, float c = 1)
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
    }

    // Expo
    public static float ExpoIn(float t, float d, float b = 0, float c = 1)
    {
        return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
    }
    public static float ExpoOut(float t, float d, float b = 0, float c = 1)
    {
        return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
    }

    public static float ExpoInOut(float t, float d, float b = 0, float c = 1)
    {
        if (t == 0) return b;
        if (t == d) return b + c;
        if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
        return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
    }

    // Circ
    public static float CircIn(float t, float d, float b = 0, float c = 1)
    {
        return -c * (Mathf.Sqrt(1 - (t /= d) * t) - 1) + b;
    }
    public static float CircOut(float t, float d, float b = 0, float c = 1)
    {
        return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
    }

    public static float CircInOut(float t, float d, float b = 0, float c = 1)
    {
        if ((t /= d / 2) < 1) return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
        return c / 2 * (Mathf.Sqrt(1 - t * (t -= 2)) + 1) + b;
    }

    // Back
    public static float BackIn(float t, float d, float b = 0, float c = 1)
    {
        float s = 1.70158f;
        float postFix = t /= d;
        return c * (postFix) * t * ((s + 1) * t - s) + b;
    }
    public static float BackOut(float t, float d, float b = 0, float c = 1)
    {
        float s = 1.70158f;
        return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
    }

    public static float BackInOut(float t, float d, float b = 0, float c = 1)
    {
        float s = 1.70158f;
        if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
        float postFix = t -= 2;
        return c / 2 * ((postFix) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
    }


    // Elastic
    public static float ElasticIn(float t, float d, float b = 0, float c = 1)
    {
        if (t == 0) return b; if ((t /= d) == 1) return b + c;
        float p = d * .3f;
        float a = c;
        float s = p / 4;
        float postFix = a * Mathf.Pow(2, 10 * (t -= 1));
        return -(postFix * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
    }

    public static float ElasticOut(float t, float d, float b = 0, float c = 1)
    {
        if (t == 0) return b; if ((t /= d) == 1) return b + c;
        float p = d * .3f;
        float a = c;
        float s = p / 4;
        return (a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
    }

    public static float ElasticInOut(float t, float d, float b = 0, float c = 1)
    {
        if (t == 0) return b; if ((t /= d / 2) == 2) return b + c;
        float p = d * (.3f * 1.5f);
        float a = c;
        float s = p / 4;

        if (t < 1)
        {
            float postFix1 = a * Mathf.Pow(2, 10 * (t -= 1));
            return -.5f * (postFix1 * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
        }
        float postFix = a * Mathf.Pow(2, -10 * (t -= 1));
        return postFix * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
    }

    // Bounce
    public static float BounceIn(float t, float d, float b = 0, float c = 1)
    {
        return c - BounceOut(d - t, 0, c, d) + b;
    }
    public static float BounceOut(float t, float d, float b = 0, float c = 1)
    {
        if ((t /= d) < (1 / 2.75f))
        {
            return c * (7.5625f * t * t) + b;
        }
        else if (t < (2 / 2.75f))
        {
            float postFix = t -= (1.5f / 2.75f);
            return c * (7.5625f * (postFix) * t + .75f) + b;
        }
        else if (t < (2.5 / 2.75))
        {
            float postFix = t -= (2.25f / 2.75f);
            return c * (7.5625f * (postFix) * t + .9375f) + b;
        }
        else
        {
            float postFix = t -= (2.625f / 2.75f);
            return c * (7.5625f * (postFix) * t + .984375f) + b;
        }
    }

    public static float BounceInOut(float t, float d, float b = 0, float c = 1)
    {
        if (t < d / 2) return BounceIn(t * 2, 0, c, d) * .5f + b;
        else return BounceOut(t * 2 - d, 0, c, d) * .5f + c * .5f + b;
    }
}
