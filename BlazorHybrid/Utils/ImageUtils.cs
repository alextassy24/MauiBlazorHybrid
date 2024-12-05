namespace BlazorHybrid.Utils
{
    public static class ImageUtils
    {
        private static readonly string[] HealthyFoodImages =
        [
            "https://images.unsplash.com/photo-1547592180-85f173990554?w=800&q=80", // Colorful fruits
            "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=800&q=80", // Vegetable bowl
            "https://images.unsplash.com/photo-1490645935967-10de6ba17061?w=800&q=80", // Breakfast spread
            "https://images.unsplash.com/photo-1495521821757-a1efb6729352?w=800&q=80", // Berries
            "https://images.unsplash.com/photo-1482049016688-2d3e1b311543?w=800&q=80", // Healthy breakfast
            "https://images.unsplash.com/photo-1511690743698-d9d85f2fbf38?w=800&q=80", // Salad bowl
            "https://images.unsplash.com/photo-1498837167922-ddd27525d352?w=800&q=80", // Smoothie bowls
            "https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=800&q=80"  // Plated meal
        ];

        private static readonly Dictionary<string, string[]> CategoryImages = new()
        {
            ["strength"] =
            [
                "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&q=80", // Weight training
                "https://images.unsplash.com/photo-1583454110551-21f2fa2afe61?w=800&q=80", // Deadlift
                "https://images.unsplash.com/photo-1591940743498-55d7e0544d3b?w=800&q=80"  // Power rack
            ],

            ["cardio"] =
            [
                "https://images.unsplash.com/photo-1538805060514-97d9cc17730c?w=800&q=80", // Running
                "https://images.unsplash.com/photo-1518611012118-696072aa579a?w=800&q=80", // Cycling
                "https://images.unsplash.com/photo-1434596922112-19c563067271?w=800&q=80"  // Swimming
            ],

            ["hiit"] =
            [
                "https://images.unsplash.com/photo-1599058945522-28d584b6f0ff?w=800&q=80", // Box jumps
                "https://images.unsplash.com/photo-1601422407692-ec4eeec1d9b3?w=800&q=80", // Battle ropes
                "https://images.unsplash.com/photo-1517963879433-6ad2b056d712?w=800&q=80"  // CrossFit
            ],

            ["hypertrophy"] =
            [
                "https://images.unsplash.com/photo-1532029837206-abbe2b7620e3?w=800&q=80", // Bodybuilding
                "https://images.unsplash.com/photo-1577221084712-45b0445d2b00?w=800&q=80", // Dumbbell rack
                "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?w=800&q=80"  // Gym equipment
            ]
        };

        private static readonly string[] DefaultImages =
        [
            "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&q=80",
            "https://images.unsplash.com/photo-1576678927484-cc907957088c?w=800&q=80",
            "https://images.unsplash.com/photo-1549060279-7e168fcee0c2?w=800&q=80"
        ];

        public static string GetRandomFoodImage()
        {
            var random = new Random();
            return HealthyFoodImages[random.Next(HealthyFoodImages.Length)];
        }

        public static string GetRandomImage(string? category = null)
        {
            var random = new Random();

            if (string.IsNullOrWhiteSpace(category) || !CategoryImages.ContainsKey(category.ToLower()))
            {
                return DefaultImages[random.Next(DefaultImages.Length)];
            }

            var images = CategoryImages[category.ToLower()];
            return images[random.Next(images.Length)];
        }
    }
}
