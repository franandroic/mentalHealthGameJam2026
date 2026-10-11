using System.Collections.Generic;

//Central store of hardcoded strings, grouped by the situation they're used in.
public static class TextRepository
{
    // ---------- CLUES ----------
    public static class Clues
    {
        public const string Anger = "Thunder awakens the sleeping lake. Capture the embodiment of wrath.";
        public const string Disgust = "The image of filter. Revulsion preserves the purity of the pool.";
        public const string Fear = "Judgement. It is wise to meet heaven's dark void with tremble and fear.";
        public const string Happiness = "Trust in joy. Auspicious.";
        public const string Sadness = "Water beneath the willow. The stream of tears cradles a weary heart.";

        public static string TextFor(Emotion emotion)
        {
            switch (emotion)
            {
                case Emotion.Anger: return Anger;
                case Emotion.Disgust: return Disgust;
                case Emotion.Fear: return Fear;
                case Emotion.Happiness: return Happiness;
                case Emotion.Sadness: return Sadness;
                default: return string.Empty;
            }
        }
    }

    // ---------- FEEDBACK ----------
    public static class Feedback
    {
        public const string VeryWrong = "No way!";
        public const string Wrong = "Not quite.";
        public const string Medium = "Neither here nor there.";
        public const string QuiteRight = "Yeah, somewhat...";
        public const string SpotOn = "Correct!";

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
        public const string BestAnger = "The anger king!";
        public const string BestDisgust = "The disgust champion!";
        public const string BestFear = "The fear GOAT!";
        public const string BestHappiness = "The happiness legend!";
        public const string BestSadness = "The sadness player!";

        public static readonly string[] All = { BestAnger, BestDisgust, BestFear, BestHappiness, BestSadness };
    }
}