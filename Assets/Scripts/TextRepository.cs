using System.Collections.Generic;
using UnityEngine;

//Central store of hardcoded strings, grouped by the situation they're used in.
public static class TextRepository
{
    // ---------- CLUES ----------
    public static class Clues
    {
        public const string Anger1 = "Thunder awakens the sleeping lake. Capture the embodiment of wrath.";
        public const string Anger2 = "The angry dragon stirs in the depths: it accepts no harmony, biting only to destroy.";
        public const string Anger3 = "Climbing the hostile stream. Seek the spirit that protects its domain.";
        public const string Disgust1 = "The image of filter. Revulsion preserves the purity of the pool.";
        public const string Disgust2 = "The waters turn thick with decay. Wait for the leviathan whose presence breeds nausea.";
        public const string Disgust3 = "Discriminating spirit under marsh. It strikes the lure to purge the river of what does not belong.";
        public const string Fear1 = "Judgement. It is wise to meet heaven's dark void with tremble and fear.";
        public const string Fear2 = "Wind over the chasm. Startled prey leaps in the dark.";
        public const string Fear3 = "Obstruction in the pitch-black trench. Wait for the sudden tug of a heart driven by perpetual alarm.";
        public const string Happiness1 = "Trust in joy. Auspicious.";
        public const string Happiness2 = "Abundance without measure. Seek the reckless swimmer that leaps into the blazing light.";
        public const string Happiness3 = "The golden sparkle stirs in the pool. Hook the creature whose leap brings delight to the tranquil deep.";
        public const string Sadness1 = "Water beneath the willow. The stream of tears cradles a weary heart.";
        public const string Sadness2 = "The image of the waning moon. Seek the creature heavy with loss.";
        public const string Sadness3 = "Tears falling into the deep stream. Hook the creature thriving only where joy has withered away.";

        public static string TextFor(Emotion emotion)
        {
            switch (emotion)
            {
                case Emotion.Anger: return Pick(Anger1, Anger2, Anger3);
                case Emotion.Disgust: return Pick(Disgust1, Disgust2, Disgust3);
                case Emotion.Fear: return Pick(Fear1, Fear2, Fear3);
                case Emotion.Happiness: return Pick(Happiness1, Happiness2, Happiness3);
                case Emotion.Sadness: return Pick(Sadness1, Sadness2, Sadness3);
                default: return string.Empty;
            }
        }

        private static string Pick(params string[] options)
        {
            return options[Random.Range(0, options.Length)];
        }
    }

    // ---------- FEEDBACK ----------
    public static class Feedback
    {
        public const string VeryWrong = "Big NO. Did you even look at it before reeling?";
        public const string Wrong = "Weak match. This fish is out of touch.";
        public const string Medium = "Half points. The vibe is watered down.";
        public const string QuiteRight = "Pretty close! It's got the spirit, just needs coffee.";
        public const string SpotOn = "Spot on! This fish is the CEO of that feeling.";

        public static string ForScore(int score)
        {
            switch (score)
            {
                case 1: return VeryWrong;
                case 2: return Wrong;
                case 3: return Medium;
                case 4: return QuiteRight;
                case 5: return SpotOn;
                default: return score < 1 ? VeryWrong : SpotOn;
            }
        }
    }

    // ---------- RESULTS ----------
    public static class Results
    {
        public const string BestAnger = "Congrats! You're the Expert in Anger.";
        public const string BestDisgust = "Congrats! You're the Lord of Sadness.";
        public const string BestFear = "Congrats! You're the Lord of Disgust.";
        public const string BestHappiness = "Congrats! You're the Expert in Joy.";
        public const string BestSadness = "Congrats! You're the Master of Fear.";

        public static readonly string[] All = { BestAnger, BestDisgust, BestFear, BestHappiness, BestSadness };
    }
}