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

        public static readonly string[] All = { Anger, Disgust, Fear, Happiness, Sadness };
    }

    // ---------- FEEDBACK ----------
    public static class Feedback
    {
        public const string VeryWrong = "No way!";
        public const string Wrong = "Not quite.";
        public const string Medium = "Neither here nor there.";
        public const string QuiteRight = "Yeah, somewhat...";
        public const string SpotOn = "Correct!";

        public static readonly string[] All = { VeryWrong, Wrong, Medium, QuiteRight, SpotOn };
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